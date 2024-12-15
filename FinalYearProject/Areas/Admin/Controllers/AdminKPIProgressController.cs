using Microsoft.AspNetCore.Mvc;
using FinalYearProject.Models;
using FinalYearProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FinalYearProject.Models.ViewModels;


namespace FinalYearProject.Controllers
{
    [Area("Admin")]
    public class AdminKPIProgressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminKPIProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all proof submissions

        public async Task<IActionResult> Index()
        {
            var proofSubmissions = await _context.ProofSubmissions
                .Include(p => p.Employee)
                .Include(p => p.CompanyKPI)
                .ToListAsync();

            // Fetch KPI rewards and ensure we load Employee and CompanyKPI
            var kpiRewards = await _context.KPIRewards
                .Include(r => r.Employee)
                .Include(r => r.CompanyKPI)
                .ToListAsync();



            var viewModel = new AdditionalSalaryVM
            {
                ProofSubmissions = proofSubmissions,
                KPIRewards = kpiRewards // Pass the rewards to the ViewModel
            };

            return View(viewModel);
        }





        // Display proof submission details and options to approve/reject
        public async Task<IActionResult> Details(int id)
        {
            var proofSubmission = await _context.ProofSubmissions
                .Include(p => p.Employee)
                .Include(p => p.CompanyKPI)
                .FirstOrDefaultAsync(p => p.ProofSubmissionId == id);

            if (proofSubmission == null)
            {
                return NotFound();
            }

            return View(proofSubmission);
        }

        // Handle proof approval or rejection
        [HttpPost]
        public async Task<IActionResult> ApproveRejectProof(int id, string status, string comment)
        {
            var proofSubmission = await _context.ProofSubmissions
                .FirstOrDefaultAsync(p => p.ProofSubmissionId == id);

            if (proofSubmission == null)
            {
                return NotFound();
            }

            proofSubmission.Status = status; // "Approved" or "Rejected"
            proofSubmission.AdminComment = comment; // Comment added by admin

            _context.Update(proofSubmission);
            await _context.SaveChangesAsync();

            // If approved, check if the current progress matches the target
            if (proofSubmission.Status == "Approved")
            {
                // Retrieve the associated KPI
                var companyKPI = await _context.CompanyKPIs
                    .FirstOrDefaultAsync(k => k.KPI_id == proofSubmission.KPI_id);

                if (companyKPI != null)
                {
                    // Ensure that KPIAmount is not null and represents valid progress
                    if (proofSubmission.KPIAmount > 0)
                    {
                        // Update the Company's CurrentProgress after adding the proof's progress
                        companyKPI.CurrentProgress += proofSubmission.KPIAmount; // Add the progress from the proof submission

                        // Attach entity if it's detached
                        if (_context.Entry(companyKPI).State == EntityState.Detached)
                        {
                            _context.Attach(companyKPI);
                        }

                        // Update the KPI record with the new progress
                        _context.Update(companyKPI);
                        await _context.SaveChangesAsync(); // Save the new progress to the database

                        // Check if the target KPI is now met
                        if (companyKPI.CurrentProgress >= companyKPI.target_KPI)
                        {
                            // Retrieve all employees who are eligible for this KPI
                            var employees = await _context.EmployeeDetails.ToListAsync(); // Assuming all employees are eligible

                            // Loop through all employees and create a reward for each
                            foreach (var employee in employees)
                            {
                                var reward = new KPIReward
                                {
                                    EmployeeId = employee.employee_id, // Assign reward to each employee
                                    KPIId = companyKPI.KPI_id, // Assign the correct KPI ID
                                    HitKPIReward = companyKPI.hit_KPI_allowance ?? 0, // Ensure the reward is not null
                                    DateAdded = DateTime.Now
                                };

                                // Add the reward entry for each employee
                                _context.Add(reward);
                            }

                            await _context.SaveChangesAsync(); // Save all rewards to the database

                            // Update the proof submission status to "Completed" or "Hit"
                            proofSubmission.Status = "Completed";
                            _context.Update(proofSubmission);
                            await _context.SaveChangesAsync(); // Save the updated proof submission status
                        }
                    }
                }
            }

            // If rejected, we don't need to adjust the salary
            return RedirectToAction(nameof(Index));
        }
    }
}
