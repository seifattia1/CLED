using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Areas.Admin.Controllers
{
    public class UserManagerController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
