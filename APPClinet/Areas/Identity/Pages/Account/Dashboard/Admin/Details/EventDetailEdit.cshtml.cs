using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using APPClinet.Messages;
using APPClinet.Data;
using APPClinet.Classes;
using Microsoft.AspNetCore.Hosting;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class EventDetailEditModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();
        private readonly IApplicationUser _iuser;


        public EventDetailEditModel(APPClinetDbContext db,
            IWebHostEnvironment environment,
            IApplicationUser iuser)
        {
            _db = db;
            _environment = environment;
            _iuser = iuser;
        }

        public int CategoryId { get; set; }

        public string TitleCategory { get; set; }

        public int SubCategoryId { get; set; }

        public string TitleSubCategory { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.Text)]
            public string Text { get; set; }
            public string Image { get; set; }
            [EmailAddress(ErrorMessage = faMessage.RequiredMsgEmailAddress)]
            [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = faMessage.RequiredMsgEmailAddressExpression)]
            [Display(Name = faMessage.Email)]
            public string Email { get; set; }
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\s*\+?[0-9]\d{1,2}?[- .]?\d{3,4}[- .]?\d{7,8}$", ErrorMessage = faMessage.RequiredMsgInfo)]
            [Display(Name = faMessage.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^\s*\+?[0-9]\d{1,2}?[- .]?\d{3,4}[- .]?\d{7,8}$", ErrorMessage = faMessage.RequiredMsgInfo)]
            [Display(Name = faMessage.PrivateNumber)]
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
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.TimeEvent)]
            public string TimeEvent { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.Countdown)]
            public string Countdown { get; set; }
            public string EmbedLinkGoogleMap { get; set; }
            public int CategoryId { get; set; }
            public string TitleCategory { get; set; }
            public int SubCategoryId { get; set; }
            public string TitleSubCategory { get; set; }
            public bool ActivePassive { get; set; }
            public string UserId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var subcategories = await _db.Tbl_EventSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var categories = await _db.Tbl_EventCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            var detail = _db.Tbl_EventDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
            if (detail == null)
            {
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/EventSubCategoriesView");
            }
            CategoryId = categories.Id;
            TitleCategory = categories.TitleCategory;
            SubCategoryId = subcategories.Id;
            TitleSubCategory = subcategories.TitleSubCategory;
            Input = new InputModel
            {
                CategoryId = categories.Id,
                SubCategoryId = subcategories.Id,
                TitleCategory = categories.TitleCategory,
                TitleSubCategory = subcategories.TitleSubCategory,
                ActivePassive = detail.ActivePassive,
                Text = detail.Text,
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile Image)
        {
            if (Input.Text != null)
            {
                var subcategories = await _db.Tbl_EventSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
                var categories = await _db.Tbl_EventCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
                var detail = _db.Tbl_EventDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        if (Image.Length <= 1048576) // 1 MB -> 1048576 Bit
                        {
                            string codeimage = _codeGenerators.CodeImage();
                            var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\events");
                            var deleteimage = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\events\\" + detail.Image);
                            if (System.IO.File.Exists(deleteimage))
                            {
                                System.IO.File.Delete(deleteimage);
                            }
                            string filename = "church-" + codeimage + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            detail.Text = Input.Text;
                            detail.Image = filename;
                            detail.ActivePassive = Input.ActivePassive;
                            detail.UserId = user.Id;
                            detail.Email = Input.Email;
                            detail.PhoneNumber = Input.PhoneNumber;
                            detail.PrivateNumber = Input.PrivateNumber;
                            detail.Telegram = Input.Telegram;
                            detail.Youtube = Input.Youtube;
                            detail.Instagram = Input.Instagram;
                            detail.Twitter = Input.Twitter;
                            detail.Facebook = Input.Facebook;
                            detail.Website = Input.Website;
                            detail.YoutubeLink = Input.YoutubeLink;
                            detail.StartAt = Input.StartAt;
                            detail.StopAt = Input.StopAt;
                            detail.TimeEvent = Input.TimeEvent;
                            detail.Countdown = Input.Countdown;
                            detail.EmbedLinkGoogleMap = Input.EmbedLinkGoogleMap;
                            _db.Tbl_EventDetails.Update(detail);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    detail.Text = Input.Text;
                    detail.ActivePassive = Input.ActivePassive;
                    detail.UserId = user.Id;
                    detail.Email = Input.Email;
                    detail.PhoneNumber = Input.PhoneNumber;
                    detail.PrivateNumber = Input.PrivateNumber;
                    detail.Telegram = Input.Telegram;
                    detail.Youtube = Input.Youtube;
                    detail.Instagram = Input.Instagram;
                    detail.Twitter = Input.Twitter;
                    detail.Facebook = Input.Facebook;
                    detail.Website = Input.Website;
                    detail.YoutubeLink = Input.YoutubeLink;
                    detail.StartAt = Input.StartAt;
                    detail.StopAt = Input.StopAt;
                    detail.TimeEvent = Input.TimeEvent;
                    detail.Countdown = Input.Countdown;
                    detail.EmbedLinkGoogleMap = Input.EmbedLinkGoogleMap;
                    _db.Tbl_EventDetails.Update(detail);
                    await _db.SaveChangesAsync();
                }
                // Send Emails
                //if (detail.Id == 1)
                //{
                    string callbackUrl = Url.PageLink().Replace("/Identity/Account/Dashboard/Admin/Details/EventDetailEdit", "/Identity/Search/Details/EventDetailsView?titlesubcategory=" + subcategories.TitleSubCategory.Replace(" ", "§").ToString()).ToString();
                    string urlpagelink = Url.PageLink().Replace("/Identity/Account/Dashboard/Admin/Details/EventDetailEdit", "/").ToString();
                    var users = await _db.Users.Where(u => u.EmailConfirmed == true && u.Newsletter == true).ToListAsync();
                    _iuser.OnGetSendEventEmailUsers(id, users, callbackUrl, urlpagelink);
                //}
                // Send Emails
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/EventSubCategoriesView");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
