using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CLED.Pages
{
    [Authorize]
    public class DemoOutputModel : PageModel
    {


        [BindProperty(Name = "list7", SupportsGet = true)]
        public List<List<string>> List7 { get; set; }

        int a { get; set; }

        

        [BindProperty]
        public string DrawingContent { get; set; }
        public IActionResult OnGet()
        {
            var data = TempData["ListResults"] as string;
            List7 = JsonSerializer.Deserialize<List<List<string>>>(data);
            return Page();
        }

        public IActionResult OnPostSave()
        {
            var drawingJson = DrawingContent;
            return new JsonResult(new { Valid = true });
        }

    }
}
