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
using APPClinet.Areas.Identity.Data.Models.Categories.Church.About;
using Microsoft.AspNetCore.Hosting;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin.Details
{
    [RoleAttribute("Admin")]
    public class ChurchDetailAddModel : PageModel
    {
        private readonly APPClinetDbContext _db;
        private readonly IWebHostEnvironment _environment;
        private readonly CodeGenerators _codeGenerators = new CodeGenerators();

        public ChurchDetailAddModel(APPClinetDbContext db,
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
            [Display(Name = faMessage.Text)]
            public string Text { get; set; }
            public string Image { get; set; }
            public int CategoryId { get; set; }
            public string TitleCategory { get; set; }
            public int SubCategoryId { get; set; }
            public string TitleSubCategory { get; set; }
            public bool ActivePassive { get; set; }
            public string UserId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var subcategories = await _db.Tbl_ChurchSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
            var categories = await _db.Tbl_ChurchCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
            var detail = _db.Tbl_ChurchDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
            if (detail != null)
            {
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/ChurchSubCategoriesView");
            }
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

        public async Task<IActionResult> OnPostAsync(int id, IFormFile Image)
        {
            if (Input.Text != null)
            {
                ChurchDetails detail = new ChurchDetails();
                var subcategories = await _db.Tbl_ChurchSubCategories.FirstOrDefaultAsync(c => c.Id.Equals(id));
                var categories = await _db.Tbl_ChurchCategories.FirstOrDefaultAsync(c => c.Id.Equals(subcategories.CategoryId));
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName.Equals(User.Identity.Name));
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        if (Image.Length <= 1048576) // 1 MB -> 1048576 Bit
                        {
                            string codeimage = _codeGenerators.CodeImage();
                            var uploads = Path.Combine(_environment.WebRootPath, "uploads\\images\\details\\church");
                            //var directoryuploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\images\\details\\church");
                            string filename = "church-" + codeimage + "." + Image.FileName.Split('.').Last();
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                Image.CopyTo(fileStream);
                            }
                            int count = _db.Tbl_ChurchDetails.Count();
                            detail.Date = DateTime.Now;
                            detail.Position = count + 1;
                            detail.Text = Input.Text;
                            detail.Image = filename;
                            detail.CategoryId = categories.Id;
                            detail.SubCategoryId = subcategories.Id;
                            detail.ActivePassive = Input.ActivePassive;
                            detail.UserId = user.Id;
                            detail.CountViews = null;
                            _db.Tbl_ChurchDetails.Add(detail);
                            await _db.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    int count = _db.Tbl_ChurchDetails.Count();
                    detail.Date = DateTime.Now;
                    detail.Position = count + 1;
                    detail.Text = Input.Text;
                    detail.Image = null;
                    detail.CategoryId = categories.Id;
                    detail.SubCategoryId = subcategories.Id;
                    detail.ActivePassive = Input.ActivePassive;
                    detail.UserId = user.Id;
                    detail.CountViews = null;
                    _db.Tbl_ChurchDetails.Add(detail);
                    await _db.SaveChangesAsync();
                }
                return Redirect(Url.Content("~/") + "Identity/Account/Dashboard/Admin/SubCategories/ChurchSubCategoriesView");
            }
            else
            {
                await OnGetAsync(id);
                return Page();
            }
        }
    }
}
