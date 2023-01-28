using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Stripe;
using Stripe.Checkout;
using System.Xml.Linq;

namespace GrowGreenWeb.Pages.Donation
{
    public class successModel : PageModel
    {
        private readonly GrowGreenContext _context;

        private readonly IWebHostEnvironment _environment;

        public successModel(GrowGreenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> OnGetAsync(string sessionId , string donationText)
        {

            // recording donation record into database
            var service = new SessionService();
            var session = service.Get(Convert.ToString(sessionId));
            System.Diagnostics.Debug.WriteLine(session);
            JToken contourManifest = JObject.Parse(session.ToJson().ToString());

            JToken features = contourManifest.SelectToken("id");


            JToken customerDetails = JObject.Parse(contourManifest.SelectToken("customer_details").ToString());
            GrowGreenWeb.Models.Donation newDonation = new GrowGreenWeb.Models.Donation()
            {
                DateTime = DateTime.Now,
                Remarks = donationText,
                Amount = (decimal)contourManifest.SelectToken("amount_total"),
                ReceipientId = 1,
                Email = (string)customerDetails.SelectToken("email"),
                Purpose = "Donation",
                SenderId = null,
                TransactionId = null,
                Sender = null,
                Transaction = null,
            };

        _context.Add(newDonation);
        await _context.SaveChangesAsync();
        TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Successfully donation" + features;
            return Page();
        }
    }
}
