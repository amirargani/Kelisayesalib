using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.About
{
    public class ChurchPages
    {
        public List<ChurchCategories> churchCategories { get; set; }
        public List<ChurchSubCategories> churchSubCategories { get; set; }
        public List<ChurchDetails> churchDetails { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
