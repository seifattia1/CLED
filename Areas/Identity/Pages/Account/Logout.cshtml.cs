using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CLED.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;

namespace CLED.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<CLEDUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<CLEDUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
      

         
         
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            
              await _signInManager.SignOutAsync();
              _logger.LogInformation("User logged out.");
              if (returnUrl != null)
              {
                return RedirectToAction("Index", "Home");
            }
              else
              {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
