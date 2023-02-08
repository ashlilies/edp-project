using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GrowGreenWeb.Models;

namespace GrowGreenWeb.Pages.Admin.Newsletters.History
{
    public class DeleteModel : PageModel
    {
        private readonly GrowGreenWeb.Models.GrowGreenContext _context;

        public DeleteModel(GrowGreenWeb.Models.GrowGreenContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Newsletter Newsletter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Newsletters == null)
            {
                return NotFound();
            }

            var newsletter = await _context.Newsletters.FirstOrDefaultAsync(m => m.Id == id);

            if (newsletter == null)
            {
                return NotFound();
            }
            else 
            {
                Newsletter = newsletter;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Newsletters == null)
            {
                return NotFound();
            }
            var newsletter = await _context.Newsletters.FindAsync(id);

            if (newsletter != null)
            {
                Newsletter = newsletter;
                _context.Newsletters.Remove(Newsletter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
