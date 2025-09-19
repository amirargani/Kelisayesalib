using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using APPClinet.Messages;
using APPClinet.Data;
using APPClinet.Classes;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.About;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Categories
{
    [RoleAttribute("Admin")]
    public class ChurchCategoryAddModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public ChurchCategoryAddModel(APPClinetDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string ErrorStatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [StringLength(50, ErrorMessage = faMessage.RequiredMsgMax, MinimumLength = 3)]
            [Display(Name = faMessage.TitleCategory)]
            public string TitleCategory { get; set; }
            [StringLength(150, ErrorMessage = faMessage.RequiredMsgMax, MinimumLength = 3)]
            [Display(Name = faMessage.Description)]
            public string Description { get; set; }
            public bool ActivePassive { get; set; }
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgMax, MinimumLength = 3)]
            [Display(Name = faMessage.FontName)]
            public string FontName { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            TempData.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(ChurchCategories categories)
        {
            if (ModelState.IsValid)
            {
                int count = _db.Tbl_ChurchCategories.Count();
                categories.Date = DateTime.Now;
                categories.Position = count + 1;
                categories.TitleCategory = Input.TitleCategory;
                categories.Description = Input.Description;
                categories.FontName = Input.FontName;
                categories.ActivePassive = Input.ActivePassive;
                _db.Tbl_ChurchCategories.Add(categories);
                await _db.SaveChangesAsync();
                StatusMessage = faMessage.CategoryMessageAdd;
                return RedirectToPage();
            }
            else
            {
                ErrorStatusMessage = faMessage.CategoryErrroMessage;
                return RedirectToPage();
            }
        }
    }
}
