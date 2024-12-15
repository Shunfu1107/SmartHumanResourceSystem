using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using SendGrid.Helpers.Mail;

namespace FinalYearProject.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class AdditionalSalaryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdditionalSalaryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var aspId = User.Identity?.Name;

            if (aspId == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
            else
            {
                var currentUser = await _db.EmployeeDetails
                    .Where(e => e.user_id == aspId)
                    .FirstOrDefaultAsync();

                if (currentUser == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                else
                {
                    var proofSubmissions = await _db.ProofSubmissions
                        .Include(p => p.Employee)
                        .Include(p => p.CompanyKPI)
                        .Where(p => p.Employee.employee_id == currentUser.employee_id)
                        .ToListAsync();

                    var kpiRewards = await _db.KPIRewards
                        .Include(r => r.Employee)
                        .Include(r => r.CompanyKPI)
                        .Where(r => r.Employee.employee_id == currentUser.employee_id)
                        .ToListAsync();

                    var additionalSalaries = await _db.AdditionalSalary
                        .Where(s => s.EmployeeId == currentUser.employee_id)
                        .Include(s => s.Claim)
                        .ToListAsync();

                    AdditionalSalaryVM vm = new AdditionalSalaryVM()
                    {
                        Employee = currentUser,
                        AdditionalSalaries = additionalSalaries,
                        ProofSubmissions = proofSubmissions,
                        KPIRewards = kpiRewards
                    };

                    return View(vm);
                }
            }
        }


        // GET: Staff/AdditionalSalary/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var additionalSalary = await _db.AdditionalSalary
                .FirstOrDefaultAsync(s => s.Id == id);

            if (additionalSalary == null)
            {
                return NotFound();
            }

            // Return the Edit view with the existing record
            return View(additionalSalary);
        }

        // POST: Staff/AdditionalSalary/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,ClaimId,ClaimAmount,ApprovedAmount,DateAdded,ClaimFile")] AdditionalSalary updatedSalary)
        {
            if (id != updatedSalary.Id)
            {
                return NotFound();
            }

            // Retrieve the existing AdditionalSalary record from the database
            var existingSalary = await _db.AdditionalSalary
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            if (existingSalary == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Only update the ApprovedAmount if it has changed
                if (existingSalary.ApprovedAmount != updatedSalary.ApprovedAmount)
                {
                    existingSalary.ApprovedAmount = updatedSalary.ApprovedAmount;
                    existingSalary.DateAdded = DateTime.Now;  // Update DateAdded to current date to track changes
                }

                // Optionally update ClaimFile if provided
                if (!string.IsNullOrEmpty(updatedSalary.ClaimFile))
                {
                    existingSalary.ClaimFile = updatedSalary.ClaimFile;
                }

                try
                {
                    // Save the changes to the database
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalSalaryExists(updatedSalary.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect back to the Index page after updating
                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, return to the edit view with validation errors
            return View(updatedSalary);
        }




        // Check if the AdditionalSalary exists
        private bool AdditionalSalaryExists(int id)
        {
            return _db.AdditionalSalary.Any(e => e.Id == id);
        }
    }
}
