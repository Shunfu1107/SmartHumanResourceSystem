using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Utility;
using Microsoft.AspNetCore.Authorization;

namespace FinalYearProject.Areas.Staff.Controllers
{
	[Area("Staff")]
	//[Authorize(Roles = SD.CompanyKPIs)]
	public class CompanyKPIsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public CompanyKPIsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Admin/CompanyKPIs
		public async Task<IActionResult> Index()
		{
			var aspId = User.Identity?.Name;

			if (aspId == null)
			{
				return RedirectToAction("Login", "Account", new { area = "Identity" });
			}
			//else
			//{
			//    var currentUser = await _context.EmployeeDetails.Where(e => e.user_id == aspId).FirstOrDefaultAsync();

			//    if (currentUser == null)
			//    {
			//        return RedirectToAction("Login", "Account", new { area = "Identity" });
			//    }
			//    else
			//    {
			//        var applicationDbContext = await _context.CompanyKPIs.Where(cp => cp.company_id == currentUser.parent_company).ToListAsync();
			//        return View(applicationDbContext);
			//    }
			//}

			var company = await _context.Company.Where(e => e.current_admin == aspId).FirstOrDefaultAsync();

			var applicationDbContext = await _context.CompanyKPIs.Where(cp => cp.company_id == company.company_id).ToListAsync();
			return View(applicationDbContext);
		}

		// GET: Admin/CompanyKPIs/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null || _context.CompanyKPIs == null)
			{
				return NotFound();
			}

			var companyKPI = await _context.CompanyKPIs
				.FirstOrDefaultAsync(m => m.KPI_id == id);
			if (companyKPI == null)
			{
				return NotFound();
			}

			return View(companyKPI);
		}

		// GET: Admin/CompanyKPIs/Create
		public async Task<IActionResult> Create()
		{
			var aspId = User.Identity?.Name;

			if (aspId == null)
			{
				return RedirectToAction("Login", "Account", new { area = "Identity" });
			}
			else
			{
				var currentUser = await _context.EmployeeDetails.Where(e => e.user_id == aspId).FirstOrDefaultAsync();

				if (currentUser == null)
				{
					var currentAdmin = await _context.Admin.Where(a => a.admin_id == aspId).FirstOrDefaultAsync();

					if (currentAdmin == null)
					{
						return RedirectToAction("Login", "Account", new { area = "Identity" });
					}
					else
					{
						var company = await _context.Company.Where(a => a.current_admin == aspId).FirstOrDefaultAsync();
						CompanyKPI kpi = new CompanyKPI()
						{
							KPI_id = await GenerateCompanyKPIID(),
							company_id = company.company_id
						};
						ViewBag.StatusOptions = new List<string> { "Pending", "Achieve", "Fail" };
						return View(kpi);
						//return View(await _context.CompanyKPIs.ToListAsync());
					}
				}
				else
				{
					CompanyKPI kpi = new CompanyKPI()
					{
						KPI_id = await GenerateCompanyKPIID(),
						company_id = currentUser.parent_company
					};
					ViewBag.StatusOptions = new List<string> { "Pending", "Achieve", "Fail" };
					return View(kpi);
				}
			}
		}

		// POST: Admin/CompanyKPIs/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CompanyKPI companyKPI)
		{
			// Inline validation for numbers
			if (companyKPI.target_KPI == null || companyKPI.target_KPI < 0)
			{
				ModelState.AddModelError("target_KPI", "Target KPI must be a valid number greater than or equal to 0.");
			}

			if (companyKPI.hit_KPI_allowance == null || companyKPI.hit_KPI_allowance < 0)
			{
				ModelState.AddModelError("hit_KPI_allowance", "Hit KPI Allowance must be a valid number greater than or equal to 0.");
			}

			// Inline validation for dates
			if (companyKPI.start_date == null)
			{
				ModelState.AddModelError("start_date", "Start date is required.");
			}

			if (companyKPI.end_date == null)
			{
				ModelState.AddModelError("end_date", "End date is required.");
			}
			else if (companyKPI.end_date < companyKPI.start_date)
			{
				ModelState.AddModelError("end_date", "End date must be later than the start date.");
			}

			// Check if there are any validation errors
			if (!ModelState.IsValid)
			{
				ViewBag.StatusOptions = new List<string> { "Pending", "Achieve", "Fail" };
				return View(companyKPI);
			}

			_context.Add(companyKPI);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}


		// GET: Admin/CompanyKPIs/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null || _context.CompanyKPIs == null)
			{
				return NotFound();
			}

			var companyKPI = await _context.CompanyKPIs.FindAsync(id);
			if (companyKPI == null)
			{
				return NotFound();
			}
			ViewBag.StatusOptions = new List<string> { "Pending", "Achieve", "Fail" };
			return View(companyKPI);
		}

		// POST: Admin/CompanyKPIs/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, CompanyKPI companyKPI)
		{
			if (id != companyKPI.KPI_id)
			{
				return NotFound();
			}

			if (companyKPI.end_date < companyKPI.start_date)
			{
				ViewData["ErrorMessageDate"] = "Start date must be earlier than end date";
				ViewBag.StatusOptions = new List<string> { "Pending", "Achieve", "Fail" };
				return View(companyKPI);
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(companyKPI);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CompanyKPIExists(companyKPI.KPI_id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewBag.StatusOptions = new List<string> { "Pending", "Achieve", "Fail" };
			return View(companyKPI);
		}

		// GET: Admin/CompanyKPIs/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null || _context.CompanyKPIs == null)
			{
				return NotFound();
			}

			var companyKPI = await _context.CompanyKPIs
				.FirstOrDefaultAsync(m => m.KPI_id == id);
			if (companyKPI == null)
			{
				return NotFound();
			}

			return View(companyKPI);
		}

		// POST: Admin/CompanyKPIs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			if (_context.CompanyKPIs == null)
			{
				return Problem("Entity set 'ApplicationDbContext.CompanyKPIs'  is null.");
			}
			var companyKPI = await _context.CompanyKPIs.FindAsync(id);
			if (companyKPI != null)
			{
				_context.CompanyKPIs.Remove(companyKPI);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CompanyKPIExists(string id)
		{
			return (_context.CompanyKPIs?.Any(e => e.KPI_id == id)).GetValueOrDefault();
		}

		public async Task<string> GenerateCompanyKPIID()
		{
			string newId;
			string prefix = "CKPI";

			var ckpi = await _context.CompanyKPIs
			 .OrderByDescending(kpi => kpi.KPI_id)
			 .FirstOrDefaultAsync();

			if (ckpi != null)
			{
				int lastIdNumericPart = int.Parse(ckpi.KPI_id.Substring(4));
				newId = prefix + (lastIdNumericPart + 1).ToString("00000");
			}
			else
			{
				newId = prefix + "00001";
			}

			return newId;
		}
	}
}