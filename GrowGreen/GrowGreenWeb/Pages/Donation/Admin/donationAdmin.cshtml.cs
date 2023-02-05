using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stripe;

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

        public async Task<IActionResult> OnPostDelete(int id)
        {
            if (id == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Invalid Record";
                return Page();
            }

            //Course? course = await _context.Courses.FindAsync(id);
            GrowGreenWeb.Models.Donation findDonation = await _context.Donations.FindAsync(id);

            if (findDonation == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Invalid Record";
                return Page();
            }
            else
            {
                _context.Donations.Remove(findDonation);
                await _context.SaveChangesAsync();

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully Deleted Donation Record.";
            }
            return await OnGetAsync();
        }
    }
}
