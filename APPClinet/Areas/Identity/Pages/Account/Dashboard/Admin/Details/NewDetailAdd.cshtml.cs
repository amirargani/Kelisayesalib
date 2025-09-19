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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.News;
using Microsoft.AspNetCore.Hosting;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class NewDetailAddModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();
        private readonly IApplicationUser _iuser;

        public NewDetailAddModel(APPClinetDbContext db,
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
            var subcategories = await _db.Tbl_NewSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var categories = await _db.Tbl_NewCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            CategoryId = categories.Id;
            TitleCategory = categories.TitleCategory;
            SubCategoryId = subcategories.Id;
            TitleSubCategory = subcategories.TitleSubCategory;
            Input = new InputModel
            {
                CategoryId = categories.Id,
                SubCategoryId = subcategories.Id
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile ImageLink, IFormFile Image)
        {
            if (ImageLink != null && Input.Text != null)
            {
                NewDetails detail = new NewDetails();
                if (ImageLink.ContentType == "image/jpeg")
                {
                    if (ImageLink.Length <= 1048576) // 1 MB -> 1048576 Bit
                    {
                        string codeimageprofile = _codeGenerators.CodeImage();
                        var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\news");
                        string filenameimagelink = "news-g-" + codeimageprofile + "." + ImageLink.FileName.Split('.').Last();
                        using (var fileStream = new FileStream(Path.Combine(uploads, filenameimagelink), FileMode.Create))
                        {
                            ImageLink.CopyTo(fileStream);
                        }
                        //NewDetails detail = new NewDetails();
                        var subcategories = await _db.Tbl_NewSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
                        var categories = await _db.Tbl_NewCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
                        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg")
                            {
                                if (Image.Length <= 1048576) // 1 MB -> 1048576 Bit
                                {
                                    string codeimage = _codeGenerators.CodeImage();
                                    //var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\news");
                                    //var directoryuploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\images\\details\\news");
                                    string filename = "news-" + codeimage + "." + Image.FileName.Split('.').Last();
                                    using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                                    {
                                        Image.CopyTo(fileStream);
                                    }
                                    int count = _db.Tbl_NewDetails.Count();
                                    detail.Date = DateTime.Now;
                                    detail.Position = count + 1;
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
                                    detail.CategoryId = categories.Id;
                                    detail.SubCategoryId = subcategories.Id;
                                    detail.ActivePassive = Input.ActivePassive;
                                    detail.UserId = user.Id;
                                    detail.CountViews = null;
                                    _db.Tbl_NewDetails.Add(detail);
                                    await _db.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            int count = _db.Tbl_NewDetails.Count();
                            detail.Date = DateTime.Now;
                            detail.Position = count + 1;
                            detail.Title = Input.Title;
                            detail.Text = Input.Text;
                            detail.Image = null;
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
                            detail.CategoryId = categories.Id;
                            detail.SubCategoryId = subcategories.Id;
                            detail.ActivePassive = Input.ActivePassive;
                            detail.UserId = user.Id;
                            detail.CountViews = null;
                            _db.Tbl_NewDetails.Add(detail);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
                // Send Emails
                string callbackUrl = Url.PageLink().Replace("/Identity/Account/Dashboard/Admin/Details/NewDetailAdd", "/Identity/Search/Details/NewDetailsInfoView?id=" + detail.Id + "&title=" + detail.Title.Replace(" ", "§").ToString()).ToString();
                string urlpagelink = Url.PageLink().Replace("/Identity/Account/Dashboard/Admin/Details/NewDetailAdd", "/").ToString();
                var users = await _db.Users.Where(u => u.EmailConfirmed == true && u.Newsletter == true).ToListAsync();
                _iuser.OnGetSendNewsletterNewEmailUsers(detail.Id, users, callbackUrl, urlpagelink);
                // Send Emails
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/NewSubCategoriesView");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
