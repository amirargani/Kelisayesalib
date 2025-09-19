using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using APPClinet.Data;
using APPClinet.Classes;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.User.Settings
{
    [RoleAttribute("User")]
    public class NewsletterModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public NewsletterModel(APPClinetDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public bool Newsletter { get; set; }
        }
        public void OnGet()
        {
            var newsletter = _db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name)).Newsletter;
            Input = new InputModel
            {
                Newsletter = newsletter
            };
        }

        public async Task<IActionResult> OnPostAsync(InputModel input)
        {
            if (ModelState.IsValid)
            {
                var newsletter = _db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
                    newsletter.Newsletter = input.Newsletter;
                _db.Users.Update(newsletter);
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/User/Settings");
            }
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/User/Settings");
        }
    }
}
