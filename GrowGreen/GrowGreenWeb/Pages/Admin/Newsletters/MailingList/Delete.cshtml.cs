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
    public class DeleteModel : PageModel
    {
        private readonly GrowGreenWeb.Models.GrowGreenContext _context;

        public DeleteModel(GrowGreenWeb.Models.GrowGreenContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }
            var email = await _context.Emails.FindAsync(id);

            if (email != null)
            {
                Email = email;
                _context.Emails.Remove(Email);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
