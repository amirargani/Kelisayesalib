using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.About;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Events;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Teams;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.News;
using APPClinet.Areas.Identity.Data.Models.Settings.Footer;

namespace APPClinet.Areas.Identity.Data.Interfaces
{
    public interface IApplicationUser
    {
        string ExistsRoleName(string username);
        bool ExistsRole(string roleId, string username);
        bool ExistsRoleNames(string roleName, string username);
        bool ExistsPermission(int claimId, string username);
        void GetLogout();
        ApplicationUser ExistsUser(string username);
        ApplicationUser UpdateUser(ApplicationUser applicationUser, string firstName, string lastName);
        // Categories
        IList<ChurchCategories> OnGetChurchCategories();
        IList<CourseCategories> OnGetCourseCategories();
        IList<EventCategories> OnGetEventCategories();
        IList<GalleryCategories> OnGetGalleryCategories();
        IList<NewCategories> OnGetNewCategories();
        IList<TeamCategories> OnGetTeamCategories();
        // Categories

        // SubCategories
        IList<ChurchSubCategories> OnGetChurchSubCategories();
        IList<CourseSubCategories> OnGetCourseSubCategories();
        IList<EventSubCategories> OnGetEventSubCategories();
        IList<GallerySubCategories> OnGetGallerySubCategories();
        IList<NewSubCategories> OnGetNewSubCategories();
        IList<TeamSubCategories> OnGetTeamSubCategories();
        // SubCategories

        // Details

        // Details

        // EmailUsers
        void OnGetSendEventEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink);
        void OnGetSendNewsletterCourseEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink);
        void OnGetSendNewsletterGalleryEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink);
        void OnGetSendNewsletterNewEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink);
        void OnGetSendNewsletterCourseVideoEmailUsers(int id, IList<ApplicationUser> users, string callbackUrl, string urlpagelink, string videotitle);
        // EmailUsers

        // _FooterFixed.cshtml
        Address OnGetAddress(int id);
        SocialNetworks OnGetSocialNetworks(int id);
        // _FooterFixed.cshtml

        // StartNew.cshtml
        IList<EventDetails> OnGetEventDetails();
        string ExistsEventDetailsTitlecategory(int id, string titlecategory);
        string ExistsEventDetailsTitlesubcategory(int id, string titlesubcategory);
        IList<CourseDetails> OnGetCourseDetails();
        IList<GalleryDetails> OnGetGalleryDetails();
        IList<NewDetails> OnGetNewDetails();
        // StartNew.cshtml

        // ASP.NET Core Razor Pages - Pagination
        ChurchPages OnGetChurchPagesCategories(int pageid = 1);
        ChurchPages OnGetChurchPagesSubCategories(int pageid = 1);
        ChurchPages OnGetChurchPagesDetails(int pageid = 1);
        CoursePages OnGetCoursePagesCategories(int pageid = 1);
        CoursePages OnGetCoursePagesSubCategories(int pageid = 1);
        CoursePages OnGetCoursePagesDetails(int pageid = 1);
        CoursePages OnGetSearchCoursePagesDetails(string titlesubcategory, int pageid = 1);
        CoursePages OnGetCoursePagesVideos(int id, int pageid = 1);
        EventPages OnGetEventPagesCategories(int pageid = 1);
        EventPages OnGetEventPagesSubCategories(int pageid = 1);
        EventPages OnGetEventPagesDetails(int pageid = 1);
        GalleryPages OnGetGalleryPagesCategories(int pageid = 1);
        GalleryPages OnGetGalleryPagesSubCategories(int pageid = 1);
        GalleryPages OnGetGalleryPagesDetails(int pageid = 1);
        GalleryPages OnGetSearchGalleryPagesDetails(string titlesubcategory, int pageid = 1);
        GalleryPages OnGetGalleryPagesImages(int id, int pageid = 1);
        NewPages OnGetNewPagesCategories(int pageid = 1);
        NewPages OnGetNewPagesSubCategories(int pageid = 1);
        NewPages OnGetNewPagesDetails(int pageid = 1);
        NewPages OnGetSearchNewPagesDetails(string titlesubcategory, int pageid = 1);
        NewPages OnGetNewPagesImages(int id, int pageid = 1);
        TeamPages OnGetTeamPagesCategories(int pageid = 1);
        TeamPages OnGetTeamPagesSubCategories(int pageid = 1);
        TeamPages OnGetTeamPagesDetails(int pageid = 1);
        TeamPages OnGetSearchTeamPagesDetails(string titlesubcategory, int pageid = 1);
        // ASP.NET Core Razor Pages - Pagination
    }
}
