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

namespace APPClinet.Areas.Identity.Pages.Search.Details
{
    [AllowAnonymous]
    public class GalleryDetailsInfoViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public GalleryDetailsInfoViewModel(APPClinetDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string Title { get; set; }
            public string Text { get; set; }
            public string Image { get; set; }
            public string ImageLink { get; set; }
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
            public string EmbedLinkGoogleMap { get; set; }
            public int CategoryId { get; set; }
            public string TitleCategory { get; set; }
            public int SubCategoryId { get; set; }
            public string TitleSubCategory { get; set; }
            public int CountViews { get; set; }
        }

        public IList<GalleryImages> galleryImages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, string title)
        {
            var detail = await _db.Tbl_GalleryDetails.Where(a => a.Id == id && a.Title == title.Replace("§", " ").ToString() && a.ActivePassive == true).FirstOrDefaultAsync();
            if (detail == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            galleryImages = await _db.Tbl_GalleryImages.Where(i => i.DetailId.Equals(detail.Id) && i.ActivePassive == true).ToListAsync();
            var subcategories = await _db.Tbl_GallerySubCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.SubCategoryId));
            var categories = await _db.Tbl_GalleryCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.CategoryId));
            Input = new InputModel
            {
                Id = id,
                TitleCategory = categories.TitleCategory,
                TitleSubCategory = subcategories.TitleSubCategory,
                Date= detail.Date,
                Title = detail.Title,
                Text = detail.Text,
                Image = detail.Image,
                ImageLink = detail.ImageLink,
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
                EmbedLinkGoogleMap = detail.EmbedLinkGoogleMap,
                CountViews = Convert.ToInt32(detail.CountViews)
            };
            if (detail.CountViews == null)
                detail.CountViews = 1;
            else
                detail.CountViews = detail.CountViews + 1;
            _db.Tbl_GalleryDetails.Update(detail);
            await _db.SaveChangesAsync();
            return Page();
        }

        public IActionResult OnGetViews(int id)
        {
            var image = _db.Tbl_GalleryImages.Where(a => a.Id == id).FirstOrDefault();
            if (image.CountViews == null)
                image.CountViews = 1;
            else
                image.CountViews = image.CountViews + 1;
            _db.Tbl_GalleryImages.Update(image);
            _db.SaveChanges();
            return new JsonResult("Ok");
        }
    }
}
