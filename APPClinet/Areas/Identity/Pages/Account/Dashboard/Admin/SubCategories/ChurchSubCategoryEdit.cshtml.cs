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

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.SubCategories
{
    [RoleAttribute("Admin")]
    public class ChurchSubCategoryEditModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public ChurchSubCategoryEditModel(APPClinetDbContext db)
        {
            _db = db;
        }

        public int CategoryId { get; set; }

        public string TitleCategory { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [StringLength(50, ErrorMessage = faMessage.RequiredMsgMax, MinimumLength = 3)]
            [Display(Name = faMessage.TitleSubCategory)]
            public string TitleSubCategory { get; set; }
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
            var subcategories = await _db.Tbl_ChurchSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var categories = await _db.Tbl_ChurchCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            CategoryId = categories.Id;
            TitleCategory = categories.TitleCategory;
            Input.Id = subcategories.Id;
            Input.TitleSubCategory = subcategories.TitleSubCategory;
            Input.Description = subcategories.Description;
            Input.FontName = subcategories.FontName;
            Input.ActivePassive = subcategories.ActivePassive;
            return Page();
        }

        public async Task<IActionResult> OnPostChangeAsync(int id)
        {
            var subcategories = await _db.Tbl_ChurchSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            if (subcategories != null)
            {
                subcategories.TitleSubCategory = Input.TitleSubCategory;
                subcategories.Description = Input.Description;
                subcategories.FontName = Input.FontName;
                subcategories.ActivePassive = Input.ActivePassive;
                _db.Tbl_ChurchSubCategories.Update(subcategories);
                await _db.SaveChangesAsync();
                return RedirectToPage("ChurchSubCategoriesView");
            }
            return RedirectToPage("ChurchSubCategoriesView");
        }
    }
}
