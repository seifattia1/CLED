using CLED.Areas.Admin.ViewModels;
using CLED.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Admin.Controllers.Manager
{
    [Area("admin")]
    [Authorize(Roles =("Admin"))]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [Route("/admin/manage/role")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/manage/roleAdd")]
        public async Task<IActionResult> Add(AdministrationCreateRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            var roleIsExists = await _roleManager.RoleExistsAsync(model.Name);
            if (roleIsExists)
            {
                ModelState.AddModelError("Name", "Role is exists!");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }
            await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
            
            return RedirectToAction(nameof(this.Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/manage/role/")]
        public async Task<IActionResult> Index(string Id)
        {
            var roleIsExists = await _roleManager.RoleExistsAsync(Id);
            if (roleIsExists)
            {
                return View("Index", await _roleManager.Roles.ToListAsync());
            }
           
            await _roleManager.DeleteAsync(_roleManager.FindByIdAsync(Id).Result);

            return RedirectToAction(nameof(this.Index));
        }
    }
}
