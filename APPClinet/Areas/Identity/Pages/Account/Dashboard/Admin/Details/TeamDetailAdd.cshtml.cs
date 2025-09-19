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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Teams;
using Microsoft.AspNetCore.Hosting;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class TeamDetailAddModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();

        public TeamDetailAddModel(APPClinetDbContext db,
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
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.FirstName)]
            public string FirstName { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.LastName)]
            public string LastName { get; set; }
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.FirstNameEN)]
            public string FirstNameEN { get; set; }
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.LastNameEN)]
            public string LastNameEN { get; set; }
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.Service)]
            public string Service { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.Text)]
            public string Text { get; set; }
            public string Image { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.Image)]
            public string ImageProfile { get; set; }
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
            public int CategoryId { get; set; }
            public string TitleCategory { get; set; }
            public int SubCategoryId { get; set; }
            public string TitleSubCategory { get; set; }
            public bool ActivePassive { get; set; }
            public string UserId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var subcategories = await _db.Tbl_TeamSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var categories = await _db.Tbl_TeamCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            // EventDetailAdd.cshtml.cs
            //var detail = _db.Tbl_TeamDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
            //if (detail != null)
            //{
            //    return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/TeamSubCategoriesView");
            //}
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

        public async Task<IActionResult> OnPostAsync(int id, IFormFile ImageProfile, IFormFile Image)
        {
            if (ImageProfile != null && Input.Text != null)
            {
                if (ImageProfile.ContentType == "image/jpeg")
                {
                    if (ImageProfile.Length <= 1048576) // 1 MB -> 1048576 Bit
                    {
                        string codeimageprofile = _codeGenerators.CodeImage();
                        var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\teams");
                        string filenameimageprofile = "teams-g-" + codeimageprofile + "." + ImageProfile.FileName.Split('.').Last();
                        using (var fileStream = new FileStream(Path.Combine(uploads, filenameimageprofile), FileMode.Create))
                        {
                            ImageProfile.CopyTo(fileStream);
                        }
                        TeamDetails detail = new TeamDetails();
                        var subcategories = await _db.Tbl_TeamSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
                        var categories = await _db.Tbl_TeamCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
                        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                        if (Image != null)
                        {
                            if (Image.ContentType == "image/jpeg")
                            {
                                if (Image.Length <= 1048576) // 1 MB -> 1048576 Bit
                                {
                                    string codeimage = _codeGenerators.CodeImage();
                                    //var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\teams");
                                    //var directoryuploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\images\\details\\teams");
                                    string filename = "teams-" + codeimage + "." + Image.FileName.Split('.').Last();
                                    using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                                    {
                                        Image.CopyTo(fileStream);
                                    }
                                    int count = _db.Tbl_TeamDetails.Count();
                                    detail.Date = DateTime.Now;
                                    detail.Position = count + 1;
                                    detail.FirstName = Input.FirstName;
                                    detail.LastName = Input.LastName;
                                    detail.FirstNameEN = Input.FirstNameEN;
                                    detail.LastNameEN = Input.LastNameEN;
                                    detail.Service = Input.Service;
                                    detail.Text = Input.Text;
                                    detail.Image = filename;
                                    detail.ImageProfile = filenameimageprofile;
                                    detail.Email = Input.Email;
                                    detail.PhoneNumber = Input.PhoneNumber;
                                    detail.PrivateNumber = Input.PrivateNumber;
                                    detail.Telegram = Input.Telegram;
                                    detail.Youtube = Input.Youtube;
                                    detail.Instagram = Input.Instagram;
                                    detail.Twitter = Input.Twitter;
                                    detail.Facebook = Input.Facebook;
                                    detail.Website = Input.Website;
                                    detail.CategoryId = categories.Id;
                                    detail.SubCategoryId = subcategories.Id;
                                    detail.ActivePassive = Input.ActivePassive;
                                    detail.UserId = user.Id;
                                    detail.CountViews = null;
                                    _db.Tbl_TeamDetails.Add(detail);
                                    await _db.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            int count = _db.Tbl_TeamDetails.Count();
                            detail.Date = DateTime.Now;
                            detail.Position = count + 1;
                            detail.FirstName = Input.FirstName;
                            detail.LastName = Input.LastName;
                            detail.FirstNameEN = Input.FirstNameEN;
                            detail.LastNameEN = Input.LastNameEN;
                            detail.Service = Input.Service;
                            detail.Text = Input.Text;
                            detail.Image = null;
                            detail.ImageProfile = filenameimageprofile;
                            detail.Email = Input.Email;
                            detail.PhoneNumber = Input.PhoneNumber;
                            detail.PrivateNumber = Input.PrivateNumber;
                            detail.Telegram = Input.Telegram;
                            detail.Youtube = Input.Youtube;
                            detail.Instagram = Input.Instagram;
                            detail.Twitter = Input.Twitter;
                            detail.Facebook = Input.Facebook;
                            detail.Website = Input.Website;
                            detail.CategoryId = categories.Id;
                            detail.SubCategoryId = subcategories.Id;
                            detail.ActivePassive = Input.ActivePassive;
                            detail.UserId = user.Id;
                            detail.CountViews = null;
                            _db.Tbl_TeamDetails.Add(detail);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/TeamSubCategoriesView");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
