﻿using System.Linq;
using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
namespace FinalYearProject.Areas.Staff
{
    [Area("Staff")]
    //[Authorize(Roles = SD.TrainingManage)]
    public class TrainingController : Controller
    {

        private readonly ApplicationDbContext _db;

        public TrainingController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Training.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Training training)
        {
            // Check if the start date is in the future
            if (training.start_date < DateTime.Now)
            {
                ModelState.AddModelError("Custom Error", "Start Date should be after Today Date");
            }
            else
            {
                // Generate a unique training_id based on the start date
                DateTime date = (DateTime)training.start_date;
                training.training_id = "T" + date.Year + date.Month + date.Day + date.Hour + date.Minute;

                // If the model is valid, save the training
                if (ModelState.IsValid)
                {
                    _db.Training.Add(training);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Return the same view if the model state is not valid or if there's a custom error
            return View(training);
        }


        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var trainingFromDb = _db.Training.Find(id);
            if (trainingFromDb == null)
            {
                return NotFound();
            }
            return View(trainingFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Training training)
        {
            if (training.start_date < DateTime.Now)
            {
                ModelState.AddModelError("Custom Error", "Start Date should after Today Date");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _db.Training.Update(training);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(training);
        }

        public async Task<IActionResult> ViewTraining()
        {
            var ID = from s in _db.TrainingProgress where s.completion == false && s.duration_left == 0 select s.training_id;
            string[] IDArray = ID.ToArray();
            var eID = from s in _db.TrainingProgress where s.completion == false && s.duration_left == 0 select s.staff_id;
            string[] eIDArray = eID.ToArray();
            for (int i = 0; i < IDArray.Length; i++)
            {
                TrainingProgress trainingUpdated = await (from s in _db.TrainingProgress where s.completion == false && s.duration_left == 0 && s.training_id == IDArray[i] && s.staff_id == eIDArray[i] select s).FirstOrDefaultAsync();
                trainingUpdated.completion = true;
                trainingUpdated.duration_left = 0;
                _db.TrainingProgress.Update(trainingUpdated);
                await _db.SaveChangesAsync();
            }
            var employeeID = from s in _db.TrainingProgress where s.completion == false && s.duration_left != 0 select s.staff_id;
            string[] employeeIDArray = employeeID.ToArray();

            var trainingProgressID = from s in _db.TrainingProgress where s.completion == false && s.duration_left != 0 select s.training_id;
            string[] trainingProgressIDArray = trainingProgressID.ToArray();
            for (int i = 0; i < trainingProgressIDArray.Length; i++)
            {
                TrainingProgress dbFromTP = await (from s in _db.TrainingProgress where s.completion == false && s.duration_left != 0 && s.training_id == trainingProgressIDArray[i] && s.staff_id == employeeIDArray[i] select s).FirstAsync();
                var startDate = await (from s in _db.Training where s.training_id == trainingProgressIDArray[i] select s.start_date).FirstOrDefaultAsync();
                DateTime startTime = (DateTime)startDate;
                var duration = await (from s in _db.Training where s.training_id == trainingProgressIDArray[i] select s.duration).FirstOrDefaultAsync();
                TimeSpan timeDiff = startTime.AddDays((double)duration) - DateTime.Now;
                if (timeDiff.TotalDays <= 0)
                {
                    dbFromTP.completion = true;
                    dbFromTP.duration_left = 0;
                    _db.TrainingProgress.Update(dbFromTP);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    dbFromTP.completion = false;
                    dbFromTP.duration_left = (int)timeDiff.TotalDays;
                }
                _db.TrainingProgress.Update(dbFromTP);
                await _db.SaveChangesAsync();
            }
            return View(await _db.TrainingProgress.ToListAsync());
        }

        public async Task<IActionResult> AssignEmployee()
        {
            var trainingID = await _db.Training.ToListAsync();
            var staffID = await _db.EmployeeDetails.ToListAsync();
            ViewBag.trainingID = new SelectList(trainingID.AsEnumerable(), "training_id", "training_id");
            ViewBag.staffID = new SelectList(staffID.AsEnumerable(), "employee_id", "employee_id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignEmployee(TrainingProgress trainingProgress)
        {
            var startDate = await (from s in _db.Training where s.training_id == trainingProgress.training_id select s.start_date).FirstOrDefaultAsync();
            DateTime startTime = (DateTime)startDate;

            var employeeID = await _db.EmployeeDetails.FirstOrDefaultAsync(e => e.employee_id == trainingProgress.staff_id);
            var training = await _db.Training.FirstOrDefaultAsync(e => e.training_id == trainingProgress.training_id);

            if (startDate <= DateTime.Now)
            {
                ModelState.AddModelError("Custom Error", "This Training has already Started");
            }
            else
            {
                var duration = await (from s in _db.Training where s.training_id == trainingProgress.training_id select s.duration).FirstOrDefaultAsync();
                TimeSpan timeDiff = startTime.AddDays((double)duration) - DateTime.Now;
                trainingProgress.duration_left = (int)timeDiff.TotalDays;

                var proofDocument = await _db.Document.FirstOrDefaultAsync(d => d.owner_id == employeeID.employee_id && d.document_name.Contains(training.training_name) && d.expiry_date > DateTime.Now);

                if (proofDocument != null)
                {
                    trainingProgress.completion = true;
                    trainingProgress.cert_id = proofDocument.document_id;
                }
                else
                {
                    trainingProgress.completion = false;
                    trainingProgress.cert_id = null;
                }

                if (ModelState.IsValid)
                {
                    _db.TrainingProgress.Add(trainingProgress);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(trainingProgress);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var training = await _db.Training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            return View(training);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var training = await _db.Training.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            _db.Training.Remove(training);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}