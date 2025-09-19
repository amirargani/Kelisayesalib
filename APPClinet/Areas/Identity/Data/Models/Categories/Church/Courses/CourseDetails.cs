using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses
{
    public class CourseDetails
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string ImageLink { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PrivateNumber { get; set; }
        public string Telegram { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public string YoutubeLink { get; set; }
        public string EmbedLinkGoogleMap { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public bool ActivePassive { get; set; }
        public string UserId { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> CountViews { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser AspNetUsers { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual CourseCategories Tbl_CourseCategories { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public virtual CourseSubCategories Tbl_CourseSubCategories { get; set; }
        public virtual ICollection<CourseVideos> Tbl_CourseVideos { get; set; }
    }
}
