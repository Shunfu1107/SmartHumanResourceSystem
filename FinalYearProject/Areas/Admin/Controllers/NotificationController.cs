using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FinalYearProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NotificationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Notification.ToListAsync());
        }

        public IActionResult Create()
        {
            Notification newNotification = new Notification()
            {
                notification_id = GenerateNotificationID().Result,
                notification_type = "Normal Notification"
            };

            return View(newNotification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notification notification)
        {
            notification.notification_date = DateTime.Now;

            if (notification.notification_receiver?.Contains("Everyone") == true)
            {
                notification.notification_receiver = "Everyone";
            }

            if (ModelState.IsValid)
            {
                _db.Notification.Add(notification);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(notification);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _db.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            ViewBag.Receivers = notification.notification_receiver?.Split(",").ToList();

            return View(notification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Notification notification)
        {
            if (notification.notification_receiver?.Contains("Everyone") == true)
            {
                notification.notification_receiver = "Everyone";
            }

            if (ModelState.IsValid)
            {
                _db.Update(notification);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(notification);
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

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notification = await _db.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return View(notification);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string? id)
        {
            var notification = await _db.Notification.FindAsync(id);

            if (notification == null)
            {
                return View();
            }

            _db.Notification.Remove(notification);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<string> GenerateNotificationID()
        {
            string newId;
            string prefix = "N";

            var latestNotification = await _db.Notification
                .Where(n => n.notification_type == "Normal Notification")
                .OrderByDescending(n => n.notification_id)
                .FirstOrDefaultAsync();

            if (latestNotification == null)
            {
                newId = prefix + "00001";
            }
            else
            {
                string latestId = latestNotification.notification_id;
                int numericPart = int.Parse(latestId.Substring(prefix.Length));
                newId = prefix + (numericPart + 1).ToString("D5");
            }

            return newId;
        }
    }
}
