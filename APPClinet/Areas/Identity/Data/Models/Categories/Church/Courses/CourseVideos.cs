using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses
{
    public class CourseVideos
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public string Video { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public Nullable<int> DetailId { get; set; }
        public bool ActivePassive { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> CountViews { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser AspNetUsers { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual CourseCategories Tbl_CourseCategories { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public virtual CourseSubCategories Tbl_CourseSubCategories { get; set; }

        [ForeignKey(nameof(DetailId))]
        public virtual CourseDetails Tbl_CourseDetails { get; set; }
    }
}
