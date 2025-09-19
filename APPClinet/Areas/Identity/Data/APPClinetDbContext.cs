using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPClinet.Areas.Identity.Data;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.About;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Events;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.News;
using APPClinet.Areas.Identity.Data.Models.Categories.Church.Teams;
using APPClinet.Areas.Identity.Data.Models.Settings.Footer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APPClinet.Data
{
    public class APPClinetDbContext : IdentityDbContext<ApplicationUser>
    // stackoverflow.com/questions/60952643/adding-a-custom-column-to-aspnetuserroles-table-in-asp-net-core-3-0-asp-net-co
    {
        public APPClinetDbContext(DbContextOptions<APPClinetDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}

        // Categories
        // Categories > Church
        // Categories > Church > About
        public DbSet<ChurchCategories> Tbl_ChurchCategories { get; set; }
        public DbSet<ChurchSubCategories> Tbl_ChurchSubCategories { get; set; }
        public DbSet<ChurchDetails> Tbl_ChurchDetails { get; set; }
        // Categories > Church > About
        // Categories > Church > Courses
        public DbSet<CourseCategories> Tbl_CourseCategories { get; set; }
        public DbSet<CourseSubCategories> Tbl_CourseSubCategories { get; set; }
        public DbSet<CourseDetails> Tbl_CourseDetails { get; set; }
        public DbSet<CourseVideos> Tbl_CourseVideos { get; set; }
        // Categories > Church > Courses
        // Categories > Church > Events
        public DbSet<EventCategories> Tbl_EventCategories { get; set; }
        public DbSet<EventSubCategories> Tbl_EventSubCategories { get; set; }
        public DbSet<EventDetails> Tbl_EventDetails { get; set; }
        // Categories > Church > Events
        // Categories > Church > Galleries
        public DbSet<GalleryCategories> Tbl_GalleryCategories { get; set; }
        public DbSet<GallerySubCategories> Tbl_GallerySubCategories { get; set; }
        public DbSet<GalleryDetails> Tbl_GalleryDetails { get; set; }
        public DbSet<GalleryImages> Tbl_GalleryImages { get; set; }
        // Categories > Church > Galleries
        // Categories > Church > News
        public DbSet<NewCategories> Tbl_NewCategories { get; set; }
        public DbSet<NewSubCategories> Tbl_NewSubCategories { get; set; }
        public DbSet<NewDetails> Tbl_NewDetails { get; set; }
        public DbSet<NewImages> Tbl_NewImages { get; set; }
        // Categories > Church > News
        // Categories > Church > Teams
        public DbSet<TeamCategories> Tbl_TeamCategories { get; set; }
        public DbSet<TeamSubCategories> Tbl_TeamSubCategories { get; set; }
        public DbSet<TeamDetails> Tbl_TeamDetails { get; set; }
        // Categories > Church > Teams
        // Categories > Church
        // Categories
        // Settings
        // Settings > Footer
        public DbSet<Address> Tbl_Address { get; set; }
        public DbSet<SocialNetworks> Tbl_SocialNetworks { get; set; }
        // Settings > Footer
        // Settings

    }
}
