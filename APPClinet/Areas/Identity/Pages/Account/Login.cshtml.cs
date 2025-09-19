using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
//using System.Text.Encodings.Web; // Disable
using Microsoft.AspNetCore.Authorization;
using APPClinet.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using APPClinet.Messages;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IApplicationUser _iuser;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IApplicationUser iuser)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _iuser = iuser;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsgEmail)]
            [EmailAddress(ErrorMessage = faMessage.RequiredMsgEmailAddress)]
            [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = faMessage.RequiredMsgEmailAddressExpression)]
            [Display(Name = faMessage.Email)]
            public string Email { get; set; }

            [Required(ErrorMessage = faMessage.RequiredMsgPassword)]
            [StringLength(100, ErrorMessage = faMessage.RequiredMsgPasswordStringLength, MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = faMessage.Password)]
            public string Password { get; set; }

            [Display(Name = faMessage.RememberMe)]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            TempData.Clear();
            if (User.Identity.IsAuthenticated)
            {
                // Add
                if (_iuser.ExistsRoleName(User.Identity.Name) == "User")
                {
                    //Response.Redirect("./Manage/Index");
                    Response.Redirect("./Dashboard/User/Index");
                }
                if (_iuser.ExistsRoleName(User.Identity.Name) == "Admin")
                {
                    Response.Redirect("./Dashboard/Admin/Index");
                }
                // Add
            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                // Add
                var user = _userManager.Users.Where(a => a.UserName.Equals(Input.Email)).SingleOrDefault();
                if (user == null)
                {
                    ErrorMessage = faMessage.EmailUnknownTitle;
                    return Page();
                }
                // Add
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //return LocalRedirect(returnUrl); // Disable
                    // Add
                    if (_iuser.ExistsRoleName(user.UserName) == "User")
                    {
                        //Response.Redirect("./Manage/Index");
                        Response.Redirect("./Dashboard/User/Index");
                    }
                    if (_iuser.ExistsRoleName(user.UserName) == "Admin")
                    {
                        Response.Redirect("./Dashboard/Admin/Index");
                    }
                    // Add
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    if (user.EmailConfirmed == true && result.Succeeded == false)
                    {
                        ErrorMessage = faMessage.EmailLoginPasswordErrorTitle;
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                    if (user.EmailConfirmed == false && result.Succeeded == true)
                    {
                        ErrorMessage = faMessage.EmailConfirmEmailActiveErrorTitle;
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                    if (user.EmailConfirmed == false && result.Succeeded == false)
                    {
                        ErrorMessage = faMessage.EmailConfirmEmailActiveErrorTitle;
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
