using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Controllers
{
    [Authorize(Roles = ("PremiumUser,Admin"))]
    public class ChatBoxController1 : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        [Route("support")]
        public IActionResult ChatBox()
        {
            return View();
        }
    }
}
