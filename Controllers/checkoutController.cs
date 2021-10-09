using CLED.Areas.Admin.ViewModels;
using CLED.Areas.Identity.Data;
using CLED.Data;
using CLED.Models;
using CLED.Payment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Controllers
{
    [Route("/checkout")]
    public class CheckoutController : Controller
    {
        private readonly CLEDContext _context;

        private readonly ILogger<CheckoutController> _logger;
        private readonly UserManager<CLEDUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CheckoutController(ILogger<CheckoutController> logger, CLEDContext context, UserManager<CLEDUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [Route("/checkout/success")]
        public IActionResult Success()
        {
            return View();
        }

        [Route("/checkout/pay")]
        public async Task<dynamic> Pay(PayModel payModel)
        {
            if (ModelState.IsValid)
            {
                Facture result = await ProcessPayment.PayAsync(payModel);

                if (result.FactureStatus == "paid")
                {
                    var success = await _context.Factures.AddAsync(result);
                   CLEDUser userPremium =  await _userManager.FindByNameAsync(result.userName);
                    
                    if (success == null)
                    {
                    
                        return RedirectToAction("Error");
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(userPremium, "FreeUser");
                        await _userManager.AddToRoleAsync(userPremium, "PremiumUser");
                        var a = await _context.SaveChangesAsync();
                        return RedirectToAction("Success");
                    }
                    
                }
                else
                {
                    return RedirectToAction("Error", result);
                }
            }

            return RedirectToAction("Index");
        }

       
    }
}
