using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Donation.Admin
{
    public class donationAdminModel : PageModel
    {
        public List<GrowGreenWeb.Models.Donation> acceptedDonations { get; set; } = new();
        private readonly GrowGreenContext _context;

        public donationAdminModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            List <GrowGreenWeb.Models.Donation> donations = await _context.Donations.ToListAsync();

            acceptedDonations = donations;
            return Page();
        }
    }
}
