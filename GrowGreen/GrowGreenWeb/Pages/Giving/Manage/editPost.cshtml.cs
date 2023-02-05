using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Stripe;
using System.Xml.Linq;
using Microsoft.Build.Framework;

namespace GrowGreenWeb.Pages.Giving.Manage
{
    public class editPostModel : PageModel
    {
        [BindProperty, MaxLength(100), DisplayName("UpdatedpostTitle")]
        public string UpdatedpostTitle { get; set; }

        [BindProperty, MaxLength(100), DisplayName("UpdatedpostLocation")]
        public string UpdatedpostLocation { get; set; }

        [BindProperty, MaxLength(200), DisplayName("UpdatedpostDescription")]
        public string UpdatedpostDescription { get; set; }

        [BindProperty, DisplayName("New Post Image (jpg/jpeg/png)")]
        public IFormFile[]? Upload { get; set; }

        [BindProperty]
        public string postImage { get; set; }

        [BindProperty]
        public string postId { get; set; }
        public Post post { get; set; }

        private readonly GrowGreenContext _context;

        private readonly IWebHostEnvironment _environment;

        public editPostModel(GrowGreenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> OnGetAsync(string postId)
        {
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
                return Page();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string postId)
        {
            int id;
            if (Int32.TryParse(postId, out id))
            {
                Post? selectedPost = _context.Posts.SingleOrDefault(c => c.PostId == id);
                if (selectedPost == null)
                {
                    TempData["FlashMessage.Type"] = "error";
                    TempData["FlashMessage.Text"] = "Post ID was not found in our database";
                    return await OnGetAsync(postId);
                }
                selectedPost.Title = UpdatedpostTitle;
                selectedPost.Description = UpdatedpostDescription;
                selectedPost.Location = UpdatedpostLocation;

                _context.Update(selectedPost);
                await _context.SaveChangesAsync();

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully updated post details.";
                return RedirectToPage("../browsePost" , new { postId = postId});
            }
            return Page();
        }
    }
}
