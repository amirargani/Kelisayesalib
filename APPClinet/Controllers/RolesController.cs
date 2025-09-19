using APPClinet.Classes; // Add
using APPClinet.Messages; // Add
using APPClinet.Models; // Add
using Microsoft.AspNetCore.Identity; // Add
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Add
using System;
using System.Collections.Generic;
using System.Diagnostics; // Add
using System.Linq;
using System.Threading.Tasks;

namespace APPClinet.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            await Initializer.Initial(_roleManager, "User");
            await Initializer.Initial(_roleManager, "Admin");
            await Initializer.Initial(_roleManager, "Manager");
            await Initializer.Initial(_roleManager, "AdminWebsite");
            await Initializer.Initial(_roleManager, "AuthorText");
            await Initializer.Initial(_roleManager, "Pastor");
            await Initializer.Initial(_roleManager, "Teacher");
            await Initializer.Initial(_roleManager, "AuthorKids");
            await Initializer.Initial(_roleManager, "AuthorNews");
            await Initializer.Initial(_roleManager, "AuthorText");
            await Initializer.Initial(_roleManager, "AuthorMusic");
            await Initializer.Initial(_roleManager, "AuthorVideo");
            await Initializer.Initial(_roleManager, "ios");
            await Initializer.Initial(_roleManager, "ApiWeb");
            await Initializer.Initial(_roleManager, "Linux");
            await Initializer.Initial(_roleManager, "Desktop");
            await Initializer.Initial(_roleManager, "Android");
            return RedirectToAction(nameof(faController.StartNew), "fa");
        }
    }
}
