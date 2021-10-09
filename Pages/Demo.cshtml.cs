using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CLED.Areas.Identity.Data;
using CLED.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CLED.Pages
{
    [Authorize]
    public class DemoModel : PageModel
    { 
      [BindProperty]
        public InputModel Input { get; set; }
    private HttpClient _client;

    public string message { get; set; }
    public string lang { get; set; }
    public resultJson result { get; set; }
    public List<List<string>> list1 = new List<List<string>>();
    public List<List<string>> list3 { get; set; }

        private readonly UserManager<CLEDUser> _userManager;
        private readonly CLEDContext _context;
        public class InputModel
    {
        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Language")]
        public string Language { get; set; }


    }

    public DemoModel(HttpClient client, UserManager<CLEDUser> userManager, CLEDContext context)
    {
        _client = client;
            _userManager = userManager;
            _context = context;

    }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
            CLEDUser user = await _userManager.GetUserAsync(User);

        message = Input.Message;
        lang = Input.Language;
        try
        {
             
            var response = await _client.GetAsync(" http://127.0.0.1:8000/api?seed=+" + message + "&lang=" + lang);
            var result = await response.Content.ReadAsStringAsync();
            resultJson objActualField = JsonConvert.DeserializeObject<resultJson>(result);
            foreach (var res in objActualField.data)
            {

                list1.Add(res.ToList());


            }
            list3 = list1;

            TempData["ListResults"] = System.Text.Json.JsonSerializer.Serialize(list3);
                user.usedOrnot = true;
                var add = _context.Users.Update(user);
                var save = _context.SaveChangesAsync();

                if (save.IsCompleted)
                {
                    return RedirectToPage("/demooutput");
                }
                else
                    return RedirectToPage("/demooutput");
        }
        catch (System.Exception ex)
        {
            ViewData["Error"] = " Internal Server Error Please Try Again !!! ";

            return Page();
        }



    }
}
    }
