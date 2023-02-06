using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Courses.Viewer
{
    public class QnAModel : PageModel
    {
        [BindProperty]
        public string NewMessageText { get; set; } = string.Empty;
        
        [BindProperty]
        public string? EditMessageText { get; set; }

        public List<Chat> Chats { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public User Learner { get; set; } = null!;

        private readonly GrowGreenContext _context;

        public QnAModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? user = await _context.Users.FindAsync(learnerId);
            if (user == null)
                return Forbid();

            Learner = user;

            Course? course = await _context.Courses.Include(c => c.Learners).SingleOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();
            
            ViewData["CourseId"] = course.Id;

            // add learner record if not found (todo: update to registration page)
            if (!course.Learners.Contains(Learner))
            {
                return Forbid();
            }

            Course = course;

            Chats = _context.Chats
                .Include(c => c.User)
                .Where(c => c.CourseId == Course.Id)
                .OrderBy(c => c.Timestamp)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostSendAsync(int id)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? user = await _context.Users.FindAsync(learnerId);
            if (user == null)
                return Forbid();

            Learner = user;

            Course? course = await _context.Courses.Include(c => c.Learners).SingleOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();

            // add learner record if not found (todo: update to registration page)
            if (!course.Learners.Contains(Learner))
            {
                return Forbid();
            }

            Course = course;

            if (string.IsNullOrWhiteSpace(NewMessageText))
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Message cannot be empty.";
                return await OnGetAsync(id);
            }

            // create new chat message
            Chat chat = new Chat
            {
                CourseId = Course.Id,
                UserId = Learner.Id,
                Content = NewMessageText,
                Timestamp = DateTime.Now,
                IsReadByLecturer = false
            };

            _context.Add(chat);
            await _context.SaveChangesAsync();

            NewMessageText = string.Empty;

            return await OnGetAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync(int id, int chatId)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? user = await _context.Users.FindAsync(learnerId);
            if (user == null)
                return Forbid();

            Learner = user;

            Course? course = await _context.Courses.Include(c => c.Learners).SingleOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();

            // add learner record if not found (todo: update to registration page)
            if (!course.Learners.Contains(Learner))
            {
                return Forbid();
            }

            Course = course;

            if (string.IsNullOrWhiteSpace(EditMessageText))
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Message cannot be empty.";
                return await OnGetAsync(id);
            }

            // update chat message
            Chat? chat = await _context.Chats.FindAsync(chatId);
            if (chat == null)
                return NotFound();

            chat.Content = EditMessageText;
            chat.EditedTimestamp = DateTime.Now;

            return await OnGetAsync(id);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, int chatId)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? user = await _context.Users.FindAsync(learnerId);
            if (user == null)
                return Forbid();

            Learner = user;

            Course? course = await _context.Courses.Include(c => c.Learners).SingleOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();

            // add learner record if not found (todo: update to registration page)
            if (!course.Learners.Contains(Learner))
            {
                return Forbid();
            }

            Course = course;

            Chat? chat = await _context.Chats.FindAsync(chatId);
            if (chat is null)
                return NotFound();

            _context.Remove(chat);
            await _context.SaveChangesAsync();

            return await OnGetAsync(id);
        }
    }
}