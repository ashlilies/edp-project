using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GrowGreenWeb.Models;

namespace GrowGreenWeb.Pages.Admin.Newsletters.History
{
    public class EditModel : PageModel
    {
        private readonly GrowGreenWeb.Models.GrowGreenContext _context;

        public EditModel(GrowGreenWeb.Models.GrowGreenContext context)
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

            var newsletter =  await _context.Newsletters.FirstOrDefaultAsync(m => m.Id == id);
            if (newsletter == null)
            {
                return NotFound();
            }
            Newsletter = newsletter;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Newsletter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsletterExists(Newsletter.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NewsletterExists(int id)
        {
          return (_context.Newsletters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
