using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using APPClinet.Data;
using APPClinet.Classes;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class GalleryImagesViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;
        public GalleryImagesViewModel(APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _db = db;
            _iuser = iuser;
        }

        //public IList<GalleryImages> images { get; set; }

        public int iddetail { get; set; }
        public GalleryPages pages { get; set; }

        //public async Task OnGetAsync(int id)
        //{
        //    images = await _db.Tbl_GalleryImages.Where(i => i.DetailId.Equals(id)).Include(s => s.Tbl_GalleryDetails).Include(s => s.Tbl_GallerySubCategories).Include(c => c.Tbl_GalleryCategories).Include(u => u.AspNetUsers).ToListAsync();
        //}

        public IActionResult OnGetAsync(int id, int pageid = 1)
        {
            // ASP.NET Core Razor Pages - Pagination
            iddetail = id;
            pages = _iuser.OnGetGalleryPagesImages(id, pageid);
            if (pages.galleryImages.Count == 0)
            {
                return Page();
            }
            if (pageid > pages.PageCount)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            return Page();
        }

        public async Task<IActionResult> OnPostActiveAsync(int id)
        {
            var find = _db.Tbl_GalleryImages.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = true;
                _db.Tbl_GalleryImages.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/GalleryImagesView?id=" + Convert.ToInt32(find.DetailId));
            }
            return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/GalleryImagesView?id=" + Convert.ToInt32(find.DetailId));
        }

        public async Task<IActionResult> OnPostInactiveAsync(int id)
        {
            var find = _db.Tbl_GalleryImages.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = false;
                _db.Tbl_GalleryImages.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/GalleryImagesView?id=" + Convert.ToInt32(find.DetailId));
            }
            return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/GalleryImagesView?id=" + Convert.ToInt32(find.DetailId));
        }
    }
}

