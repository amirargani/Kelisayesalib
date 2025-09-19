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
    public class CourseVideoEditModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();

        public CourseVideoEditModel(APPClinetDbContext db,
            IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
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
            var images = _db.Tbl_CourseVideos.Where(a => a.Id.Equals(id)).FirstOrDefault();
            if (images == null)
            {
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseVideosView?id=" + images.DetailId);
            }
            var details = await _db.Tbl_CourseDetails.FirstOrDefaultAsync(c => c.Id.Equals(images.DetailId));
            var subcategories = await _db.Tbl_CourseSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(details.SubCategoryId));
            var categories = await _db.Tbl_CourseCategories.FirstOrDefaultAsync(c => c.Id.Equals(details.CategoryId));
            var video = _db.Tbl_CourseVideos.Where(a => a.Id.Equals(details.Id)).FirstOrDefault();
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
                TitleCategory = categories.TitleCategory,
                TitleSubCategory = subcategories.TitleSubCategory,
                DetailId = details.Id,
                TitleDetail = details.Title,
                Title = video.Title,
                ActivePassive = images.ActivePassive
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile Video)
        {
            if (Input.Title != null)
            {
                var video = _db.Tbl_CourseVideos.Where(a => a.Id.Equals(id)).FirstOrDefault();
                var details = await _db.Tbl_CourseDetails.FirstOrDefaultAsync(c => c.Id.Equals(video.DetailId));
                var subcategories = await _db.Tbl_CourseSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(details.SubCategoryId));
                var categories = await _db.Tbl_CourseCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (Video != null)
                {
                    if (Video.ContentType == "video/mp4")
                    {
                        if (Video.Length <= 367001600) // 350 MB -> 367001600 Bit
                        {
                            string codeimage = _codeGenerators.CodeImage();
                            var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\courses");
                            //var directoryuploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\images\\details\\courses");
                            var deleteimage = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\courses\\" + video.Video);
                            if (System.IO.File.Exists(deleteimage))
                            {
                                System.IO.File.Delete(deleteimage);
                            }
                            string filename = "coursesvideo-" + codeimage + "." + Video.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                Video.CopyTo(fileStream);
                                fileStream.Flush();
                            }
                            video.Title = Input.Title;
                            video.Video = filename;
                            video.ActivePassive = Input.ActivePassive;
                            video.UserId = user.Id;
                            _db.Tbl_CourseVideos.Update(video);
                            await _db.SaveChangesAsync();
                        }
                    }
                    return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseVideosView?id=" + _db.Tbl_CourseVideos.Where(a => a.Id.Equals(id)).FirstOrDefault().DetailId);
                }
                else
                {
                    video.Title = Input.Title;
                    video.ActivePassive = Input.ActivePassive;
                    video.UserId = user.Id;
                    _db.Tbl_CourseVideos.Update(video);
                    await _db.SaveChangesAsync();
                    return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/CourseVideosView?id=" + _db.Tbl_CourseVideos.Where(a => a.Id.Equals(id)).FirstOrDefault().DetailId);
                }
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
