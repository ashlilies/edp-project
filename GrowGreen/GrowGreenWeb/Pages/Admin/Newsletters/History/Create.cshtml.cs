using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GrowGreenWeb.Models;

namespace GrowGreenWeb.Pages.Admin.Newsletters.History
{
    public class CreateModel : PageModel
    {
        private readonly GrowGreenWeb.Models.GrowGreenContext _context;

        public CreateModel(GrowGreenWeb.Models.GrowGreenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Newsletter Newsletter { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Newsletters == null || Newsletter == null)
            {
                return Page();
            }

            _context.Newsletters.Add(Newsletter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
