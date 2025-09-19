using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data.Models.Settings.Footer
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser AspNetUsers { get; set; }
    }
}
