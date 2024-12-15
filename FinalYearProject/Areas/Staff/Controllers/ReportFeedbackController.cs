using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class ReportFeedbackController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public ReportFeedbackController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string feedback_content, string report_id)
        {
            var report = await _db.Report.FindAsync(report_id);

            var aspId = User.Identity?.Name;

            var currentUser = await _db.EmployeeDetails.Where(e => e.user_id == aspId).FirstOrDefaultAsync();

            if (report == null)
            {
                return NotFound();
            }
            else
            {
                report.report_status = "Commented";
            }

            var feedback = new ReportFeedback
            {
                feedback_id = GenerateReportFeedbackID().Result,
                feedback_content = feedback_content,
                report_id = report.report_id,
                employee_id = currentUser.employee_id,
                feedback_date = DateTime.Now
            };

            _db.ReportFeedback.Add(feedback);
            await _db.SaveChangesAsync();

            return RedirectToAction("Details", "Report", new { area = "Staff", id = report.report_id });
        }

        public async Task<string> GenerateReportFeedbackID()
        {
            string newId;
            string prefix = "REFE";

            var totalReport = await _db.ReportFeedback.CountAsync();

            newId = prefix + (totalReport + 1).ToString("00000");

            return newId;
        }
    }
}
