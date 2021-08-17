using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace CLED.Pages
{
    [Authorize]
    public class ProblemeInputModel : PageModel
    {

        [BindProperty]
        public InputModel Input { get; set; }
        private HttpClient _client;

        public string message { get; set;}
        public string lang { get; set; }
        public resultJson result { get; set; }
        public List<List<string>> list1 = new List<List<string>>();
        public List<List<string>> list3 { get; set; }


        public class InputModel
        {
            [Required]
            [Display(Name = "Message")]
            public string Message { get; set; }

            [Required]
            [Display(Name = "Language")]
            public string Language { get; set; }

           
        }

        public ProblemeInputModel(HttpClient client)
        {
            _client = client;
          
        }
        public string[] k { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            message = Input.Message;
            lang = Input.Language;

            var response = await _client.GetAsync(" http://127.0.0.1:8000/api?seed=+" + message + "&lang=" + lang);
            var result = await response.Content.ReadAsStringAsync();
            resultJson objActualField = JsonConvert.DeserializeObject<resultJson>(result);
            foreach (var res in objActualField.data) {

                list1.Add(res.ToList());

               
            }
            list3 = list1;

            TempData["ListResults"]= System.Text.Json.JsonSerializer.Serialize(list3);

            return RedirectToPage("/problemoutput") ;

        
        }
    }
}
