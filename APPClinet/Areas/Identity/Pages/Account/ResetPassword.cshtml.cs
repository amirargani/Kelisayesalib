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
using APPClinet.Classes;
using APPClinet.Data;
using APPClinet.Messages;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly faEmails _faEmails = new faEmails();
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager,
            APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _userManager = userManager;
            _db = db;
            _iuser = iuser;

        }

        public string Email { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsgPassword)]
            [StringLength(100, ErrorMessage = faMessage.RequiredMsgPasswordStringLength, MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = faMessage.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = faMessage.ConfirmPassword)]
            [Compare("Password", ErrorMessage = faMessage.RequiredMsgPasswordCompare)]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }

            public string UserId { get; set; }

            public string Link { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Add
                _iuser.GetLogout();
                // Add
            }
            if (userId == null || code == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }

            var user = _userManager.Users.Where(a => a.IDEmailConfirmed.Equals(userId)).SingleOrDefault();
            if (user == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            if (user.DateEmailConfirmed.AddDays(7) < DateTime.Today)
            {
                if (user.EmailConfirmed == false)
                {
                    string returnUrl = Url.Content("~/");
                    var qcode = await _userManager.GeneratePasswordResetTokenAsync(user);
                    qcode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(qcode));
                    string codeidemailconfirmed = _codeGenerators.GetCode() + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", userId = codeidemailconfirmed, code = qcode, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    user.IDEmailConfirmed = codeidemailconfirmed;
                    user.EmailConfirmed = false;
                    user.DateEmailConfirmed = DateTime.Now;
                    user.DateActiveCode = DateTime.Now;
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    var subcategories = _db.Tbl_ChurchSubCategories.FirstOrDefault(c => c.Id.Equals(1));
                    var categories = _db.Tbl_ChurchCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
                    var detail = _db.Tbl_ChurchDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
                    string pic = null;
                    if (detail.Image != null) { pic = "uploads/images/details/church/" + detail.Image; } else { pic = "uploads/images/details/default/default.jpg"; }
                    string urlpagelink = Url.PageLink().Replace("/Identity/Account/ResetPassword", "/").ToString();
                    _faEmails.SendEmail(user.Email, faMessage.SubjectResstPasswordNewLink, callbackUrl, faSmtp.Email, faMessage.TemplateEmail, faMessage.DescriptionResstPasswordLink, faMessage.TextResstPassword, detail.Text, faMessage.TextResstPasswordActivation, faMessage.TextActivationLink, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + pic, //faMessage.WebsiteKelisayesalib
                        _iuser.OnGetSocialNetworks(1).Facebook, _iuser.OnGetSocialNetworks(1).Twitter, _iuser.OnGetSocialNetworks(1).Instagram,
                        _iuser.OnGetSocialNetworks(1).Youtube, _iuser.OnGetSocialNetworks(1).Telegram, _iuser.OnGetSocialNetworks(1).Email,
                        _iuser.OnGetAddress(1).Street, _iuser.OnGetAddress(1).Number, _iuser.OnGetAddress(1).PostCode,
                        _iuser.OnGetAddress(1).City, _iuser.OnGetAddress(1).Country);
                    return RedirectToPage("./ForgotPasswordConfirmation", new { email = user.Email, returnUrl = returnUrl });
                }
            }

            if (user.EmailConfirmed == false)
            {
                Email = user.Email;
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)),
                    UserId = userId,
                    Link = code
                };
                return Page();
            }
            else
            {
                return RedirectToPage("./Login");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Add
                _iuser.GetLogout();
                // Add
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _db.Users.FirstOrDefault(u => u.IDEmailConfirmed.Equals(Input.UserId));
            if (user == null)
            {
                string returnUrl = Url.Content("~/");
                return RedirectToPage("./ResetPassword", new { userId = Input.UserId, code = Input.Link, returnUrl = returnUrl });
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                user.IDEmailConfirmed = _codeGenerators.GetCode() + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                user.EmailConfirmed = true;
                user.DateEmailConfirmed = DateTime.Now;
                user.DateActiveCode = DateTime.Now;
                _db.Users.Update(user);
                _db.SaveChanges();
                return RedirectToPage("./Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            Email = user.Email;
            return Page();
        }
    }
}
