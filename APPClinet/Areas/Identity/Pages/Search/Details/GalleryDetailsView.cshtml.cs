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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Search.Details
{
    [AllowAnonymous]
    public class GalleryDetailsViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;

        public GalleryDetailsViewModel(APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _db = db;
            _iuser = iuser;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string TitleCategory { get; set; }
            public string TitleSubCategory { get; set; }
        }

        public IList<GalleryDetails> details { get; set; }

        public GalleryPages pages { get; set; }

        public async Task<IActionResult> OnGetAsync(string titlesubcategory, int pageid = 1)
        {
            var subcategories = await _db.Tbl_GallerySubCategories.FirstOrDefaultAsync(c => c.TitleSubCategory.Equals(titlesubcategory));
            if (subcategories == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            var categories = await _db.Tbl_GalleryCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            details = await _db.Tbl_GalleryDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id && a.ActivePassive == true).ToListAsync();
            if (details.Count == 0)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            // ASP.NET Core Razor Pages - Pagination
            pages = _iuser.OnGetSearchGalleryPagesDetails(titlesubcategory, pageid);
            if (pageid > pages.PageCount)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            Input = new InputModel
            {
                TitleCategory = categories.TitleCategory,
                TitleSubCategory = subcategories.TitleSubCategory
            };
            if (subcategories.CountViews == null)
                subcategories.CountViews = 1;
            else
                subcategories.CountViews = subcategories.CountViews + 1;
            _db.Tbl_GallerySubCategories.Update(subcategories);
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}
