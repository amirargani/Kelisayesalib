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
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly faEmails _faEmails = new faEmails();
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager,
            APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _userManager = userManager;
            _db = db;
            _iuser = iuser;
        }
        //stackoverflow.com/questions/39897994/how-to-clear-specific-tempdata
        [TempData]
        public string ErrorStatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsgEmail)]
            [EmailAddress(ErrorMessage = faMessage.RequiredMsgEmailAddress)]
            [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = faMessage.RequiredMsgEmailAddressExpression)]
            [Display(Name = faMessage.Email)]
            public string Email { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            TempData.Clear();
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    ErrorStatusMessage = faMessage.EmailUnknownTitle;
                    return Page();
                }
                if (user != null && user.EmailConfirmed == true)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    string codeidemailconfirmed = _codeGenerators.GetCode() + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", userId = codeidemailconfirmed, code = code, returnUrl = returnUrl },
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
                    string urlpagelink = Url.PageLink().Replace("/Identity/Account/ForgotPassword", "/").ToString();
                    _faEmails.SendEmail(Input.Email, faMessage.SubjectResstPassword, callbackUrl, faSmtp.Email, faMessage.TemplateEmail, faMessage.DescriptionResstPasswordLink, faMessage.TextResstPassword, detail.Text, faMessage.TextResstPasswordActivation, faMessage.TextActivationLink, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + pic, //faMessage.WebsiteKelisayesalib
                        _iuser.OnGetSocialNetworks(1).Facebook, _iuser.OnGetSocialNetworks(1).Twitter, _iuser.OnGetSocialNetworks(1).Instagram,
                        _iuser.OnGetSocialNetworks(1).Youtube, _iuser.OnGetSocialNetworks(1).Telegram, _iuser.OnGetSocialNetworks(1).Email,
                        _iuser.OnGetAddress(1).Street, _iuser.OnGetAddress(1).Number, _iuser.OnGetAddress(1).PostCode,
                        _iuser.OnGetAddress(1).City, _iuser.OnGetAddress(1).Country);
                    return RedirectToPage("./ForgotPasswordConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                }
                if (user != null && user.EmailConfirmed == false)
                {
                    ErrorStatusMessage = faMessage.EmailConfirmEmailActiveErrorTitle;
                    return Page();
                }
            }
            ErrorStatusMessage = faMessage.EmailUnknownTitle;
            return Page();
        }
    }
}
