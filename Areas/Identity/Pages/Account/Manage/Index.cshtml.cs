using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CLED.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CLED.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<CLEDUser> _userManager;
        private readonly SignInManager<CLEDUser> _signInManager;

        public IndexModel(
            UserManager<CLEDUser> userManager,
            SignInManager<CLEDUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [PersonalData]
            [Display(Name = "FullName")]
            public string FullName { get; set; }

            [Display(Name = "Country")]
            public string Country { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(CLEDUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var fullname = user.FullName;
            var Country = user.coutry;
            Username = userName;

            Input = new InputModel
            {

                FullName = fullname,
                Country = Country,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            
            if (Input.PhoneNumber != phoneNumber || Input.Country != Request.Form["country-select"].ToString() || Input.FullName != user.FullName )
            {
                Input.Country = Request.Form["country-select"].ToString();
                user.FullName = Input.FullName;
                user.coutry = Input.Country.ToString();
                user.PhoneNumber = Input.PhoneNumber;
                var setPhoneResult = await _userManager.UpdateAsync(user);
                if (!setPhoneResult.Succeeded)
                {

                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
                else
                {
                    await _signInManager.RefreshSignInAsync(user);
                    StatusMessage = "Your profile has been updated";
                   
                }
            }

            return RedirectToPage();
        }
    }
}
