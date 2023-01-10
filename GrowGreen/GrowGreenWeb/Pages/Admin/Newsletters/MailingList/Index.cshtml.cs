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
    public class IndexModel : PageModel
    {
        private readonly GrowGreenWeb.Models.GrowGreenContext _context;

        public IndexModel(GrowGreenWeb.Models.GrowGreenContext context)
        {
            _context = context;
        }

        public IList<Email> Email { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Emails != null)
            {
                Email = await _context.Emails.ToListAsync();
            }
        }
    }
}
