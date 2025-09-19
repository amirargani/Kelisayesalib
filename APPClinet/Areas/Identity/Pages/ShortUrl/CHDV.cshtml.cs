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

namespace APPClinet.Areas.Identity.Pages.ShortUrl
{
    public class CHDVModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public CHDVModel(APPClinetDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int pageid)
        {
            var subcategories = await _db.Tbl_ChurchSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(pageid));
            if (subcategories == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            return Redirect(Url.Content("~/") + "Identity/Search/Details/ChurchDetailsView?titlesubcategory=" + subcategories.TitleSubCategory.Replace(" ", "§").ToString());
        }
    }
}
