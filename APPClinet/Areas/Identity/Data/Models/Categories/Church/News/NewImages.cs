using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.News
{
    public class NewImages
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
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
        public virtual NewCategories Tbl_NewCategories { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public virtual NewSubCategories Tbl_NewSubCategories { get; set; }

        [ForeignKey(nameof(DetailId))]
        public virtual NewDetails Tbl_NewDetails { get; set; }
    }
}
