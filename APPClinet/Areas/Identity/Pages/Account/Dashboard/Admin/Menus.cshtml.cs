using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using APPClinet.Classes;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin
{
    [RoleAttribute("Admin")]
    public class AdminMenusModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
