using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CLED.Areas.Admin.ViewModels;
using CLED.Data;
using Microsoft.AspNetCore.Authorization;

namespace CLED.Areas.Admin.Controllers.Facture
{
    [Area("Admin")]
    [Authorize(Roles = ("Admin"))]
    public class FactureController : Controller
    {
        private readonly CLEDContext _context;

        public FactureController(CLEDContext context)
        {
            _context = context;
        }

        // GET: Admin/Facture
        [Route("Admin/Facture/")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Factures.ToListAsync());
        }

        // GET: Admin/Facture/Details/5

        [HttpGet]
        [Route("Admin/Facture/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factureViewModel = await _context.Factures
                .FirstOrDefaultAsync(m => m.FactureId == id);
            if (factureViewModel == null)
            {
                return NotFound();
            }

            return View(factureViewModel);
        }


    }
}
