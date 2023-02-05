using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace GrowGreenWeb.Pages.Giving.Chat
{   //chat report (each message)
    //public int Id { get; set; }
    //public string Content { get; set; } = null!;
    //public DateTime Timestamp { get; set; }
    //public bool IsRead { get; set; }
    //public int ChatId { get; set; }

    //public virtual Chat Chat { get; set; } = null!;

    // chat
    //public int Id { get; set; }
    //public DateTime Timestamp { get; set; }
    //public string Content { get; set; } = null!;
    //public bool IsReadByLecturer { get; set; }
    //public bool isRead { get; set; }
    //public int UserId { get; set; }
    //public DateTime? EditedTimestamp { get; set; }
    //public string? PostId { get; set; }
    //public int? CourseId { get; set; }
    //public int? ReplyToChatId { get; set; }
    //public string? AttachmentUrl { get; set; }

    //public virtual Course? Course { get; set; }
    //public virtual Post? Post { get; set; }
    //public virtual Chat? ReplyToChat { get; set; }
    //public virtual User User { get; set; } = null!;
    //public virtual ICollection<ChatReport> ChatReports { get; set; }
    //public virtual ICollection<Chat> InverseReplyToChat { get; set; }
    public class IndexModel : PageModel
    {
        [BindProperty , Required]
        public string? Id { get; set; }

        public int userId { get; set; }

        private readonly GrowGreenContext _context;

        private readonly IWebHostEnvironment _environment;

        public IndexModel(GrowGreenContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> OnGetAsync(string postId)
        {
            userId = 1; //  hardcoded for now
            Id = postId;
            return Page();
        }

        public async Task<IActionResult> OnPostSend(string message , string postId , string userId)
        {
            int id;
            if (Int32.TryParse(postId, out id))
            {
                GrowGreenWeb.Models.Chat? searchChat = await _context.Chats.FindAsync(id);
                if (searchChat == null)
                {
                    TempData["FlashMessage.Type"] = "error";
                    TempData["FlashMessage.Text"] = "Post ID was not found in our database";
                    return Redirect("./main");
                }
                return Page();
            }
            return await OnGetAsync(postId);
        }
    }
}
