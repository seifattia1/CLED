using CLED.Areas.Admin.ViewModels;
using CLED.Areas.Identity.Data;
using CLED.Constants;
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
    [Authorize(Roles = ("Admin"))]
    public class UsersController : Controller
    {
        private readonly UserManager<CLEDUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        
        public UsersController(UserManager<CLEDUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

      
        [Route("/admin/manage/Users")]
        public async Task<IActionResult> Index()
        {
            
            var user = await _userManager.Users.Select(user => new UserClassView
            { 
                Id= user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result
            }
            ).ToListAsync();
            
            
            
            return View(user);
        }

        [HttpGet]
        [Route("/admin/manage/Users/editRole")]
        public async Task<IActionResult> ManageRoles(string userId)
        {
          
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound();
            }
            var roles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRoleViewModel
            {
                UserId = user.Id,
                Email=user.Email,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user,role.Name).Result
                }).ToList()
                
                
            };
            return View(viewModel);
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/manage/Users/editRole")]
        public async Task<IActionResult> ManageRoles(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }


            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = await _roleManager.Roles.ToListAsync();
         

            foreach (var role in model.Roles)
            {
                                    
                    if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
                    {
                        
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/manage/Users/")]
        public async Task<IActionResult> Index(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

           var delete = await _userManager.DeleteAsync(user);

            if (delete.Succeeded)
            {
              return  RedirectToAction(nameof(this.Index));

            }

          
            return RedirectToAction(nameof(this.Index));
        }
    }
}
