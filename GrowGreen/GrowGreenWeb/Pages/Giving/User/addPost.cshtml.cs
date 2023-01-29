using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GrowGreenWeb.Pages.Giving.User
{
    public class addPostModel : PageModel
    {
        [BindProperty, Required, MaxLength(100), DisplayName("postTitle")]
        public string postTitle { get; set; }

        [BindProperty, Required, MaxLength(100), DisplayName("postLocation")]
        public string postLocation { get; set; }

        [BindProperty, Required, MaxLength(200), DisplayName("postDescription")]
        public string postDescription { get; set; }

        [BindProperty, DisplayName("New Post Image (jpg/jpeg/png)")]
        public IFormFile[]? Upload { get; set; }

        [BindProperty, Required]
        public string postImage { get; set; }

        private readonly GrowGreenContext _context;

        private readonly IWebHostEnvironment _environment;

        public addPostModel(GrowGreenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostUploadAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            ModelState.Clear();

            if (!ModelState.IsValid)
            {
                return Page();

            }
            if (postTitle == null || postDescription == null || postLocation == null)
            {
                TempData["FlashMessage.Type"] = "error";
                TempData["FlashMessage.Text"] = "Please enter all the required fields to make a post";
                return Page();
            }
            else
            {
                if (Upload == null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = "Error uploading image";
                    return Page();
                }

                if (!Constants.AllowedImageExtensions.Contains(Path.GetExtension(Upload[0].FileName)))
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = "Image file type not allowed!";
                    return Page();
                }
                else
                {
                    if (Upload.Length > 0)
                    {
                        List<string> FileNames = new List<string>();
                        foreach (IFormFile photo in Upload)
                        {
                            string guuidStr = Guid.NewGuid().ToString();
                            var file = Path.Combine(_environment.WebRootPath, "uploads", "GivingC", guuidStr + "-" + photo.FileName);
                            await using (var fileStream = new FileStream(file, FileMode.Create))
                            {
                                await photo.CopyToAsync(fileStream);
                                FileNames.Add("/uploads/GivingC/" + guuidStr + "-" + photo.FileName);
                            }
                        }
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = "Image Successfully uploaded";
                        postImage = string.Join(",", FileNames);
                    }
                }

                Post newPost = new Post()
                {
                    Timestamp = DateTime.Now,
                    Description = postDescription,
                    Title = postTitle,
                    Location = postLocation,
                    Likes = 0,
                    Image = postImage,
                    UserId = 1,
                    User = null,
                    GivingReviews = null,
            };

            _context.Add(newPost);
            await _context.SaveChangesAsync();
            }
            return Page();
        }
    }
}
