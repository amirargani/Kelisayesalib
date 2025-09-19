using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using APPClinet.Areas.Identity.Data;
using APPClinet.Areas.Identity.Data.Interfaces;
using APPClinet.Classes;
using APPClinet.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPClinet.Areas.Identity.Pages.Account.Dashboard.Admin
{
    [RoleAttribute("Admin")]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUser _iuser;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApplicationUser iuser)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _iuser = iuser;
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            //[Required(ErrorMessage = faMessage.RequiredMsgFirstName)]
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgFirstNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.FirstName)]
            public string FirstName { get; set; }
            //[Required(ErrorMessage = faMessage.RequiredMsgLastName)]
            [StringLength(30, ErrorMessage = faMessage.RequiredMsgLastNameStringLength, MinimumLength = 3)]
            [Display(Name = faMessage.LastName)]
            public string LastName { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            Username = userName;
            FirstName = _iuser.ExistsUser(Username).FirstName;
            LastName = _iuser.ExistsUser(Username).LastName;

            Input = new InputModel
            {
                FirstName = FirstName,
                LastName = LastName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            TempData.Clear();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            else
            {
                _iuser.UpdateUser(user, Input.FirstName, Input.LastName);
            }

            await _signInManager.RefreshSignInAsync(user);
            //StatusMessage = "Your profile has been updated";
            StatusMessage = faMessage.UpdateTitle;
            //return RedirectToPage();
            return Redirect("./Index");
        }
    }
}
