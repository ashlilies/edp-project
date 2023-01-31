using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowGreenWeb.Pages.Giving
{
    public class browsePostModel : PageModel
    {
        public int userId { get; set; }
        public Post? post { get; set; }

        private readonly GrowGreenContext _context;

        private readonly IWebHostEnvironment _environment;

        public browsePostModel(GrowGreenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> OnGetAsync(string postId)
        {

            // getting the post id and searching in db
            int id;
            if (Int32.TryParse(postId, out id))
            {
                Post? selectedPost = await _context.Posts.FindAsync(id);
                if (selectedPost == null)
                {
                    TempData["FlashMessage.Type"] = "error";
                    TempData["FlashMessage.Text"] = "Post ID was not found in our database";
                    return Redirect("./main");
                }
                post = selectedPost;
                userId = selectedPost.UserId;
                return Page();
            }
            
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(string postId)
        {
            int id;
            if (Int32.TryParse(postId, out id))
            {
                Post? selectedPost = await _context.Posts.FindAsync(id);
                if (selectedPost == null)
                {
                    TempData["FlashMessage.Type"] = "error";
                    TempData["FlashMessage.Text"] = "Post ID was not found in our database";
                    return RedirectToPage("main");
                }

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully deleted Post";
                _context.Posts.Remove(selectedPost);
                await _context.SaveChangesAsync();
                return RedirectToPage("main");
            }
            return await OnGetAsync(postId);
        }
    }
}
