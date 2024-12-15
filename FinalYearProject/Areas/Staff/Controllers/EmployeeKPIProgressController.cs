using Microsoft.AspNetCore.Mvc;
using FinalYearProject.Models;
using FinalYearProject.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Controllers
{
    [Area("Staff")]
    public class EmployeeKPIProgressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeKPIProgressController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var kpis = _context.CompanyKPIs.ToList();
            return View(kpis);
        }

        public IActionResult SubmitProof(string KPI_id)
        {
            if (string.IsNullOrEmpty(KPI_id))
            {
                return BadRequest("Invalid KPI ID.");
            }

            var model = new ProofSubmission
            {
                KPI_id = KPI_id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitProof(ProofSubmission proofSubmission, IFormFile file)
        {
            
            string loggedInEmployeeId = User.Identity?.Name;

            if (loggedInEmployeeId == null)
            {
                
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            
            var currentUser = await _context.EmployeeDetails
                .Where(e => e.user_id == loggedInEmployeeId)
                .FirstOrDefaultAsync();

            if (currentUser == null)
            {
                
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            
            proofSubmission.Employee_id = currentUser.employee_id;

            
            proofSubmission.Status = "Pending";

            
            proofSubmission.DateSubmitted = DateTime.Now;

            
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                proofSubmission.ProofFileUrl = "/uploads/" + file.FileName;
            }

            
            _context.ProofSubmissions.Add(proofSubmission);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<int> GenerateSubmitProofID()
        {
            var latestProofSubmission = await _context.ProofSubmissions
                .OrderByDescending(n => n.ProofSubmissionId)
                .FirstOrDefaultAsync();

            if (latestProofSubmission == null)
            {
                return 1;
            }

            return latestProofSubmission.ProofSubmissionId + 1;
        }
    }
}
