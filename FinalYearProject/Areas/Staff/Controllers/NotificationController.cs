using System.Security.Claims;
using FinalYearProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var aspId = User.Identity?.Name;

            if (aspId == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var currentUser = await _db.EmployeeDetails.Where(e => e.user_id == aspId).FirstOrDefaultAsync();

            var currentUserEmail = currentUser.email;

            var notifications = await _db.Notification.ToListAsync();

            var filteredNotifications = notifications.Where(n =>
                n.notification_receiver.Equals("Everyone", StringComparison.OrdinalIgnoreCase) ||
                n.notification_receiver.Split(',').Select(e => e.Trim()).Contains(currentUserEmail)
            ).OrderByDescending(n => n.notification_date);

            return View(filteredNotifications);
        }

        public async Task<IActionResult> Details(string? id)
        {
            var notification = await _db.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }
            else
            {
                return View(notification);
            }
        }
    }
}
