using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.News
{
    public class NewSubCategories
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        [MaxLength(50)]
        public string TitleSubCategory { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public bool ActivePassive { get; set; }
        [MaxLength(30)]
        public string FontName { get; set; }

        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> CountViews { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual NewCategories Tbl_NewCategories { get; set; }
        public virtual ICollection<NewDetails> Tbl_NewDetails { get; set; }
        public virtual ICollection<NewImages> Tbl_NewIamges { get; set; }
    }
}
