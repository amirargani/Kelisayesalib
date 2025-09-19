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
    public class EventDetailsViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public EventDetailsViewModel(APPClinetDbContext db)
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
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string PrivateNumber { get; set; }
            public string Telegram { get; set; }
            public string Youtube { get; set; }
            public string Instagram { get; set; }
            public string Twitter { get; set; }
            public string Facebook { get; set; }
            public string Website { get; set; }
            public string YoutubeLink { get; set; }
            public int StartAt { get; set; }
            public int StopAt { get; set; }
            public string TimeEvent { get; set; }
            public string Countdown { get; set; }
            public string EmbedLinkGoogleMap { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string titlesubcategory)
        {
            var subcategories = await _db.Tbl_EventSubCategories.FirstOrDefaultAsync(c => c.TitleSubCategory.Equals(titlesubcategory.Replace("§", " ").ToString()));
            if (subcategories == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            var categories = await _db.Tbl_EventCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            var detail = _db.Tbl_EventDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id && a.ActivePassive == true).FirstOrDefault();
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
                Image = detail.Image,
                Email = detail.Email,
                PhoneNumber = detail.PhoneNumber,
                PrivateNumber = detail.PrivateNumber,
                Telegram = detail.Telegram,
                Youtube = detail.Youtube,
                Instagram = detail.Instagram,
                Twitter = detail.Twitter,
                Facebook = detail.Facebook,
                Website = detail.Website,
                YoutubeLink = detail.YoutubeLink,
                StartAt = Convert.ToInt32(detail.StartAt),
                StopAt = Convert.ToInt32(detail.StopAt),
                TimeEvent = detail.TimeEvent,
                Countdown = detail.Countdown,
                EmbedLinkGoogleMap = detail.EmbedLinkGoogleMap
            };
            if (detail.CountViews == null)
                detail.CountViews = 1;
            else
                detail.CountViews = detail.CountViews + 1;
            _db.Tbl_EventDetails.Update(detail);
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}
