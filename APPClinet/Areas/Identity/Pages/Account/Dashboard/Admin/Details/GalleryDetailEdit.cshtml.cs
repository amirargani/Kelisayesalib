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

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class GalleryDetailEditModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();

        public GalleryDetailEditModel(APPClinetDbContext db,
            IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
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
            [StringLength(50, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.Title)]
            public string Title { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.Text)]
            public string Text { get; set; }
            public string Image { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.Image)]
            public string ImageLink { get; set; }
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
            var detail = _db.Tbl_GalleryDetails.Where(a => a.Id.Equals(id)).FirstOrDefault();
            if (detail == null)
            {
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/GallerySubCategoriesView");
            }
            var subcategories = await _db.Tbl_GallerySubCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.SubCategoryId));
            var categories = await _db.Tbl_GalleryCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.CategoryId));
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
                Title = detail.Title,
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
                EmbedLinkGoogleMap = detail.EmbedLinkGoogleMap
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile ImageLink, IFormFile Image)
        {
            if (Input.Text != null)
            {
                var detail = _db.Tbl_GalleryDetails.Where(a => a.Id.Equals(id)).FirstOrDefault();
                var subcategories = await _db.Tbl_GallerySubCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.SubCategoryId));
                var categories = await _db.Tbl_GalleryCategories.FirstOrDefaultAsync(c => c.Id.Equals(detail.CategoryId));
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (ImageLink != null)
                {
                    if (ImageLink.ContentType == "image/jpeg")
                    {
                        if (ImageLink.Length <= 1048576) // 1 MB -> 1048576 Bit
                        {
                            var deleteimagelink = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\galleries\\" + detail.ImageLink);
                            if (System.IO.File.Exists(deleteimagelink))
                            {
                                System.IO.File.Delete(deleteimagelink);
                            }
                            string codeimageprofile = _codeGenerators.CodeImage();
                            var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\galleries");
                            string filenameimagelink = "galleries-g-" + codeimageprofile + "." + ImageLink.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, filenameimagelink), FileMode.Create))
                            {
                                ImageLink.CopyTo(fileStream);
                            }
                            if (Image != null)
                            {
                                if (Image.ContentType == "image/jpeg")
                                {
                                    if (Image.Length <= 1048576) // 1 MB -> 1048576 Bit
                                    {
                                        var deleteimage = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\galleries\\" + detail.Image);
                                        if (System.IO.File.Exists(deleteimage))
                                        {
                                            System.IO.File.Delete(deleteimage);
                                        }
                                        string codeimage = _codeGenerators.CodeImage();
                                        //var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\galleries");
                                        //var directoryuploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\images\\details\\galleries");
                                        string filename = "galleries-" + codeimage + "." + Image.FileName.Split('.').Last();
                                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                                        {
                                            Image.CopyTo(fileStream);
                                        }
                                        detail.Title = Input.Title;
                                        detail.Text = Input.Text;
                                        detail.Image = filename;
                                        detail.ImageLink = filenameimagelink;
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
                                        detail.EmbedLinkGoogleMap = Input.EmbedLinkGoogleMap;
                                        detail.ActivePassive = Input.ActivePassive;
                                        detail.UserId = user.Id;
                                        _db.Tbl_GalleryDetails.Update(detail);
                                        await _db.SaveChangesAsync();
                                    }
                                }
                            }
                            else
                            {
                                detail.Title = Input.Title;
                                detail.Text = Input.Text;
                                detail.ImageLink = filenameimagelink;
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
                                detail.EmbedLinkGoogleMap = Input.EmbedLinkGoogleMap;
                                detail.ActivePassive = Input.ActivePassive;
                                detail.UserId = user.Id;
                                _db.Tbl_GalleryDetails.Update(detail);
                                await _db.SaveChangesAsync();
                            }
                        }
                    }
                }
                else
                {
                    detail.Title = Input.Title;
                    detail.Text = Input.Text;
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
                    detail.EmbedLinkGoogleMap = Input.EmbedLinkGoogleMap;
                    detail.ActivePassive = Input.ActivePassive;
                    detail.UserId = user.Id;
                    _db.Tbl_GalleryDetails.Update(detail);
                    await _db.SaveChangesAsync();
                }
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/GalleryDetailsView");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
