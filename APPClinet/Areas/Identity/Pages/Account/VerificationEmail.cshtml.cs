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
    public class VerificationEmailModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public VerificationEmailModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            TempData.Clear();
            if (userId == null || code == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }

            var user = _userManager.Users.Where(a => a.IDEmailConfirmed.Equals(userId)).SingleOrDefault();
            if (user == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }

            if (user.EmailConfirmed == true)
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                StatusMessage = result.Succeeded ? faMessage.EmailRegistrationActiveTitle : faMessage.EmailRegistrationActiveErrorTitle;
                return Page();
            }
            else
            {
                return RedirectToPage("ConfirmEmail", new { userId = userId, code = code });
            }
        }
    }
}
