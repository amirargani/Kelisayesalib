using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Teams
{
    public class TeamPages
    {
        public List<TeamCategories> teamCategories { get; set; }
        public List<TeamSubCategories> teamSubCategories { get; set; }
        public List<TeamDetails> teamDetails { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
