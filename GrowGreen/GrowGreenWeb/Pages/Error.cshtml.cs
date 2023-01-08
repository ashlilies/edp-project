using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowGreenWeb.Pages;

public class Error : PageModel
{
    public int ErrorCode { get; set; }
    public void OnGet(int id)
    {
        ErrorCode = id;
    }
}