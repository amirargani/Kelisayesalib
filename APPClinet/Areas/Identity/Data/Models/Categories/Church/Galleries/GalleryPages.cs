using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries
{
    public class GalleryPages
    {
        public List<GalleryCategories> galleryCategories { get; set; }
        public List<GallerySubCategories> gallerySubCategories { get; set; }
        public List<GalleryDetails> galleryDetails { get; set; }
        public List<GalleryImages> galleryImages { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
