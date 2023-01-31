using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Giving
{
    public class mainModel : PageModel
    {
        public List<Post> PostList { get; set; } = new();

        private readonly GrowGreenContext _context;

        private readonly IWebHostEnvironment _environment;

        public mainModel(GrowGreenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            List<Post> allPosts = await _context.Posts.Where(r => r.PostId != null).OrderBy(r=> r.Likes).ToListAsync();
            // list top 6 posts from the database via the likes
            if (allPosts.Count > 6)
            {
                for(int i = allPosts.Count-1; i > 6; i--)
                {
                    allPosts.RemoveAt(i);
                }
            }
            PostList = allPosts;

           return Page();
        }
    }
}
