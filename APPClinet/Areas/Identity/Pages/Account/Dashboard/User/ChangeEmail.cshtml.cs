using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using APPClinet.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using APPClinet.Messages;
using APPClinet.Data;
using APPClinet.Classes;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.User
{
    [RoleAttribute("User")]
    public class ChangeEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly APPClinetDbContext _db;

        public ChangeEmailModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            APPClinetDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string ErrorStatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsgEmail)]
            [EmailAddress(ErrorMessage = faMessage.RequiredMsgEmailAddress)]
            [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = faMessage.RequiredMsgEmailAddressExpression)]
            [Display(Name = faMessage.NewEmail)]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            TempData.Clear();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
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

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                user.Email = Input.NewEmail;
                user.UserName = Input.NewEmail;
                user.NormalizedEmail = Input.NewEmail.ToUpper();
                user.NormalizedUserName = Input.NewEmail.ToUpper();
                _db.Users.Update(user);
                _db.SaveChanges();

                //StatusMessage = "Confirmation link to change email sent. Please check your email.";
                StatusMessage = faMessage.EmailTitle;
                //await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                Email = Input.NewEmail;

                Input = new InputModel
                {
                    NewEmail = Input.NewEmail
                };
                return RedirectToPage();
            }

            //StatusMessage = "Your email is unchanged.";
            ErrorStatusMessage = faMessage.EmailUnchangedTitle;
            return RedirectToPage();
        }
    }
}
