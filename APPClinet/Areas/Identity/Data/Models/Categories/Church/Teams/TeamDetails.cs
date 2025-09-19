using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Categories.Church.Teams
{
    public class TeamDetails
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(256)]
        public string FirstName { get; set; }
        [MaxLength(256)]
        public string LastName { get; set; }
        [MaxLength(256)]
        public string FirstNameEN { get; set; }
        [MaxLength(256)]
        public string LastNameEN { get; set; }
        [MaxLength(256)]
        public string Service { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string ImageProfile { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PrivateNumber { get; set; }
        public string Telegram { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public bool ActivePassive { get; set; }
        public string UserId { get; set; }
        public Nullable<int> Position { get; set; }
        public Nullable<int> CountViews { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser AspNetUsers { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual TeamCategories Tbl_TeamCategories { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public virtual TeamSubCategories Tbl_TeamSubCategories { get; set; }
    }
}
