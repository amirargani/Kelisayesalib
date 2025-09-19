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
    public class NDIVModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public NDIVModel(APPClinetDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGetAsync(int pageid)
        {
            var detail = await _db.Tbl_NewDetails.Where(a => a.Id == pageid && a.ActivePassive == true).FirstOrDefaultAsync();
            if (detail == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            return Redirect(Url.Content("~/") + "Identity/Search/Details/NewDetailsInfoView?id=" + pageid + "&title=" + detail.Title.Replace(" ", "§").ToString());
        }
    }
}
