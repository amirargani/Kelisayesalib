using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using APPClinet.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using APPClinet.Messages;

namespace APPClinet.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmationModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForgotPasswordConfirmationModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl)
        {
            TempData.Clear();
            if (email == null || returnUrl == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }

            var user = _userManager.Users.Where(a => a.Email.Equals(email)).SingleOrDefault();
            if (user == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }

            if (user.EmailConfirmed == true)
            {
                var result = await _userManager.IsEmailConfirmedAsync(user);
                StatusMessage = result ? faMessage.EmailRegistrationActiveTitle : faMessage.EmailRegistrationActiveErrorTitle;
                return RedirectToPage("./Login");
            }
            else
            {
                StatusMessage = null;
                return Page();
            }
        }
    }
}
