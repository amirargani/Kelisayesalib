using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPClinet.Areas.Identity.Data;
using APPClinet.Areas.Identity.Data.Interfaces;
using APPClinet.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Add - Include
using APPClinet.Areas.Identity.Data.Models.Categories.Church.About;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Events;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Teams;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.News;
using APPClinet.Areas.Identity.Data.Models.Settings.Footer;
using APPClinet.Classes;
using APPClinet.Messages;

namespace APPClinet.Areas.Identity.Data.Services
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly APPClinetDbContext _db;
        private readonly faEmails _faEmails = new faEmails();

        // ctor 2xTab
        public ApplicationUserService(SignInManager<ApplicationUser> signInManager, APPClinetDbContext db)
        {
            _signInManager = signInManager;
            _db = db;
        }

        public string ExistsRoleName(string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == username); // UserId
            var roleId = _db.UserRoles.FirstOrDefault(r => r.UserId == user.Id).RoleId; // RoleId
            return _db.Roles.FirstOrDefault(r => r.Id == roleId).Name; // RoleName
        }

        public bool ExistsPermission(int claimId, string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == username); // UserId
            var claim = _db.RoleClaims.FirstOrDefault(c => c.Id == claimId); // ClaimId
            if (claim == null)
            {
                return false;
            }
            return _db.UserRoles.Any(ur => ur.RoleId == claim.RoleId && ur.UserId == user.Id);
        }

        public bool ExistsRole(string roleId, string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == username); // UserId
            var role = _db.Roles.FirstOrDefault(r => r.Id == roleId); // RoleId
            if (role == null)
            {
                return false;
            }
            return _db.UserRoles.Any(ur => ur.RoleId == role.Id && ur.UserId == user.Id);
        }

        public bool ExistsRoleNames(string roleName, string username)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == username); // UserId
            var role = _db.Roles.FirstOrDefault(r => r.Name == roleName); // RoleId
            if (role == null)
            {
                return false;
            }
            return _db.UserRoles.Any(ur => ur.RoleId == role.Id && ur.UserId == user.Id);
        }

        public void GetLogout()
        {
            _signInManager.SignOutAsync().Dispose();
        }

        public ApplicationUser ExistsUser(string username)
        {
            return _db.Users.FirstOrDefault(u => u.UserName == username);
        }

        public ApplicationUser UpdateUser(ApplicationUser applicationUser, string firstName, string lastName)
        {
            applicationUser.FirstName = firstName;
            applicationUser.LastName = lastName;
            _db.Users.Update(applicationUser);
            _db.SaveChanges();
            return applicationUser;
        }
        // Categories
        public IList<ChurchCategories> OnGetChurchCategories()
        {
            return _db.Tbl_ChurchCategories.ToList();
        }

        public IList<CourseCategories> OnGetCourseCategories()
        {
            return _db.Tbl_CourseCategories.ToList();
        }


        public IList<EventCategories> OnGetEventCategories()
        {
            return _db.Tbl_EventCategories.ToList();
        }

        public IList<GalleryCategories> OnGetGalleryCategories()
        {
            return _db.Tbl_GalleryCategories.ToList();
        }

        public IList<NewCategories> OnGetNewCategories()
        {
            return _db.Tbl_NewCategories.ToList();
        }

        public IList<TeamCategories> OnGetTeamCategories()
        {
            return _db.Tbl_TeamCategories.ToList();
        }

        // Categories

        // SubCategories
        public IList<ChurchSubCategories> OnGetChurchSubCategories()
        {
            return _db.Tbl_ChurchSubCategories.ToList();
        }

        public IList<CourseSubCategories> OnGetCourseSubCategories()
        {
            return _db.Tbl_CourseSubCategories.ToList();
        }

        public IList<EventSubCategories> OnGetEventSubCategories()
        {
            return _db.Tbl_EventSubCategories.ToList();
        }

        public IList<GallerySubCategories> OnGetGallerySubCategories()
        {
            return _db.Tbl_GallerySubCategories.ToList();
        }

        public IList<NewSubCategories> OnGetNewSubCategories()
        {
            return _db.Tbl_NewSubCategories.ToList();
        }

        public IList<TeamSubCategories> OnGetTeamSubCategories()
        {
            return _db.Tbl_TeamSubCategories.ToList();
        }

        // SubCategories

        // Details

        // Details

        // EmailUsers
        public void OnGetSendEventEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink)
        {
            var subcategories = _db.Tbl_EventSubCategories.FirstOrDefault(c => c.Id.Equals(id));
            var categories = _db.Tbl_EventCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
            var detail = _db.Tbl_EventDetails.Where(a => a.SubCategoryId == subcategories.Id && a.CategoryId == categories.Id).FirstOrDefault();
            string pic = null;
            if (detail.Image != null) { pic = "uploads/images/details/events/" + detail.Image; } else { pic = "uploads/images/details/default/default.jpg"; }
            foreach (var item in users)
            {
                //System.Threading.Thread.Sleep(10000);
                //docs.microsoft.com/de-de/dotnet/api/system.threading.tasks.task.delay?view=net-5.0
                Task.Delay(10000); // 10s = 10000ms
                if (ExistsRoleName(item.Email) == "User")
                {
                    _faEmails.SendNewsletterEmail(item.Email, subcategories.TitleSubCategory, callbackUrl, faSmtp.EmailNewsletter, faMessage.TemplateNewsletter, subcategories.TitleSubCategory,
                        detail.Text, "<strong>تاریخ برگزاری</strong><strong>: </strong>" + ShamsiPlugin.ToPeString(Convert.ToDateTime(detail.Countdown), "dddd dd MMMM yyyy"), Convert.ToDateTime(detail.Countdown).ToString("dd/MM/yyyy"),
                        "<strong>زمان برگزاری</strong><strong>: </strong>" + detail.TimeEvent.ToString(), faMessage.TextPageButton, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + pic,                        
                        OnGetSocialNetworks(1).Facebook, OnGetSocialNetworks(1).Twitter, OnGetSocialNetworks(1).Instagram,
                        OnGetSocialNetworks(1).Youtube, OnGetSocialNetworks(1).Telegram, OnGetSocialNetworks(1).Email,
                        OnGetAddress(1).Street, OnGetAddress(1).Number, OnGetAddress(1).PostCode,
                        OnGetAddress(1).City, OnGetAddress(1).Country);
                }
            }
        }
        public void OnGetSendNewsletterCourseEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink)
        {
            var detail = _db.Tbl_CourseDetails.Where(a => a.Id == id).FirstOrDefault();
            var subcategories = _db.Tbl_CourseSubCategories.FirstOrDefault(c => c.Id.Equals(detail.SubCategoryId) && c.CategoryId.Equals(detail.CategoryId));
            var categories = _db.Tbl_CourseCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
            foreach (var item in users)
            {
                //System.Threading.Thread.Sleep(10000);
                //docs.microsoft.com/de-de/dotnet/api/system.threading.tasks.task.delay?view=net-5.0
                Task.Delay(10000); // 10s = 10000ms
                if (ExistsRoleName(item.Email) == "User")
                {
                    _faEmails.SendNewsletterEmail(item.Email, detail.Title, callbackUrl, faSmtp.EmailNewsletter, faMessage.TemplateNewsletter, subcategories.TitleSubCategory + "<br />" + detail.Title,
                        detail.Text, ShamsiPlugin.ToPeString(Convert.ToDateTime(detail.Date), "dddd dd MMMM yyyy"), Convert.ToDateTime(detail.Date).ToString("dd/MM/yyyy"),
                        null, faMessage.TextPageButton, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + "uploads/images/details/courses/" + detail.ImageLink,
                        OnGetSocialNetworks(1).Facebook, OnGetSocialNetworks(1).Twitter, OnGetSocialNetworks(1).Instagram,
                        OnGetSocialNetworks(1).Youtube, OnGetSocialNetworks(1).Telegram, OnGetSocialNetworks(1).Email,
                        OnGetAddress(1).Street, OnGetAddress(1).Number, OnGetAddress(1).PostCode,
                        OnGetAddress(1).City, OnGetAddress(1).Country);
                }
            }
        }

        public void OnGetSendNewsletterGalleryEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink)
        {
            var detail = _db.Tbl_GalleryDetails.Where(a => a.Id == id).FirstOrDefault();
            var subcategories = _db.Tbl_GallerySubCategories.FirstOrDefault(c => c.Id.Equals(detail.SubCategoryId) && c.CategoryId.Equals(detail.CategoryId));
            var categories = _db.Tbl_GalleryCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
            foreach (var item in users)
            {
                //System.Threading.Thread.Sleep(10000);
                //docs.microsoft.com/de-de/dotnet/api/system.threading.tasks.task.delay?view=net-5.0
                Task.Delay(10000); // 10s = 10000ms
                if (ExistsRoleName(item.Email) == "User")
                {
                    _faEmails.SendNewsletterEmail(item.Email, detail.Title, callbackUrl, faSmtp.EmailNewsletter, faMessage.TemplateNewsletter, subcategories.TitleSubCategory + "<br />" + detail.Title,
                        detail.Text, ShamsiPlugin.ToPeString(Convert.ToDateTime(detail.Date), "dddd dd MMMM yyyy"), Convert.ToDateTime(detail.Date).ToString("dd/MM/yyyy"),
                        null, faMessage.TextPageButton, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + "uploads/images/details/galleries/" + detail.ImageLink,
                        OnGetSocialNetworks(1).Facebook, OnGetSocialNetworks(1).Twitter, OnGetSocialNetworks(1).Instagram,
                        OnGetSocialNetworks(1).Youtube, OnGetSocialNetworks(1).Telegram, OnGetSocialNetworks(1).Email,
                        OnGetAddress(1).Street, OnGetAddress(1).Number, OnGetAddress(1).PostCode,
                        OnGetAddress(1).City, OnGetAddress(1).Country);
                }
            }
        }

        public void OnGetSendNewsletterNewEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink)
        {
            var detail = _db.Tbl_NewDetails.Where(a => a.Id == id).FirstOrDefault();
            var subcategories = _db.Tbl_NewSubCategories.FirstOrDefault(c => c.Id.Equals(detail.SubCategoryId) && c.CategoryId.Equals(detail.CategoryId));
            var categories = _db.Tbl_NewCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
            foreach (var item in users)
            {
                //System.Threading.Thread.Sleep(10000);
                //docs.microsoft.com/de-de/dotnet/api/system.threading.tasks.task.delay?view=net-5.0
                Task.Delay(10000); // 10s = 10000ms
                if (ExistsRoleName(item.Email) == "User")
                {
                    _faEmails.SendNewsletterEmail(item.Email, detail.Title, callbackUrl, faSmtp.EmailNewsletter, faMessage.TemplateNewsletter, subcategories.TitleSubCategory + "<br />" + detail.Title,
                        detail.Text, ShamsiPlugin.ToPeString(Convert.ToDateTime(detail.Date), "dddd dd MMMM yyyy"), Convert.ToDateTime(detail.Date).ToString("dd/MM/yyyy"),
                        null, faMessage.TextPageButton, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + "uploads/images/details/news/" + detail.ImageLink,
                        OnGetSocialNetworks(1).Facebook, OnGetSocialNetworks(1).Twitter, OnGetSocialNetworks(1).Instagram,
                        OnGetSocialNetworks(1).Youtube, OnGetSocialNetworks(1).Telegram, OnGetSocialNetworks(1).Email,
                        OnGetAddress(1).Street, OnGetAddress(1).Number, OnGetAddress(1).PostCode,
                        OnGetAddress(1).City, OnGetAddress(1).Country);
                }
            }
        }
        public void OnGetSendNewsletterCourseVideoEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink, string videotitle)
        {
            var detail = _db.Tbl_CourseDetails.Where(a => a.Id == id).FirstOrDefault();
            var subcategories = _db.Tbl_CourseSubCategories.FirstOrDefault(c => c.Id.Equals(detail.SubCategoryId) && c.CategoryId.Equals(detail.CategoryId));
            var categories = _db.Tbl_CourseCategories.FirstOrDefault(c => c.Id.Equals(subcategories.CategoryId));
            foreach (var item in users)
            {
                //System.Threading.Thread.Sleep(10000);
                //docs.microsoft.com/de-de/dotnet/api/system.threading.tasks.task.delay?view=net-5.0
                Task.Delay(10000); // 10s = 10000ms
                if (ExistsRoleName(item.Email) == "User")
                {
                    _faEmails.SendNewsletterEmail(item.Email, videotitle, callbackUrl, faSmtp.EmailNewsletter, faMessage.TemplateNewsletter, detail.Title + "<br />" + "<strong>ویدیو جدید</strong><strong>: </strong>" + videotitle,
                        detail.Text, ShamsiPlugin.ToPeString(Convert.ToDateTime(detail.Date), "dddd dd MMMM yyyy"), Convert.ToDateTime(detail.Date).ToString("dd/MM/yyyy"),
                        null, faMessage.TextPageButton, urlpagelink, urlpagelink + faMessage.WebsiteMainzLogo, urlpagelink + "uploads/images/details/courses/" + detail.ImageLink,
                        OnGetSocialNetworks(1).Facebook, OnGetSocialNetworks(1).Twitter, OnGetSocialNetworks(1).Instagram,
                        OnGetSocialNetworks(1).Youtube, OnGetSocialNetworks(1).Telegram, OnGetSocialNetworks(1).Email,
                        OnGetAddress(1).Street, OnGetAddress(1).Number, OnGetAddress(1).PostCode,
                        OnGetAddress(1).City, OnGetAddress(1).Country);
                }
            }
        }
        // EmailUsers

        // _FooterFixed.cshtml
        public Address OnGetAddress(int id)
        {
            return _db.Tbl_Address.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public SocialNetworks OnGetSocialNetworks(int id)
        {
            return _db.Tbl_SocialNetworks.Where(a => a.Id.Equals(id)).FirstOrDefault();
        }
        // _FooterFixed.cshtml

        // StartNew.cshtml
        public IList<EventDetails> OnGetEventDetails()
        {
            return _db.Tbl_EventDetails.ToList();
        }

        public string ExistsEventDetailsTitlecategory(int id, string titlecategory)
        {
            var categories = _db.Tbl_EventCategories.FirstOrDefault(u => u.Id == id);
            if (categories == null)
            {
                return null;
            }
            return categories.TitleCategory;
        }

        public string ExistsEventDetailsTitlesubcategory(int id, string titlesubcategory)
        {
            var subcategories = _db.Tbl_EventSubCategories.FirstOrDefault(u => u.Id == id);
            var categories = _db.Tbl_EventCategories.FirstOrDefault(u => u.Id == id);
            if (subcategories == null)
            {
                return null;
            }
            return subcategories.TitleSubCategory;
        }

        public IList<CourseDetails> OnGetCourseDetails()
        {
            return _db.Tbl_CourseDetails.ToList();
        }

        public IList<GalleryDetails> OnGetGalleryDetails()
        {
            return _db.Tbl_GalleryDetails.ToList();
        }

        public IList<NewDetails> OnGetNewDetails()
        {
            return _db.Tbl_NewDetails.ToList();
        }

        // StartNew.cshtml

        // ASP.NET Core Razor Pages - Pagination
        public ChurchPages OnGetChurchPagesCategories(int pageid = 1)
        {
            IQueryable<ChurchCategories> result = _db.Tbl_ChurchCategories;
            int take = 25;
            int skip = (pageid - 1) * take;

            ChurchPages churchPages = new ChurchPages();
            churchPages.CurrentPage = pageid;
            churchPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            churchPages.churchCategories = result.OrderBy(u => u.Date).OrderBy(u => u.Position).Skip(skip).Take(take).ToList();
            return churchPages;
        }

        public ChurchPages OnGetChurchPagesSubCategories(int pageid = 1)
        {
            //IQueryable<ChurchSubCategories> result = _db.Tbl_ChurchSubCategories;
            IList<ChurchSubCategories> result = (IList<ChurchSubCategories>)_db.Tbl_ChurchSubCategories.Include(c => c.Tbl_ChurchCategories).Include(d => d.Tbl_ChurchDetails).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            ChurchPages churchPages = new ChurchPages();
            churchPages.CurrentPage = pageid;
            churchPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            churchPages.churchSubCategories = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return churchPages;
        }

        public ChurchPages OnGetChurchPagesDetails(int pageid = 1)
        {
            //IQueryable<ChurchDetails> result = _db.Tbl_ChurchDetails;
            IList<ChurchDetails> result = (IList<ChurchDetails>)_db.Tbl_ChurchDetails.Include(s => s.Tbl_ChurchSubCategories).Include(c => c.Tbl_ChurchCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            ChurchPages churchPages = new ChurchPages();
            churchPages.CurrentPage = pageid;
            churchPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            churchPages.churchDetails = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return churchPages;
        }

        public CoursePages OnGetCoursePagesCategories(int pageid = 1)
        {
            IQueryable<CourseCategories> result = _db.Tbl_CourseCategories;
            int take = 25;
            int skip = (pageid - 1) * take;

            CoursePages coursePages = new CoursePages();
            coursePages.CurrentPage = pageid;
            coursePages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            coursePages.courseCategories = result.OrderBy(u => u.Date).OrderBy(u => u.Position).Skip(skip).Take(take).ToList();
            return coursePages;
        }

        public CoursePages OnGetCoursePagesSubCategories(int pageid = 1)
        {
            //IQueryable<CourseSubCategories> result = _db.Tbl_CourseSubCategories;
            IList<CourseSubCategories> result = (IList<CourseSubCategories>)_db.Tbl_CourseSubCategories.Include(c => c.Tbl_CourseCategories).Include(d => d.Tbl_CourseDetails).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            CoursePages coursePages = new CoursePages();
            coursePages.CurrentPage = pageid;
            coursePages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            coursePages.courseSubCategories = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return coursePages;
        }

        public CoursePages OnGetCoursePagesDetails(int pageid = 1)
        {
            //IQueryable<CourseDetails> result = _db.Tbl_CourseDetails;
            IList<CourseDetails> result = (IList<CourseDetails>)_db.Tbl_CourseDetails.Include(s => s.Tbl_CourseSubCategories).Include(c => c.Tbl_CourseCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            CoursePages coursePages = new CoursePages();
            coursePages.CurrentPage = pageid;
            coursePages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            coursePages.courseDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return coursePages;
        }

        public CoursePages OnGetSearchCoursePagesDetails(string titlesubcategory, int pageid = 1)
        {
            //IQueryable<CourseDetails> result = _db.Tbl_CourseDetails;
            IList<CourseDetails> result = (IList<CourseDetails>)_db.Tbl_CourseDetails.Where(c => c.Tbl_CourseSubCategories.TitleSubCategory.Equals(titlesubcategory)).Include(s => s.Tbl_CourseSubCategories).Include(c => c.Tbl_CourseCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            CoursePages coursePages = new CoursePages();
            coursePages.CurrentPage = pageid;
            coursePages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            coursePages.courseDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return coursePages;
        }

        public CoursePages OnGetCoursePagesVideos(int id, int pageid = 1)
        {
            //IQueryable<CourseImages> result = _db.Tbl_CourseImages;
            IList<CourseVideos> result = (IList<CourseVideos>)_db.Tbl_CourseVideos.Where(i => i.DetailId.Equals(id)).Include(s => s.Tbl_CourseDetails).Include(s => s.Tbl_CourseSubCategories).Include(c => c.Tbl_CourseCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            CoursePages coursePages = new CoursePages();
            coursePages.CurrentPage = pageid;
            coursePages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            coursePages.courseVideos = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return coursePages;
        }

        public EventPages OnGetEventPagesCategories(int pageid = 1)
        {
            IQueryable<EventCategories> result = _db.Tbl_EventCategories;
            int take = 25;
            int skip = (pageid - 1) * take;

            EventPages eventPages = new EventPages();
            eventPages.CurrentPage = pageid;
            eventPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            eventPages.eventCategories = result.OrderBy(u => u.Date).OrderBy(u => u.Position).Skip(skip).Take(take).ToList();
            return eventPages;
        }

        public EventPages OnGetEventPagesSubCategories(int pageid = 1)
        {
            //IQueryable<EventSubCategories> result = _db.Tbl_EventSubCategories;
            IList<EventSubCategories> result = (IList<EventSubCategories>)_db.Tbl_EventSubCategories.Include(c => c.Tbl_EventCategories).Include(d => d.Tbl_EventDetails).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            EventPages eventPages = new EventPages();
            eventPages.CurrentPage = pageid;
            eventPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            eventPages.eventSubCategories = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return eventPages;
        }

        public EventPages OnGetEventPagesDetails(int pageid = 1)
        {
            //IQueryable<EventDetails> result = _db.Tbl_EventDetails;
            IList<EventDetails> result = (IList<EventDetails>)_db.Tbl_EventDetails.Include(s => s.Tbl_EventSubCategories).Include(c => c.Tbl_EventCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            EventPages eventPages = new EventPages();
            eventPages.CurrentPage = pageid;
            eventPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            eventPages.eventDetails = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return eventPages;
        }

        public GalleryPages OnGetGalleryPagesCategories(int pageid = 1)
        {
            IQueryable<GalleryCategories> result = _db.Tbl_GalleryCategories;
            int take = 25;
            int skip = (pageid - 1) * take;

            GalleryPages galleryPages = new GalleryPages();
            galleryPages.CurrentPage = pageid;
            galleryPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            galleryPages.galleryCategories = result.OrderBy(u => u.Date).OrderBy(u => u.Position).Skip(skip).Take(take).ToList();
            return galleryPages;
        }

        public GalleryPages OnGetGalleryPagesSubCategories(int pageid = 1)
        {
            //IQueryable<GallerySubCategories> result = _db.Tbl_GallerySubCategories;
            IList<GallerySubCategories> result = (IList<GallerySubCategories>)_db.Tbl_GallerySubCategories.Include(c => c.Tbl_GalleryCategories).Include(d => d.Tbl_GalleryDetails).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            GalleryPages galleryPages = new GalleryPages();
            galleryPages.CurrentPage = pageid;
            galleryPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            galleryPages.gallerySubCategories = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return galleryPages;
        }

        public GalleryPages OnGetGalleryPagesDetails(int pageid = 1)
        {
            //IQueryable<GalleryDetails> result = _db.Tbl_GalleryDetails;
            IList<GalleryDetails> result = (IList<GalleryDetails>)_db.Tbl_GalleryDetails.Include(s => s.Tbl_GallerySubCategories).Include(c => c.Tbl_GalleryCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            GalleryPages galleryPages = new GalleryPages();
            galleryPages.CurrentPage = pageid;
            galleryPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            galleryPages.galleryDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return galleryPages;
        }

        public GalleryPages OnGetSearchGalleryPagesDetails(string titlesubcategory, int pageid = 1)
        {
            //IQueryable<GalleryDetails> result = _db.Tbl_GalleryDetails;
            IList<GalleryDetails> result = (IList<GalleryDetails>)_db.Tbl_GalleryDetails.Where(c => c.Tbl_GallerySubCategories.TitleSubCategory.Equals(titlesubcategory)).Include(s => s.Tbl_GallerySubCategories).Include(c => c.Tbl_GalleryCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            GalleryPages galleryPages = new GalleryPages();
            galleryPages.CurrentPage = pageid;
            galleryPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            galleryPages.galleryDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return galleryPages;
        }

        public GalleryPages OnGetGalleryPagesImages(int id, int pageid = 1)
        {
            //IQueryable<GalleryImages> result = _db.Tbl_GalleryImages;
            IList<GalleryImages> result = (IList<GalleryImages>)_db.Tbl_GalleryImages.Where(i => i.DetailId.Equals(id)).Include(s => s.Tbl_GalleryDetails).Include(s => s.Tbl_GallerySubCategories).Include(c => c.Tbl_GalleryCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            GalleryPages galleryPages = new GalleryPages();
            galleryPages.CurrentPage = pageid;
            galleryPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            galleryPages.galleryImages = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return galleryPages;
        }

        public NewPages OnGetNewPagesCategories(int pageid = 1)
        {
            IQueryable<NewCategories> result = _db.Tbl_NewCategories;
            int take = 25;
            int skip = (pageid - 1) * take;

            NewPages newPages = new NewPages();
            newPages.CurrentPage = pageid;
            newPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            newPages.newCategories = result.OrderBy(u => u.Date).OrderBy(u => u.Position).Skip(skip).Take(take).ToList();
            return newPages;
        }

        public NewPages OnGetNewPagesSubCategories(int pageid = 1)
        {
            //IQueryable<NewSubCategories> result = _db.Tbl_NewSubCategories;
            IList<NewSubCategories> result = (IList<NewSubCategories>)_db.Tbl_NewSubCategories.Include(c => c.Tbl_NewCategories).Include(d => d.Tbl_NewDetails).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            NewPages newPages = new NewPages();
            newPages.CurrentPage = pageid;
            newPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            newPages.newSubCategories = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return newPages;
        }

        public NewPages OnGetNewPagesDetails(int pageid = 1)
        {
            //IQueryable<NewDetails> result = _db.Tbl_NewDetails;
            IList<NewDetails> result = (IList<NewDetails>)_db.Tbl_NewDetails.Include(s => s.Tbl_NewSubCategories).Include(c => c.Tbl_NewCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            NewPages newPages = new NewPages();
            newPages.CurrentPage = pageid;
            newPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            newPages.newDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return newPages;
        }

        public NewPages OnGetSearchNewPagesDetails(string titlesubcategory, int pageid = 1)
        {
            //IQueryable<NewDetails> result = _db.Tbl_NewDetails;
            IList<NewDetails> result = (IList<NewDetails>)_db.Tbl_NewDetails.Where(c => c.Tbl_NewSubCategories.TitleSubCategory.Equals(titlesubcategory)).Include(s => s.Tbl_NewSubCategories).Include(c => c.Tbl_NewCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            NewPages newPages = new NewPages();
            newPages.CurrentPage = pageid;
            newPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            newPages.newDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return newPages;
        }

        public NewPages OnGetNewPagesImages(int id, int pageid = 1)
        {
            //IQueryable<NewImages> result = _db.Tbl_NewImages;
            IList<NewImages> result = (IList<NewImages>)_db.Tbl_NewImages.Where(i => i.DetailId.Equals(id)).Include(s => s.Tbl_NewDetails).Include(s => s.Tbl_NewSubCategories).Include(c => c.Tbl_NewCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            NewPages newPages = new NewPages();
            newPages.CurrentPage = pageid;
            newPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            newPages.newImages = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return newPages;
        }

        public TeamPages OnGetTeamPagesCategories(int pageid = 1)
        {
            IQueryable<TeamCategories> result = _db.Tbl_TeamCategories;
            int take = 25;
            int skip = (pageid - 1) * take;

            TeamPages teamPages = new TeamPages();
            teamPages.CurrentPage = pageid;
            teamPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            teamPages.teamCategories = result.OrderBy(u => u.Date).OrderBy(u => u.Position).Skip(skip).Take(take).ToList();
            return teamPages;
        }

        public TeamPages OnGetTeamPagesSubCategories(int pageid = 1)
        {
            //IQueryable<TeamSubCategories> result = _db.Tbl_TeamSubCategories;
            IList<TeamSubCategories> result = (IList<TeamSubCategories>)_db.Tbl_TeamSubCategories.Include(c => c.Tbl_TeamCategories).Include(d => d.Tbl_TeamDetails).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            TeamPages teamPages = new TeamPages();
            teamPages.CurrentPage = pageid;
            teamPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            teamPages.teamSubCategories = result.OrderBy(u => u.Date).OrderBy(u => u.CategoryId).Skip(skip).Take(take).ToList();
            return teamPages;
        }

        public TeamPages OnGetTeamPagesDetails(int pageid = 1)
        {
            //IQueryable<TeamDetails> result = _db.Tbl_TeamDetails;
            IList<TeamDetails> result = (IList<TeamDetails>)_db.Tbl_TeamDetails.Include(s => s.Tbl_TeamSubCategories).Include(c => c.Tbl_TeamCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            TeamPages teamPages = new TeamPages();
            teamPages.CurrentPage = pageid;
            teamPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            teamPages.teamDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return teamPages;
        }

        public TeamPages OnGetSearchTeamPagesDetails(string titlesubcategory, int pageid = 1)
        {

            //IQueryable<TeamDetails> result = _db.Tbl_TeamDetails;
            IList<TeamDetails> result = (IList<TeamDetails>)_db.Tbl_TeamDetails.Where(c => c.Tbl_TeamSubCategories.TitleSubCategory.Equals(titlesubcategory)).Include(s => s.Tbl_TeamSubCategories).Include(c => c.Tbl_TeamCategories).Include(u => u.AspNetUsers).ToList();
            int take = 25;
            int skip = (pageid - 1) * take;

            TeamPages teamPages = new TeamPages();
            teamPages.CurrentPage = pageid;
            teamPages.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            teamPages.teamDetails = result.OrderByDescending(u => u.Date).Skip(skip).Take(take).ToList();
            return teamPages;
        }
        // ASP.NET Core Razor Pages - Pagination
    }
}

