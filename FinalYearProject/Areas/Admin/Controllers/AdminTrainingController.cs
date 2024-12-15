using Microsoft.AspNetCore.Mvc;
using FinalYearProject.Data;
using FinalYearProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FinalYearProject.Controllers
{
	[Area("Admin")]
	public class AdminTrainingController : Controller
	{
		private readonly ApplicationDbContext _db;

		public AdminTrainingController(ApplicationDbContext db)
		{
			_db = db;
		}

		public async Task<IActionResult> CompletedTrainings()
		{
			// Define the role ID for "Interviewee"
			string intervieweeRoleId = "R00005"; // Replace with the actual role_id for "Interviewee"

			// Get employees who have completed all training and are currently in the "Interviewee" role
			var completedInterviewees = await _db.EmployeeDetails
				.Where(e => e.staff_role == intervieweeRoleId && // Only those in "Interviewee" role
							_db.IntervieweeProgress
								.Where(ip => ip.EmployeeId == e.user_id && ip.Status != "Completed")
								.Count() == 0) // All interview training must be completed
				.Include(e => e.Role) // Include role info if needed
				.ToListAsync();

			return View(completedInterviewees);
		}

		[HttpPost]
		public async Task<IActionResult> PromoteToStaff(string employeeId)
		{
			var employee = await _db.EmployeeDetails
				.FirstOrDefaultAsync(e => e.employee_id == employeeId);

			if (employee != null)
			{
				// Change the role to "R00006" (new role for Staff)
				employee.staff_role = "R00006";

				// Save changes
				await _db.SaveChangesAsync();
			}

			// Refresh the page after promotion
			return RedirectToAction("CompletedTrainings");
		}



		public async Task<IActionResult> Index()
		{
			var intervieweeProgress = await _db.IntervieweeProgress
				.Include(p => p.Training)
				.ToListAsync();

			return View(intervieweeProgress);
		}

		[HttpPost]
		public async Task<IActionResult> ApproveResult(int id)
		{
			var progress = await _db.IntervieweeProgress
				.Include(p => p.Training)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (progress != null)
			{
				progress.IsApproved = true;
				progress.Status = "Completed";

				await _db.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}
	}
}