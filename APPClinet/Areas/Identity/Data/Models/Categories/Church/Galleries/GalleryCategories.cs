using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Galleries
{
    public class GalleryCategories
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
        public virtual ICollection<GallerySubCategories> Tbl_GallerySubCategories { get; set; }
        public virtual ICollection<GalleryDetails> Tbl_GalleryDetails { get; set; }
        public virtual ICollection<GalleryImages> Tbl_GalleryIamges { get; set; }
    }
}
