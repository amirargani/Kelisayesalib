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

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Categories
{
    [RoleAttribute("Admin")]
    public class NewCategoryEditModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public NewCategoryEditModel(APPClinetDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var categories = await _db.Tbl_NewCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            Input.Id = categories.Id;
            Input.TitleCategory = categories.TitleCategory;
            Input.Description = categories.Description;
            Input.FontName = categories.FontName;
            Input.ActivePassive = categories.ActivePassive;
            return Page();
        }

        public async Task<IActionResult> OnPostChangeAsync(int id)
        {
            var categories = await _db.Tbl_NewCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            if (categories != null)
            {
                categories.TitleCategory = Input.TitleCategory;
                categories.Description = Input.Description;
                categories.FontName = Input.FontName;
                categories.ActivePassive = Input.ActivePassive;
                _db.Tbl_NewCategories.Update(categories);
                await _db.SaveChangesAsync();
                return RedirectToPage("NewCategoriesView");
            }
            return RedirectToPage("NewCategoriesView");
        }
    }
}
