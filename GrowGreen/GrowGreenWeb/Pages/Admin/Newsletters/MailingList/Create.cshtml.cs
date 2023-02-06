using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GrowGreenWeb.Models;

namespace GrowGreenWeb.Pages.Admin.Newsletters.MailingList
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
        public Email Email { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Emails == null || Email == null)
            {
                return Page();
            }

            _context.Emails.Add(Email);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
