using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace APPClinet.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //stackoverflow.com/questions/48266241/how-can-i-get-the-current-date-in-asp-net-core-mvc
        [PersonalData]
        [MaxLength(256)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [PersonalData]
        [MaxLength(256)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [PersonalData]
        public DateTime Date { get; set; }
        [PersonalData]
        public string IDEmailConfirmed { get; set; }
        [PersonalData]
        public DateTime DateEmailConfirmed { get; set; }
        [PersonalData]
        [MaxLength(6)]
        public string ActiveCode { get; set; }
        [PersonalData]
        public DateTime DateActiveCode { get; set; }
        [PersonalData]
        public string CustomerNumber { get; set; }
        [PersonalData]
        public bool Newsletter { get; set; }
    }
}
