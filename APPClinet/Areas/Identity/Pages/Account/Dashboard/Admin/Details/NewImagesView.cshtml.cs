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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.News;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class NewImagesViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;
        public NewImagesViewModel(APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _db = db;
            _iuser = iuser;
        }

        //public IList<NewImages> images { get; set; }

        public int iddetail { get; set; }
        public NewPages pages { get; set; }

        //public async Task OnGetAsync(int id)
        //{
        //    images = await _db.Tbl_NewImages.Where(i => i.DetailId.Equals(id)).Include(s => s.Tbl_NewDetails).Include(s => s.Tbl_NewSubCategories).Include(c => c.Tbl_NewCategories).Include(u => u.AspNetUsers).ToListAsync();
        //}

        public IActionResult OnGetAsync(int id, int pageid = 1)
        {
            // ASP.NET Core Razor Pages - Pagination
            iddetail = id;
            pages = _iuser.OnGetNewPagesImages(id, pageid);
            if (pages.newImages.Count == 0)
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
            var find = _db.Tbl_NewImages.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = true;
                _db.Tbl_NewImages.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/NewImagesView?id=" + Convert.ToInt32(find.DetailId));
            }
            return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/NewImagesView?id=" + Convert.ToInt32(find.DetailId));
        }

        public async Task<IActionResult> OnPostInactiveAsync(int id)
        {
            var find = _db.Tbl_NewImages.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = false;
                _db.Tbl_NewImages.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/NewImagesView?id=" + Convert.ToInt32(find.DetailId));
            }
            return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/NewImagesView?id=" + Convert.ToInt32(find.DetailId));
        }
    }
}

