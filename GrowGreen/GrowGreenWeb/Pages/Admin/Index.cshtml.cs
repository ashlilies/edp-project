using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Filters;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowGreenWeb.Pages.Admin
{
    [Authenticated(AccountType.Admin)]
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("Dashboard");
        }
    }
}
