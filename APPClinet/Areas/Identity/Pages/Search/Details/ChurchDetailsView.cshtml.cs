using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using APPClinet.Messages;
using APPClinet.Data;

namespace APPClinet.Areas.Identity.Pages.Search.Details
{
    [AllowAnonymous]
    public class ChurchDetailsViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public ChurchDetailsViewModel(APPClinetDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }
            public string Text { get; set; }
            public string Image { get; set; }
            public string TitleCategory { get; set; }
            public string TitleSubCategory { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string titlesubcategory)
        {
            var subcategories = await _db.Tbl_ChurchSubCategories.FirstOrDefaultAsync(c => c.TitleSubCategory.Equals(titlesubcategory.Replace("§", " ").ToString()));
            if (subcategories == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            var categories = await _db.Tbl_ChurchCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            var detail = _db.Tbl_ChurchDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id && a.ActivePassive == true).FirstOrDefault();
            if (detail == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            Input = new InputModel
            {
                Id = subcategories.Id,
                TitleCategory = categories.TitleCategory,
                TitleSubCategory = subcategories.TitleSubCategory,
                Text = detail.Text,
                Image = detail.Image
            };
            if (detail.CountViews == null)
                detail.CountViews = 1;
            else
                detail.CountViews = detail.CountViews + 1;
            _db.Tbl_ChurchDetails.Update(detail);
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}
