using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GrowGreenWeb.Models;

namespace GrowGreenWeb.Pages.Admin.Newsletters.MailingList
{
    public class DetailsModel : PageModel
    {
        private readonly GrowGreenWeb.Models.GrowGreenContext _context;

        public DetailsModel(GrowGreenWeb.Models.GrowGreenContext context)
        {
            _context = context;
        }

      public Email Email { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var email = await _context.Emails.FirstOrDefaultAsync(m => m.Id == id);
            if (email == null)
            {
                return NotFound();
            }
            else 
            {
                Email = email;
            }
            return Page();
        }
    }
}
