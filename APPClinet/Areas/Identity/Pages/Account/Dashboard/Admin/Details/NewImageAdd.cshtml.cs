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

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class NewImageAddModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();

        public NewImageAddModel(APPClinetDbContext db,
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
            [Display(Name = faMessage.Image)]
            public string Image { get; set; }
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
            var details = await _db.Tbl_NewDetails.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var subcategories = await _db.Tbl_NewSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(details.SubCategoryId));
            var categories = await _db.Tbl_NewCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));

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

        public async Task<IActionResult> OnPostAsync(int id, IFormFile Image)
        {
            if (Image != null)
            {
                if (Image.ContentType == "image/jpeg")
                {
                    if (Image.Length <= 1048576) // 1 MB -> 1048576 Bit
                    {
                        NewImages image = new NewImages();
                        var details = await _db.Tbl_NewDetails.FirstOrDefaultAsync(c => c.Id.Equals(id));
                        var subcategories = await _db.Tbl_NewSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(details.SubCategoryId));
                        var categories = await _db.Tbl_NewCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
                        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                        string codeimage = _codeGenerators.CodeImage();
                        var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\news");
                        //var directoryuploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\images\\details\\news");
                        string filename = "newsimage-" + codeimage + "." + Image.FileName.Split('.').Last();
                        using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            Image.CopyTo(fileStream);
                        }
                        int count = _db.Tbl_NewImages.Count();
                        image.Date = DateTime.Now;
                        image.Position = count + 1;
                        image.Image = filename;
                        image.CategoryId = categories.Id;
                        image.SubCategoryId = subcategories.Id;
                        image.DetailId = details.Id;
                        image.ActivePassive = Input.ActivePassive;
                        image.UserId = user.Id;
                        image.CountViews = null;
                        _db.Tbl_NewImages.Add(image);
                        await _db.SaveChangesAsync();
                    }
                }
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/Details/NewDetailsView");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
