using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Areas.Identity.Data
{
    public class Roles
    {
        [Key]
        [MaxLength(450)]
        public string Id { get; set; }

        public string ConcurrencyStamp { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string NormalizedName { get; set; }
    }
}
