using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/supportclient")]
        public IActionResult ChatBox()
        {
            return View();
        }

 

    }
}
