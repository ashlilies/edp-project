using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Giving.User
{
    public class viewUserPostModel : PageModel
    {
        public List<Post> PostList { get; set; } = new();

        private readonly GrowGreenContext _context;

        private readonly IWebHostEnvironment _environment;

        public viewUserPostModel(GrowGreenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // if the future will take current user_id n search
            List<Post> allPosts = await _context.Posts.Where(r => r.UserId == 1).OrderBy(r => r.Likes).ToListAsync();

            PostList = allPosts;
            return Page();
        }
    }
}
