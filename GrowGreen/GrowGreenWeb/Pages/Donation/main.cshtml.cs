using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using Stripe;

namespace GrowGreenWeb.Pages.Donation
{
    public class mainModel : PageModel
    {
        public void OnGet()
        {
            
        }
        private readonly IConfiguration _configuration;
        public void StripeKey(IConfiguration configuration)
        {
            string key = configuration.GetValue<string>("StripeSecretKey");
            StripeConfiguration.SetApiKey(key);
        }

    }
}
