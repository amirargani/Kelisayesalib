using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Events
{
    public class EventPages
    {
        public List<EventCategories> eventCategories { get; set; }
        public List<EventSubCategories> eventSubCategories { get; set; }
        public List<EventDetails> eventDetails { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
