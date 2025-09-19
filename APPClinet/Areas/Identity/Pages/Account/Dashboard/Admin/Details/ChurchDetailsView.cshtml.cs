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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.About;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class ChurchDetailsViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IApplicationUser _iuser;
        public ChurchDetailsViewModel(APPClinetDbContext db,
            IApplicationUser iuser)
        {
            _db = db;
            _iuser = iuser;
        }

        //public IList<ChurchDetails> details { get; set; }

        public ChurchPages pages { get; set; }

        //public async Task OnGetAsync()
        //{
        //    details = await _db.Tbl_ChurchDetails.Include(s => s.Tbl_ChurchSubCategories).Include(c => c.Tbl_ChurchCategories).Include(u => u.AspNetUsers).ToListAsync();
        //}

        public IActionResult OnGetAsync(int pageid = 1)
        {
            // ASP.NET Core Razor Pages - Pagination
            pages = _iuser.OnGetChurchPagesDetails(pageid);
            if (pages.churchDetails.Count == 0)
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
            var find = _db.Tbl_ChurchDetails.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = true;
                _db.Tbl_ChurchDetails.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToPage();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostInactiveAsync(int id)
        {
            var find = _db.Tbl_ChurchDetails.FirstOrDefault(i => i.Id.Equals(id));
            if (find != null)
            {
                find.ActivePassive = false;
                _db.Tbl_ChurchDetails.Attach(find).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToPage();
            }
            return RedirectToPage();
        }
    }
}

