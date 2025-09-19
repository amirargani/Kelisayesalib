using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using APPClinet.Messages;
using APPClinet.Data;
using APPClinet.Classes;
using Microsoft.AspNetCore.Hosting;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Settings
{
    [RoleAttribute("Admin")]
    public class SocialNetworksModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();

        public SocialNetworksModel(APPClinetDbContext db,
            IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [EmailAddress(ErrorMessage = faMessage.RequiredMsgEmailAddress)]
            [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = faMessage.RequiredMsgEmailAddressExpression)]
            [Display(Name = faMessage.Email)]
            public string Email { get; set; }
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\s*\+?[0-9]\d{1,2}?[- .]?\d{3,4}[- .]?\d{7,8}$", ErrorMessage = faMessage.RequiredMsgInfo)]
            [Display(Name = faMessage.PhoneNumber)]
            public string PhoneNumber { get; set; }
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\s*\+?[0-9]\d{1,2}?[- .]?\d{3,4}[- .]?\d{7,8}$", ErrorMessage = faMessage.RequiredMsgInfo)]
            [Display(Name = faMessage.PrivateNumber)]
            public string PrivateNumber { get; set; }
            public string Telegram { get; set; }
            public string Youtube { get; set; }
            public string Instagram { get; set; }
            public string Twitter { get; set; }
            public string Facebook { get; set; }
            public string EmbedLinkGoogleMap { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var socialnetworks = await _db.Tbl_SocialNetworks.FirstOrDefaultAsync(c => c.Id.Equals(id));
            if (socialnetworks == null)
            {
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Settings/Index");
            }
            Input = new InputModel
            {
                Email = socialnetworks.Email,
                PhoneNumber = socialnetworks.PhoneNumber,
                PrivateNumber = socialnetworks.PrivateNumber,
                Telegram = socialnetworks.Telegram,
                Youtube = socialnetworks.Youtube,
                Instagram = socialnetworks.Instagram,
                Twitter = socialnetworks.Twitter,
                Facebook = socialnetworks.Facebook,
                EmbedLinkGoogleMap = socialnetworks.EmbedLinkGoogleMap
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid)
            {
                var socialnetworks = await _db.Tbl_SocialNetworks.FirstOrDefaultAsync(c => c.Id.Equals(id));
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                socialnetworks.Date = DateTime.Now;
                socialnetworks.Email = Input.Email;
                socialnetworks.PhoneNumber = Input.PhoneNumber;
                socialnetworks.PrivateNumber = Input.PrivateNumber;
                socialnetworks.Telegram = Input.Telegram;
                socialnetworks.Youtube = Input.Youtube;
                socialnetworks.Instagram = Input.Instagram;
                socialnetworks.Twitter = Input.Twitter;
                socialnetworks.Facebook = Input.Facebook;
                socialnetworks.EmbedLinkGoogleMap = Input.EmbedLinkGoogleMap;
                socialnetworks.UserId = user.Id;
                _db.Tbl_SocialNetworks.Update(socialnetworks);
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Settings/Index");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
