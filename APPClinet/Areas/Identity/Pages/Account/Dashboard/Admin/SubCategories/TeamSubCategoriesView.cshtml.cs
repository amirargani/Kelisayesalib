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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Teams;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.SubCategories
{
    [RoleAttribute("Admin")]
    public class TeamSubCategoriesViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;
        public TeamSubCategoriesViewModel(APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _db = db;
            _iuser = iuser;
        }

        //public IList<TeamSubCategories> subcategories { get; set; }

        public TeamPages pages { get; set; }

        //public async Task OnGetAsync()
        //{
        //    //subcategories = await _db.Tbl_EventSubCategories.ToListAsync();
        //    subcategories = await _db.Tbl_TeamSubCategories.Include(c => c.Tbl_TeamCategories).Include(d => d.Tbl_TeamDetails).ToListAsync();
        //}

        public IActionResult OnGetAsync(int pageid = 1)
        {
            // ASP.NET Core Razor Pages - Pagination
            pages = _iuser.OnGetTeamPagesSubCategories(pageid);
            if (pages.teamSubCategories.Count == 0)
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
            var find = _db.Tbl_TeamSubCategories.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = true;
                _db.Tbl_TeamSubCategories.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToPage();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostInactiveAsync(int id)
        {
            var find = _db.Tbl_TeamSubCategories.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = false;
                _db.Tbl_TeamSubCategories.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToPage();
            }
            return RedirectToPage();
        }
    }
}

