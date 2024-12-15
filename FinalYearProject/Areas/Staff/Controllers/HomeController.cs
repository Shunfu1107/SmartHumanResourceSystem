using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUserId = User.Identity?.Name;

            if (loggedInUserId != null)
            {
                var loggedInUser = await _db.EmployeeDetails.FirstOrDefaultAsync(u => u.user_id == loggedInUserId);

                var totalEmployees = await _db.EmployeeDetails.CountAsync();
                var totaltodayLeaves = _db.Leave.Count(l => l.leave_start.Date <= DateTime.Today && l.leave_end.Date >= DateTime.Today);
                var totaltodayNotifications = _db.Notification.Count(n => n.notification_date == DateTime.Today);
                var todayNotifications = await _db.Notification.Where(n => n.notification_date == DateTime.Today).OrderByDescending(n => n.notification_date).Take(5).ToListAsync();
                var todayLeavesEmployee = await _db.Leave.Where(l => l.leave_start.Date <= DateTime.Today && l.leave_end.Date >= DateTime.Today).Take(5).ToListAsync();
                List<Leave> upcomingLeaves = null;


                if (loggedInUser != null)
                {
                    upcomingLeaves = await _db.Leave.Where(l => l.staff_id == loggedInUser.employee_id && l.leave_start > DateTime.Today).OrderBy(l => l.leave_start).Take(5).ToListAsync();
                }

                var dashboardData = new DashboardVM
                {
                    TotalEmployees = totalEmployees,
                    TodayLeaves = totaltodayLeaves,
                    TodayNotifications = totaltodayNotifications,
                    TodayNotificationsList = todayNotifications,
                    TodayLeavesList = todayLeavesEmployee,
                    UpcomingLeaves = upcomingLeaves,
                    TodayDate = DateTime.Today
                };

                return View(dashboardData);
            } else
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }
        }
    }
}
