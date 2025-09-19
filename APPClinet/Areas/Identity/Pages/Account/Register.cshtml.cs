using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
//using System.Text.Encodings.Web; // Disable
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using APPClinet.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services; // Disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using APPClinet.Data;
using APPClinet.Classes;
using APPClinet.Messages;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender; // Disable
        private readonly faEmails _faEmails = new faEmails();
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            //IEmailSender emailSender,
            APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender; // Disable
            _db = db;
            _iuser = iuser;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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

            [DataType(DataType.Password)]
            [Display(Name = faMessage.ConfirmPassword)]
            [Compare("Password", ErrorMessage = faMessage.RequiredMsgPasswordCompare)]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Add
                if (_iuser.ExistsRoleName(User.Identity.Name) == "User")
                {
                    Response.Redirect("./Dashboard/User/Index");
                }
                if (_iuser.ExistsRoleName(User.Identity.Name) == "Admin")
                {
                    Response.Redirect("./Dashboard/Admin/Index");
                }
                // Add
            }
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Date = DateTime.Now, IDEmailConfirmed = _codeGenerators.GetCode() + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second, DateEmailConfirmed = DateTime.Now, DateActiveCode = DateTime.Now };
                var quserfind = _userManager.Users.Where(a => a.Email.Equals(user.Email)).SingleOrDefault();
                if (quserfind != null)
                    return RedirectToPage("RegistrationError", new { email = Input.Email, returnUrl = returnUrl });
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.IDEmailConfirmed, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    var subcategories = _db.Tbl_ChurchSubCategories.FirstOrDefault(c => c.Id.Equals(1));
                    var categories = _db.Tbl_ChurchCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
                    var detail = _db.Tbl_ChurchDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
                    string pic = null;
                    if (detail.Image != null) { pic = "uploads/images/details/church/" + detail.Image; } else { pic = "uploads/images/details/default/default.jpg"; }
                    string urlpagelink = Url.PageLink().Replace("/Identity/Account/Register", "/").ToString();
                    _faEmails.SendEmail(Input.Email, faMessage.SubjectConfirmEmail, callbackUrl, faSmtp.Email, faMessage.TemplateEmail, faMessage.DescriptionWelcome, faMessage.TextConfirmEmail, detail.Text, faMessage.TextAccountActivation, faMessage.TextActivationLink, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + pic, //faMessage.WebsiteKelisayesalib
                        _iuser.OnGetSocialNetworks(1).Facebook, _iuser.OnGetSocialNetworks(1).Twitter, _iuser.OnGetSocialNetworks(1).Instagram,
                        _iuser.OnGetSocialNetworks(1).Youtube, _iuser.OnGetSocialNetworks(1).Telegram, _iuser.OnGetSocialNetworks(1).Email,
                        _iuser.OnGetAddress(1).Street, _iuser.OnGetAddress(1).Number, _iuser.OnGetAddress(1).PostCode,
                        _iuser.OnGetAddress(1).City, _iuser.OnGetAddress(1).Country);
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
