using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace FinalYearProject.Areas.Staff
{
    [Area("Staff")]
    //[Authorize(Roles = SD.TrainingManage)]
    public class IntervieweeTrainingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IntervieweeTrainingController(ApplicationDbContext db)
        {
            _db = db;
        }

        // List all trainings with "Interviewee" in the training_name
        public async Task<IActionResult> Index()
        {
            var currentUserRole = User.Identity.Name; // Assuming the user's ID is stored as their username. You may need to adjust this if your app uses roles.

            // Check if the current user is a staff (R00006)
            if (currentUserRole == "R00006")
            {
                // Display a message indicating staff do not need to take interviewee training
                ViewBag.Message = "Your role is Staff, you don't need to take the Interviewee training anymore.";
                return View(); // Return to the same page with the message
            }

            // For users with role R00005 (Interviewee), show the training details as normal
            var trainings = await _db.Training
                                     .Where(t => t.training_name.Contains("Interviewee"))
                                     .ToListAsync();

            return View(trainings); // Pass the list of trainings to the view
        }


        public async Task<IActionResult> Details(string? id)
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

            // Fetch previous submission if exists
            var progress = await _db.IntervieweeProgress
                                     .FirstOrDefaultAsync(p => p.TrainingId == id && p.EmployeeId == User.Identity.Name);
            ViewBag.Progress = progress; // This will be used to display current submission status

            return View(training);
        }


        // Submit a file (image) for training by an employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitResult(string trainingId, IFormFile submission)
        {
            if (submission != null && submission.Length > 0)
            {
                // Ensure the TrainingResult directory exists
                var webRootPath = Path.Combine("wwwroot", "TrainingResult");
                if (!Directory.Exists(webRootPath))
                {
                    Directory.CreateDirectory(webRootPath);
                }

                // Generate a unique filename for the submission
                var extension = Path.GetExtension(submission.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx" };
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("submission", "Invalid file type. Allowed types are: jpg, jpeg, png, pdf, doc, and docx.");
                    return RedirectToAction("Details", new { id = trainingId });
                }

                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(submission.FileName)}";
                var filePath = Path.Combine(webRootPath, fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await submission.CopyToAsync(stream);
                }

                var employeeId = User.Identity?.Name;
                if (employeeId == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                // Check if a record already exists for this training and employee
                var progress = await _db.IntervieweeProgress
                    .FirstOrDefaultAsync(p => p.TrainingId == trainingId && p.EmployeeId == employeeId);

                if (progress == null)
                {
                    // Create a new progress entry
                    progress = new IntervieweeProgress
                    {
                        TrainingId = trainingId,
                        EmployeeId = employeeId,
                        ResultImagePath = "/TrainingResult/" + fileName, // Updated path
                        Status = "Pending",
                        IsApproved = false
                    };
                    _db.IntervieweeProgress.Add(progress);
                }
                else
                {
                    // Update existing progress
                    progress.ResultImagePath = "/TrainingResult/" + fileName; // Updated path
                    progress.Status = "Pending";
                    progress.IsApproved = false;
                    _db.IntervieweeProgress.Update(progress);
                }

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("submission", "Please upload a valid file.");
            return RedirectToAction("Details", new { id = trainingId });
        }


    }
}
