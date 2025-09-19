using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using APPClinet.Areas.Identity.Data;
using APPClinet.Classes;
using APPClinet.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin
{
    [RoleAttribute("Admin")]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsgPassword)]
            [StringLength(100, ErrorMessage = faMessage.RequiredMsgPasswordStringLength, MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = faMessage.CurrentPassword)]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = faMessage.RequiredMsgPassword)]
            [StringLength(100, ErrorMessage = faMessage.RequiredMsgPasswordStringLength, MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = faMessage.NewPassword)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = faMessage.NewConfirmPassword)]
            [Compare("NewPassword", ErrorMessage = faMessage.RequiredMsgNewPasswordCompare)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            TempData.Clear();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            //StatusMessage = "Your password has been changed.";
            StatusMessage = faMessage.PasswordTitle;

            return RedirectToPage();
        }
    }
}
