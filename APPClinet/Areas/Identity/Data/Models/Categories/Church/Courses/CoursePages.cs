using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Courses
{
    public class CoursePages
    {
        public List<CourseCategories> courseCategories { get; set; }
        public List<CourseSubCategories> courseSubCategories { get; set; }
        public List<CourseDetails> courseDetails { get; set; }
        public List<CourseVideos> courseVideos { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
