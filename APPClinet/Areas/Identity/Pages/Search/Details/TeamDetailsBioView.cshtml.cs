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
    public class TeamDetailsBioViewModel : PageModel
    {
        private readonly APPClinetDbContext _db;

        public TeamDetailsBioViewModel(APPClinetDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FirstNameEN { get; set; }
            public string LastNameEN { get; set; }
            public string Service { get; set; }
            public string Text { get; set; }
            public string Image { get; set; }
            public string ImageProfile { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string PrivateNumber { get; set; }
            public string Telegram { get; set; }
            public string Youtube { get; set; }
            public string Instagram { get; set; }
            public string Twitter { get; set; }
            public string Facebook { get; set; }
            public string Website { get; set; }
            public int CategoryId { get; set; }
            public string TitleCategory { get; set; }
            public int SubCategoryId { get; set; }
            public string TitleSubCategory { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id, string firstname, string lastname)
        {
            var detail = await _db.Tbl_TeamDetails.Where(a => a.Id == id && a.FirstName == firstname.Replace("§", " ").ToString() && a.LastName == lastname.Replace("§", " ").ToString() && a.ActivePassive == true).FirstOrDefaultAsync();
            if (detail == null)
            {
                return Redirect(Url.Content("~/" + "Error"));
            }
            var subcategories = await _db.Tbl_TeamSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.SubCategoryId));
            var categories = await _db.Tbl_TeamCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.CategoryId));
            Input = new InputModel
            {
                Id = id,
                TitleCategory = categories.TitleCategory,
                TitleSubCategory = subcategories.TitleSubCategory,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                FirstNameEN = detail.FirstNameEN,
                LastNameEN = detail.LastNameEN,
                Service = detail.Service,
                Text = detail.Text,
                Image = detail.Image,
                ImageProfile = detail.ImageProfile,
                Email = detail.Email,
                PhoneNumber = detail.PhoneNumber,
                PrivateNumber = detail.PrivateNumber,
                Telegram = detail.Telegram,
                Youtube = detail.Youtube,
                Instagram = detail.Instagram,
                Twitter = detail.Twitter,
                Facebook = detail.Facebook,
                Website = detail.Website
            };
            if (detail.CountViews == null)
                detail.CountViews = 1;
            else
                detail.CountViews = detail.CountViews + 1;
            _db.Tbl_TeamDetails.Update(detail);
            await _db.SaveChangesAsync();
            return Page();
        }
    }
}
