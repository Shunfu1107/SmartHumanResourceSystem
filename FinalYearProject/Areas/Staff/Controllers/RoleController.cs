﻿using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalYearProject.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalYearProject.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> Index()
        {
            var basicRoles = await _db.Role.Include(r => r.Company).ToListAsync();

            return View(basicRoles);
        }

        public async Task<IActionResult> Details(string? id)
        {
            var basicRole = await _db.Role.FindAsync(id);

            if (basicRole == null) 
            { 
                return NotFound();
            }

            var rolePermission = await getRolePermission(basicRole);

            return View(rolePermission);
        }

        public async Task<IActionResult> Create()
        {
            Role role = new Role()
            {
                role_id = generateRoleId().Result,
                date_created = DateTime.Now
            };
            RolePermissionVM rolePermission = new RolePermissionVM(role);

            var companies = await _db.Company.ToListAsync();
            ViewBag.company_id = new SelectList(companies.AsEnumerable(), "company_id", "company_name");

            return View(rolePermission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolePermissionVM rolePerm)
        {
            if (ModelState.IsValid)
            {
                _db.Role.Add(rolePerm.Role);
                await _db.SaveChangesAsync();

                if (rolePerm.employeeView)
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.EmployeeView
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }

                if (rolePerm.employeeRegister)
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.EmployeeRegister
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }

                if (rolePerm.employeeManage)
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.EmployeeManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }

                if (rolePerm.roleManage)
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.RoleManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }

                if (rolePerm.shiftManage)
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.ShiftManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }

                if (rolePerm.trainingManage)
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.TrainingManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }

                if (rolePerm.payrateManage)
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.PayrateManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }

                return RedirectToAction(nameof(Index));
            }


            return View(rolePerm);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            var companies = await _db.Company.ToListAsync();
            ViewBag.company_id = new SelectList(companies.AsEnumerable(), "company_id", "company_name");

            var basicRole = await _db.Role.FindAsync(id);

            if (basicRole == null)
            {
                return NotFound();
            }
            var rolePerm = await getRolePermission(basicRole);

            return View(rolePerm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RolePermissionVM rolePerm)
        {
            if (ModelState.IsValid)
            {
                _db.Role.Update(rolePerm.Role);
                await _db.SaveChangesAsync();

                var permissions = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id).Select(p => p.identityRole).ToListAsync();

                if (rolePerm.employeeView && !permissions.Contains(SD.EmployeeView))
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.EmployeeView
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }
                else if (!rolePerm.employeeView && permissions.Contains(SD.EmployeeView))
                {
                    var delPerm = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id && p.identityRole == SD.EmployeeView).FirstOrDefaultAsync();

                    if (delPerm != null)
                    {
                        _db.Permission.Remove(delPerm);
                        await _db.SaveChangesAsync();
                        _db.Entry(delPerm).State = EntityState.Detached;
                    }
                }

                if (rolePerm.employeeRegister && !permissions.Contains(SD.EmployeeRegister))
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.EmployeeRegister
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }
                else if (!rolePerm.employeeView && permissions.Contains(SD.EmployeeRegister))
                {
                    var delPerm = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id && p.identityRole == SD.EmployeeRegister).FirstOrDefaultAsync();

                    if (delPerm != null)
                    {
                        _db.Permission.Remove(delPerm);
                        await _db.SaveChangesAsync();
                        _db.Entry(delPerm).State = EntityState.Detached;
                    }
                }

                if (rolePerm.employeeManage && !permissions.Contains(SD.EmployeeManage))
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.EmployeeManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }
                else if (!rolePerm.employeeView && permissions.Contains(SD.EmployeeManage))
                {
                    var delPerm = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id && p.identityRole == SD.EmployeeManage).FirstOrDefaultAsync();

                    if (delPerm != null)
                    {
                        _db.Permission.Remove(delPerm);
                        await _db.SaveChangesAsync();
                        _db.Entry(delPerm).State = EntityState.Detached;
                    }
                }

                if (rolePerm.roleManage && !permissions.Contains(SD.RoleManage))
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.RoleManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }
                else if (!rolePerm.employeeView && permissions.Contains(SD.RoleManage))
                {
                    var delPerm = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id && p.identityRole == SD.RoleManage).FirstOrDefaultAsync();

                    if (delPerm != null)
                    {
                        _db.Permission.Remove(delPerm);
                        await _db.SaveChangesAsync();
                        _db.Entry(delPerm).State = EntityState.Detached;
                    }
                }

                if (rolePerm.shiftManage && !permissions.Contains(SD.ShiftManage))
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.ShiftManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }
                else if (!rolePerm.employeeView && permissions.Contains(SD.ShiftManage))
                {
                    var delPerm = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id && p.identityRole == SD.ShiftManage).FirstOrDefaultAsync();

                    if (delPerm != null)
                    {
                        _db.Permission.Remove(delPerm);
                        await _db.SaveChangesAsync();
                        _db.Entry(delPerm).State = EntityState.Detached;
                    }
                }

                if (rolePerm.trainingManage && !permissions.Contains(SD.TrainingManage))
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.TrainingManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }
                else if (!rolePerm.employeeView && permissions.Contains(SD.TrainingManage))
                {
                    var delPerm = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id && p.identityRole == SD.TrainingManage).FirstOrDefaultAsync();

                    if (delPerm != null)
                    {
                        _db.Permission.Remove(delPerm);
                        await _db.SaveChangesAsync();
                        _db.Entry(delPerm).State = EntityState.Detached;
                    }
                }

                if (rolePerm.payrateManage && !permissions.Contains(SD.PayrateManage))
                {
                    Permission newPerm = new Permission()
                    {
                        baseRole = rolePerm.Role.role_id,
                        identityRole = SD.PayrateManage
                    };

                    _db.Permission.Add(newPerm);
                    await _db.SaveChangesAsync();
                    _db.Entry(newPerm).State = EntityState.Detached;
                }
                else if (!rolePerm.employeeView && permissions.Contains(SD.PayrateManage))
                {
                    var delPerm = await _db.Permission.Where(p => p.baseRole == rolePerm.Role.role_id && p.identityRole == SD.PayrateManage).FirstOrDefaultAsync();

                    if (delPerm != null)
                    {
                        _db.Permission.Remove(delPerm);
                        await _db.SaveChangesAsync();
                        _db.Entry(delPerm).State = EntityState.Detached;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(rolePerm);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baseRole = await _db.Role.FindAsync(id);

            if (baseRole == null)
            {
                return NotFound();
            }

            var rolePermission = await getRolePermission(baseRole);

            return View(rolePermission);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            var baseRole = await _db.Role.FindAsync(id);

            var assigned = await _db.EmployeeDetails.Where(e => e.staff_role == baseRole.role_id).CountAsync();

            if (assigned > 0) 
            {
                ViewBag.message = "There are still employee assigned to this role. Please reassign them to another role before deleting this role.";
            }

            if (baseRole == null)
            {
                return View();
            }

            var permissions = await _db.Permission.Where(p => p.baseRole == baseRole.role_id).ToListAsync();

            _db.Role.Remove(baseRole);
            await _db.SaveChangesAsync();

            _db.Permission.RemoveRange(permissions);
            await _db.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private async Task<RolePermissionVM> getRolePermission(Role basicRole)
        {

            var permissions = await _db.Permission.Where(p => p.baseRole == basicRole.role_id).Select(p => p.identityRole).ToListAsync();


            RolePermissionVM rolePermission = new RolePermissionVM(basicRole);

            if (permissions != null)
            {
                if (permissions.Contains(SD.EmployeeView)) 
                {
                    rolePermission.employeeView = true;
                }

                if (permissions.Contains(SD.EmployeeRegister))
                {
                    rolePermission.employeeRegister = true;
                }

                if (permissions.Contains(SD.EmployeeManage))
                {
                    rolePermission.employeeManage = true;
                }

                if (permissions.Contains(SD.RoleManage))
                {
                    rolePermission.roleManage = true;
                }

                if (permissions.Contains(SD.ShiftManage))
                {
                    rolePermission.shiftManage = true;
                }

                if (permissions.Contains(SD.TrainingManage))
                {
                    rolePermission.trainingManage = true;
                }

                if (permissions.Contains(SD.PayrateManage))
                {
                    rolePermission.payrateManage = true;
                }
            }

            return rolePermission;
        }

        private async Task<int> generatePermId()
        {
            return await _db.Permission.CountAsync();
        }

        private async Task<string> generateRoleId()
        {
            var count = await _db.Role.CountAsync();
            var prefix = "R";

            return prefix + (count + 1).ToString("00000");

        }
    }
}
