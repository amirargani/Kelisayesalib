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
    public class TDBVModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public TDBVModel(APPClinetDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int pageid)
        {
            var detail = await _db.Tbl_TeamDetails.Where(a => a.Id == pageid && a.ActivePassive == true).FirstOrDefaultAsync();
            if (detail == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            return Redirect(Url.Content("~/") + "Identity/Search/Details/TeamDetailsBioView?id=" + pageid + "&firstname=" + detail.FirstName.Replace(" ", "§").ToString() + "&lastname=" + detail.LastName.Replace(" ", "§").ToString());
        }
    }
}
