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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses;
using Microsoft.AspNetCore.Hosting;
using APPClinet.Areas.Identity.Data.Interfaces;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class CourseVideoAddModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();
        private readonly IApplicationUser _iuser;

        public CourseVideoAddModel(APPClinetDbContext db,
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
        public int DetailId { get; set; }
        public string TitleDetail { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [StringLength(50, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.Title)]
            public string Title { get; set; }
            [Required(ErrorMessage = faMessage.RequiredMsg)]
            [Display(Name = faMessage.Video)]
            public string Video { get; set; }
            public int CategoryId { get; set; }
            public string TitleCategory { get; set; }
            public int SubCategoryId { get; set; }
            public string TitleSubCategory { get; set; }
            public int DetailId { get; set; }
            public string TitleDetail { get; set; }
            public bool ActivePassive { get; set; }
            public string UserId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var details = await _db.Tbl_CourseDetails.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var subcategories = await _db.Tbl_CourseSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(details.SubCategoryId));
            var categories = await _db.Tbl_CourseCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));

            CategoryId = categories.Id;
            TitleCategory = categories.TitleCategory;
            SubCategoryId = subcategories.Id;
            TitleSubCategory = subcategories.TitleSubCategory;
            DetailId = details.Id;
            TitleDetail = details.Title;
            Input = new InputModel
            {
                CategoryId = categories.Id,
                SubCategoryId = subcategories.Id,
                DetailId = details.Id
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile Video)
        {
            if (Video != null)
            {
                CourseVideos video = new CourseVideos();
                if (Video.ContentType == "video/mp4")
                {
                    if (Video.Length <= 367001600) // 350 MB -> 367001600 Bit
                    {
                        //CourseVideos video = new CourseVideos();
                        var details = await _db.Tbl_CourseDetails.FirstOrDefaultAsync(c => c.Id.Equals(id));
                        var subcategories = await _db.Tbl_CourseSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(details.SubCategoryId));
                        var categories = await _db.Tbl_CourseCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
                        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                        string codeimage = _codeGenerators.CodeImage();
                        var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\courses");
                        //var directoryuploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\images\\details\\courses");
                        string filename = "coursesvideo-" + codeimage + "." + Video.FileName.Split('.').Last();
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            Video.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                        int count = _db.Tbl_CourseVideos.Count();
                        video.Date = DateTime.Now;
                        video.Position = count + 1;
                        video.Title = Input.Title;
                        video.Video = filename;
                        video.CategoryId = categories.Id;
                        video.SubCategoryId = subcategories.Id;
                        video.DetailId = details.Id;
                        video.ActivePassive = Input.ActivePassive;
                        video.UserId = user.Id;
                        video.CountViews = null;
                        _db.Tbl_CourseVideos.Add(video);
                        await _db.SaveChangesAsync();
                    }
                }
                // Send Emails
                var detail = await _db.Tbl_CourseDetails.FirstOrDefaultAsync(c => c.Id.Equals(id));
                string callbackUrl = Url.PageLink().Replace("/Identity/Account/Dashboard/Admin/Details/CourseVideoAdd", "/Identity/Search/Details/CourseDetailsInfoView?id=" + detail.Id + "&title=" + detail.Title.Replace(" ", "§").ToString()).ToString();
                string urlpagelink = Url.PageLink().Replace("/Identity/Account/Dashboard/Admin/Details/CourseVideoAdd", "/").ToString();
                var users = await _db.Users.Where(u => u.EmailConfirmed == true && u.Newsletter == true).ToListAsync();
                _iuser.OnGetSendNewsletterCourseVideoEmailUsers(detail.Id, users, callbackUrl, urlpagelink, video.Title);
                // Send Emails
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseDetailsView");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
