using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses
{
    public class CourseCategories
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        [MaxLength(50)]
        public string TitleCategory { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public bool ActivePassive { get; set; }
        [MaxLength(30)]
        public string FontName { get; set; }
        public Nullable<int> Position { get; set; }
        public virtual ICollection<CourseSubCategories> Tbl_CourseSubCategories { get; set; }
        public virtual ICollection<CourseDetails> Tbl_CourseDetails { get; set; }
        public virtual ICollection<CourseVideos> Tbl_CourseVideos { get; set; }
    }
}
