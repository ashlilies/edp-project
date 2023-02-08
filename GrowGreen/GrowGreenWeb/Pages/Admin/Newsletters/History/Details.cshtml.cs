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
    public class DetailsModel : PageModel
    {
        private readonly GrowGreenWeb.Models.GrowGreenContext _context;

        public DetailsModel(GrowGreenWeb.Models.GrowGreenContext context)
        {
            _context = context;
        }

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
    }
}
