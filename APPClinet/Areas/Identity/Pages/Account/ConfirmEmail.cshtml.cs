using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ConfirmEmailModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly APPClinetDbContext _db;
        private readonly faEmails _faEmails = new faEmails();
        private readonly IApplicationUser _iuser;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _iuser = iuser;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            TempData.Clear();
            if (userId == null || code == null)
            {
                //return RedirectToPage("/Index");
                return Redirect(Url.Content("~/" + "Error"));
            }

            //var user = await _userManager.FindByIdAsync(userId);
            var user = _userManager.Users.Where(a => a.IDEmailConfirmed.Equals(userId)).SingleOrDefault();
            if (user == null)
            {
                //return NotFound($"Unable to load user with ID '{userId}'.");
                //Identity/Account/ConfirmEmail?userId=
                return Redirect(Url.Content("~/" + "Error"));
            }
            if (user.Date.AddDays(7) < DateTime.Today)
            {
                if (user.EmailConfirmed == false)
                {
                    string returnUrl = Url.Content("~/");
                    var qcode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    qcode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(qcode));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.IDEmailConfirmed, code = qcode, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    user.Date = DateTime.Now;
                    user.DateEmailConfirmed = DateTime.Now;
                    user.DateActiveCode = DateTime.Now;
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    var subcategories = _db.Tbl_ChurchSubCategories.FirstOrDefault(c => c.Id.Equals(1));
                    var categories = _db.Tbl_ChurchCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
                    var detail = _db.Tbl_ChurchDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
                    string pic = null;
                    if (detail.Image != null) { pic = "uploads/images/details/church/" + detail.Image; } else { pic = "uploads/images/details/default/default.jpg"; }
                    string urlpagelink = Url.PageLink().Replace("/Identity/Account/ConfirmEmail", "/").ToString();
                    _faEmails.SendEmail(user.Email, faMessage.SubjectConfirmEmailActivationLink, callbackUrl, faSmtp.Email, faMessage.TemplateEmail, faMessage.DescriptionWelcome, faMessage.TextConfirmEmailActivationLink, detail.Text, faMessage.TextAccountActivation, faMessage.TextActivationLink, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + pic, //faMessage.WebsiteKelisayesalib
                        _iuser.OnGetSocialNetworks(1).Facebook, _iuser.OnGetSocialNetworks(1).Twitter, _iuser.OnGetSocialNetworks(1).Instagram,
                        _iuser.OnGetSocialNetworks(1).Youtube, _iuser.OnGetSocialNetworks(1).Telegram, _iuser.OnGetSocialNetworks(1).Email,
                        _iuser.OnGetAddress(1).Street, _iuser.OnGetAddress(1).Number, _iuser.OnGetAddress(1).PostCode,
                        _iuser.OnGetAddress(1).City, _iuser.OnGetAddress(1).Country);
                    return RedirectToPage("RegisterConfirmation", new { email = user.Email, returnUrl = returnUrl });
                }
            }

            if (user.EmailConfirmed == false)
            {
                await _userManager.AddToRoleAsync(user, "User");
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                //StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
                StatusMessage = result.Succeeded ? faMessage.EmailConfirmEmailTitle : faMessage.EmailConfirmEmailErrorTitle;
                // Add
                user.Newsletter = true;
                _db.Users.Update(user);
                _db.SaveChanges();
                // Add
                return Page();
            }
            else
            {
                return RedirectToPage("VerificationEmail", new { userId = userId, code = code });
            }

            //code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            //var result = await _userManager.ConfirmEmailAsync(user, code);
            //StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            //return Page();
        }
    }
}
