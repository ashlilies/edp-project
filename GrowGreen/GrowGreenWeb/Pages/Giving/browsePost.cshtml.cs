using GrowGreenWeb.Models;
using GrowGreenWeb.Pages.Lecturer.Courses.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace GrowGreenWeb.Pages.Giving
{
    public class browsePostModel : PageModel
    {
        public List<Request> requests { get; set; } = new();
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
                requests = _context.Requests.Where(x => x.PostId == id).ToList();
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
        public async Task<IActionResult> OnPostAccept(string postId , string senderId)
        {
            int id;
            if (Int32.TryParse(postId , out id))
            {
                Post? selectedPost = await _context.Posts.FindAsync(id);
                if (selectedPost == null)
                {
                    TempData["FlashMessage.Type"] = "error";
                    TempData["FlashMessage.Text"] = "Post ID was not found in our database";
                    return await OnGetAsync(postId);
                }
                else
                {
                    Request? checkRequest = _context.Requests.SingleOrDefault(p => p.PostId == Int32.Parse(postId) && p.SenderId == Int32.Parse(senderId));
                    if (checkRequest == null)
                    {
                        TempData["FlashMessage.Type"] = "error";
                        TempData["FlashMessage.Text"] = "Request ID not found in database";
                        return await OnGetAsync(postId);
                    }
                    else
                    {
                        checkRequest.AcceptedStatus = true;
                        _context.Requests.Update(checkRequest);
                        await _context.SaveChangesAsync();
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = "Request has been accepted!";
                        return await OnGetAsync(selectedPost.PostId.ToString());
                    }
                    

                }
            };
            return await OnGetAsync(postId);
        }
        public async Task<IActionResult> OnPostChat(string postId , string userID)
        {
            int id;
            if (Int32.TryParse(postId, out id))
            {
                Post? selectedPost = await _context.Posts.FindAsync(id);
                if (selectedPost == null)
                {
                    TempData["FlashMessage.Type"] = "error";
                    TempData["FlashMessage.Text"] = "Post ID was not found in our database";
                    return Page();
                }
                else
                {
                    if (userID.ToString() == selectedPost.UserId.ToString())
                    {
                        TempData["FlashMessage.Type"] = "error";
                        TempData["FlashMessage.Text"] = "Cannot request chat to your own post!";
                        return await OnGetAsync(selectedPost.PostId.ToString());
                    }
                    else
                    {
                        Request? checkRequest = _context.Requests.SingleOrDefault(p => p.PostId == selectedPost.PostId && p.SenderId == Int32.Parse(userID));
                        if (checkRequest == null)
                        {
                            // creating new request object
                            Request newRequest = new Request()
                            {
                                SenderId = Int32.Parse(userID),
                                DateSent = DateTime.Now,
                                AcceptedStatus = false,
                                PostId = selectedPost.PostId,
                                Post = selectedPost,
                                Sender = null,
                            };
                            _context.Requests.Add(newRequest);
                            await _context.SaveChangesAsync();
                            TempData["FlashMessage.Type"] = "success";
                            TempData["FlashMessage.Text"] = "Successfully requested";
                            return await OnGetAsync(selectedPost.PostId.ToString());
                        }
                        else
                        {
                            TempData["FlashMessage.Type"] = "error";
                            TempData["FlashMessage.Text"] = "You have already requested to this post";
                            return await OnGetAsync(selectedPost.PostId.ToString());
                        }
                    }
                }

            }
            return await OnGetAsync(postId);
        }
    }
}
