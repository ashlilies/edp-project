using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowGreenWeb.Pages.Giving.Chat
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(string postId)
        {

            return Page();
        }
    }
}
