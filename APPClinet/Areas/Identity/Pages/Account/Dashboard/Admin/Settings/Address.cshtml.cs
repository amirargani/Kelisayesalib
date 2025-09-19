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
    public class AddressModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();

        public AddressModel(APPClinetDbContext db,
            IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.Street)]
            public string Street { get; set; }
            // debuggex.com -> \d{1,3}$ 1 || 12 || 123
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [RegularExpression(@"\d{1,3}$", ErrorMessage = faMessage.RequiredMsgInfo)]
            [Display(Name = faMessage.Number)]
            public int Number { get; set; }
            // debuggex.com -> \d{7,8}$ 1234567 || 12345678
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [RegularExpression(@"\d{5,6}$", ErrorMessage = faMessage.RequiredMsgInfo)]
            [Display(Name = faMessage.PostCode)]
            public int PostCode { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.City)]
            public string City { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.Country)]
            public string Country { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var address = await _db.Tbl_Address.FirstOrDefaultAsync(c => c.Id.Equals(id));
            if (address == null)
            {
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Settings/Index");
            }
            Input = new InputModel
            {
                Street = address.Street,
                Number = address.Number,
                PostCode = address.PostCode,
                City = address.City,
                Country = address.Country
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid)
            {
                var address = await _db.Tbl_Address.FirstOrDefaultAsync(c => c.Id.Equals(id));
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                address.Date = DateTime.Now;
                address.Street = Input.Street;
                address.Number = Input.Number;
                address.PostCode = Input.PostCode;
                address.City = Input.City;
                address.Country = Input.Country;
                address.UserId = user.Id;
                _db.Tbl_Address.Update(address);
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
