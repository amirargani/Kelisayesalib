using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.News
{
    public class NewPages
    {
        public List<NewCategories> newCategories { get; set; }
        public List<NewSubCategories> newSubCategories { get; set; }
        public List<NewDetails> newDetails { get; set; }
        public List<NewImages> newImages { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
