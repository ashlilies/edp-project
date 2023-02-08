using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GrowGreenWeb.Pages.Account
{
    //ashlee: irfan you can edit this so to add sign up/remember me/other features, only login has been done
    [BindProperties]
    public class LoginModel : PageModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = false;

        private readonly AccountService _accountService;

        public LoginModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet(string? prevUrl = null)
        {
        }

        // ashlee: testing purposes only
        public void OnGetFirstTime()
        {
            _accountService.ChangePassword("viona@vyiiona.com", "1234");
            _accountService.ChangePassword("ash@ashlee.one", "1234");
            _accountService.ChangePassword("trina@example.com", "1234");
            _accountService.ChangePassword("msli@example.com", "1234");
            _accountService.ChangePassword("bobby@example.com", "1234");
        }

        public IActionResult OnGetLogout(string? prevUrl = null)
        {
            _accountService.LogoutSession(HttpContext);

            if (prevUrl != null)
            {
                return Redirect(prevUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        public IActionResult OnPost(string? prevUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_accountService.LoginSession(HttpContext, Email, Password, RememberMe))
            {
                if (prevUrl != null)
                {
                    return Redirect(prevUrl);
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }

            return Page();
        }
    }
}