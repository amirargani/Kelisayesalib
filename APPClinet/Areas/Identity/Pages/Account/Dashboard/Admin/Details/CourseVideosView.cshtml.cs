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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class CourseVideosViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;
        public CourseVideosViewModel(APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _db = db;
            _iuser = iuser;
        }

        //public IList<CourseVideo> images { get; set; }

        public int iddetail { get; set; }
        public CoursePages pages { get; set; }

        //public async Task OnGetAsync(int id)
        //{
        //    images = await _db.Tbl_CourseVideos.Where(i => i.DetailId.Equals(id)).Include(s => s.Tbl_CourseDetails).Include(s => s.Tbl_CourseSubCategories).Include(c => c.Tbl_CourseCategories).Include(u => u.AspNetUsers).ToListAsync();
        //}

        public IActionResult OnGetAsync(int id, int pageid = 1)
        {
            // ASP.NET Core Razor Pages - Pagination
            iddetail = id;
            pages = _iuser.OnGetCoursePagesVideos(id, pageid);
            if (pages.courseVideos.Count == 0)
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
            var find = _db.Tbl_CourseVideos.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = true;
                _db.Tbl_CourseVideos.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseVideosView?id=" + Convert.ToInt32(find.DetailId));
            }
            return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseVideosView?id=" + Convert.ToInt32(find.DetailId));
        }

        public async Task<IActionResult> OnPostInactiveAsync(int id)
        {
            var find = _db.Tbl_CourseVideos.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = false;
                _db.Tbl_CourseVideos.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseVideosView?id=" + Convert.ToInt32(find.DetailId));
            }
            return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseVideosView?id=" + Convert.ToInt32(find.DetailId));
        }
    }
}

