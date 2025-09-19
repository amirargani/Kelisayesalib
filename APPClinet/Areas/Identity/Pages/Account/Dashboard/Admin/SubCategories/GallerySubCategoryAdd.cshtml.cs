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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.SubCategories
{
    [RoleAttribute("Admin")]
    public class GallerySubCategoryAddModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public GallerySubCategoryAddModel(APPClinetDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string ErrorStatusMessage { get; set; }

        public IList<GalleryCategories> categories { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
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
            public int CategoryId { get; set; }
        }

        public async Task OnGetAsync()
        {
            TempData.Clear();
            categories = await _db.Tbl_GalleryCategories.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(GallerySubCategories subcategories)
        {
            if (ModelState.IsValid)
            {
                subcategories.CategoryId = Input.CategoryId;
                int count = _db.Tbl_GallerySubCategories.Count();
                subcategories.Date = DateTime.Now;
                subcategories.Position = count + 1;
                subcategories.TitleSubCategory = Input.TitleSubCategory;
                subcategories.Description = Input.Description;
                subcategories.FontName = Input.FontName;
                subcategories.ActivePassive = Input.ActivePassive;
                subcategories.CountViews = null;
                _db.Tbl_GallerySubCategories.Add(subcategories);
                await _db.SaveChangesAsync();
                StatusMessage = faMessage.SubCategoryMessageAdd;
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
