using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalYearProject.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public ReportController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await GetCurrentUserDetails();

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var reports = await _db.Report.ToListAsync();

            var filteredReports = reports.Where(n => n.report_receiver.Split(',').Select(e => e.Trim()).Contains(currentUser.email) || n.report_sender.Equals(currentUser.employee_id, StringComparison.OrdinalIgnoreCase)).OrderByDescending(n => n.report_date);

            ViewBag.CurrentUserId = currentUser.employee_id;

            return View(filteredReports);
        }

        public async Task<IActionResult> Create()
        {
            var currentUser = await GetCurrentUserDetails();
            Report newReport = new Report()
            {
                report_sender = currentUser.employee_id,
            };

            return View(newReport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Report report, IList<IFormFile> attachments)
        {
            if (attachments != null && attachments.Any())
            {
                // Defined 10MB of file size allowed to upload
                const long maxFileSize = 10 * 1024 * 1024;

                foreach (var file in attachments)
                {
                    if (file.Length > maxFileSize)
                    {
                        ModelState.AddModelError("Attachments", $"The file '{file.FileName}' exceeds the 10MB size limit.");
                        return View(report);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                // Get the current user details
                var currentUser = await GetCurrentUserDetails();

                // Split the receiver emails into a list
                var receivers = report.report_receiver.Split(',').Select(e => e.Trim()).ToList();

                foreach (var receiverEmail in receivers)
                {
                    // Create a new report for each receiver
                    var newReport = new Report
                    {
                        report_id = await GenerateReportID(),
                        report_title = report.report_title,
                        report_content = report.report_content,
                        report_sender = currentUser.employee_id,
                        report_receiver = receiverEmail,
                        report_date = DateTime.Now,
                        report_status = "Pending"
                    };

                    _db.Report.Add(newReport);
                    await _db.SaveChangesAsync();

                    // Handle attachments for each report
                    if (attachments != null && attachments.Any())
                    {
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reportAttachments");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        var count = 1;
                        foreach (var file in attachments)
                        {
                            if (file.Length > 0)
                            {
                                // Generate unique filename
                                string uniqueFileName = newReport.report_id + "_AT" + count + $"_{Path.GetFileName(file.FileName)}";
                                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                                // Save file to disk
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }

                                // Create attachment record
                                var attachment = new Attachment
                                {
                                    attachment_id = newReport.report_id + "AT" + count,
                                    report_id = newReport.report_id,
                                    file_name = file.FileName,
                                    file_path = $"/reportAttachments/{uniqueFileName}",
                                    upload_date = DateTime.Now
                                };

                                count++;
                                _db.Attachment.Add(attachment);
                            }
                        }
                        await _db.SaveChangesAsync();
                    }

                    // Add a notification for each receiver
                    var receiver = await _db.EmployeeDetails.FirstOrDefaultAsync(e => e.email == receiverEmail);
                    if (receiver != null)
                    {
                        var notification = new Notification
                        {
                            notification_id = "N" + currentUser.employee_id + newReport.report_id + receiver.employee_id,
                            notification_receiver = receiver.email,
                            notification_title = "New Report Received",
                            notification_content = $"You have received a new report titled '{newReport.report_title}' from {newReport.report_sender}. " +
                                                   $"<a href='/Staff/Report/Details/{newReport.report_id}'>Click here to view the report.</a>",
                            notification_date = DateTime.Now,
                            notification_type = "New Report Notification"
                        };

                        _db.Notification.Add(notification);
                        await _db.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _db.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUserDetails();

            var feedbacks = await _db.ReportFeedback
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.feedback_date)
                .ToListAsync();

            var attachments = await _db.Attachment
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.upload_date)
                .ToListAsync();

            var viewModel = new ReportFeedbackVM
            {
                Report = report,
                Feedbacks = feedbacks,
                CurrentUserId = currentUser.email,
                Attachments = attachments
            };

            ViewBag.Receivers = report.report_receiver?.Split(",").ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Report report, IList<IFormFile> attachments)
        {
            if (ModelState.IsValid)
            {
                report.report_status = "Edited";
                _db.Update(report);
                await _db.SaveChangesAsync();

                // Handle attachments for each report
                if (attachments != null && attachments.Any())
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reportAttachments");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var count = 1;
                    foreach (var file in attachments)
                    {
                        if (file.Length > 0)
                        {
                            // Generate unique filename
                            string uniqueFileName = report.report_id + "_AT" + count + $"_{Path.GetFileName(file.FileName)}";
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            // Save file to disk
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            // Create attachment record
                            var attachment = new Attachment
                            {
                                attachment_id = await GenerateAttachmentID(report.report_id),
                                report_id = report.report_id,
                                file_name = file.FileName,
                                file_path = $"/reportAttachments/{uniqueFileName}",
                                upload_date = DateTime.Now
                            };

                            count++;
                            _db.Attachment.Add(attachment);
                            await _db.SaveChangesAsync();
                        }
                    }
                    
                }

                return RedirectToAction(nameof(Index));
            }

            var currentUser = await GetCurrentUserDetails();

            var feedbacks = await _db.ReportFeedback
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.feedback_date)
                .ToListAsync();

            var reportAttachments = await _db.Attachment
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.upload_date)
                .ToListAsync();

            var viewModel = new ReportFeedbackVM
            {
                Report = report,
                Feedbacks = feedbacks,
                CurrentUserId = currentUser.email,
                Attachments = reportAttachments
            };

            ViewBag.Receivers = report.report_receiver?.Split(",").ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Details(string? id)
        {
            var report = await _db.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUserDetails();

            if (report.report_status == "Pending" && currentUser.email == report.report_receiver)
            {
                report.report_status = "Seen";
                await _db.SaveChangesAsync();
            }

            var feedbacks = await _db.ReportFeedback
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.feedback_date)
                .ToListAsync();

            var attachments = await _db.Attachment
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.upload_date)
                .ToListAsync();

            var viewModel = new ReportFeedbackVM
            {
                Report = report,
                Feedbacks = feedbacks,
                CurrentUserId = currentUser.email,
                Attachments = attachments
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _db.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUserDetails();

            var feedbacks = await _db.ReportFeedback
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.feedback_date)
                .ToListAsync();

            var attachments = await _db.Attachment
                .Where(f => f.report_id == report.report_id)
                .OrderByDescending(f => f.upload_date)
                .ToListAsync();

            var viewModel = new ReportFeedbackVM
            {
                Report = report,
                Feedbacks = feedbacks,
                CurrentUserId = currentUser.email,
                Attachments = attachments
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(string? id)
        {
            var report = await _db.Report.FindAsync(id);

            if (report == null)
            {
                return View();
            }

            report.report_status = "Deleted";

            _db.Report.Update(report);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _db.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            // Update report status to Approved
            report.report_status = "Approved";
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<string> GenerateReportID()
        {
            string newId;
            string prefix = "RE";

            var latestReport = await _db.Report
                .OrderByDescending(n => n.report_id)
                .FirstOrDefaultAsync();

            if (latestReport == null)
            {
                newId = prefix + "00001";
            }
            else
            {
                string latestId = latestReport.report_id;
                int numericPart = int.Parse(latestId.Substring(prefix.Length));

                newId = prefix + (numericPart + 1).ToString("D5");
            }

            return newId;
        }

        private async Task<string> GenerateAttachmentID(string? reportId)
        {

            string newId;
            string prefix = reportId + "AT";

            var latestAttachments = await _db.Attachment.Where(n => n.report_id == reportId).OrderByDescending(n => n.attachment_id).FirstOrDefaultAsync();

            if (latestAttachments == null)
            {
                newId = prefix + "1";
            }
            else
            {
                string latestId = latestAttachments.attachment_id;
                int numericPart = int.Parse(latestId.Substring(prefix.Length));

                newId = prefix + (numericPart + 1).ToString("D1");
            }

            return newId;
        }

        public async Task<IActionResult> DownloadAttachment(string id)
        {
            var attachment = await _db.Attachment.FindAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", attachment.file_path.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), attachment.file_name);
        }

        [HttpGet]
        public IActionResult DeleteAttachment(string id)
        {
            var attachment = _db.Attachment.Find(id);
            if (attachment == null)
                return Json(new { success = false, message = "Attachment not found." });

            // Remove from the database
            _db.Attachment.Remove(attachment);
            _db.SaveChanges();

            // Remove from filesystem
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", attachment.file_path.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToAction("Edit", new { id = attachment.report_id });
        }

        private string GetContentType(string filePath)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        private async Task<EmployeeDetails> GetCurrentUserDetails()
        {
            var aspId = User.Identity?.Name;

            if (aspId == null)
            {
                return null;
            }

            // Querying the database once to get user details
            var currentUser = await _db.EmployeeDetails.Where(e => e.user_id == aspId).FirstOrDefaultAsync();

            return currentUser;
        }
    }
}
