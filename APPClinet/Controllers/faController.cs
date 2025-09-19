using APPClinet.Areas.Identity.Data; // Add
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
    public class faController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<faController> _logger;

        public faController(SignInManager<ApplicationUser> signInManager, ILogger<faController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        //public IActionResult Index()
        //{
        //    ViewBag.Title = TitleApp.app_en + TitleApp.app_fa;
        //    return View();
        //}
        public IActionResult StartNew()
        {
            ViewBag.Title = TitleApp.app_en + TitleApp.app_fa;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Terms()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SendEmailMessage(string name, string email, string message)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            //return LocalRedirect("/");
            return RedirectToAction(nameof(faController.StartNew), "fa");
        }
        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            //return LocalRedirect(returnUrl);
            return RedirectToAction(nameof(faController.StartNew), "fa");
        }
    }
}
