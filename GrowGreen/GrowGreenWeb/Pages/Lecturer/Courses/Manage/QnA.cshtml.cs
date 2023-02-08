using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Lecturer.Courses.Manage
{
    public class QnAModel : PageModel
    {
        [BindProperty]
        public string NewMessageText { get; set; } = null!;
        
        [BindProperty]
        public string? EditMessageText { get; set; }

        public List<Chat> Chats { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public User CurrentUser { get; set; } = null!;

        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public QnAModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            User? user = await _context.Users.FindAsync(lecturerId);
            if (user == null)
                return Forbid();

            CurrentUser = user;

            Course? course = await _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;
            ViewData["CourseId"] = course.Id;

            List<Chat> chats = _context.Chats
                .Include(c => c.User)
                .Where(c => c.CourseId == Course.Id)
                .OrderBy(c => c.Timestamp)
                .ToList();
            
            Chats = chats.Select(c => c.Clone()).ToList();
            
            chats.Where(c => c.UserId != lecturerId).ToList().ForEach(c =>
            {
                c.IsReadByLecturer = true;
                _context.Update(c);
            });
            await _context.SaveChangesAsync();
            
            return Page();
        }

        public async Task<IActionResult> OnPostSendAsync(int id)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            User? user = await _context.Users.FindAsync(lecturerId);
            if (user == null)
                return Forbid();

            CurrentUser = user;

            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

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
                UserId = CurrentUser.Id,
                Content = NewMessageText,
                Timestamp = DateTime.Now,
                IsReadByLecturer = true
            };

            _context.Add(chat);
            await _context.SaveChangesAsync();

            NewMessageText = string.Empty;

            return await OnGetAsync(id);
        }

        public async Task<IActionResult> OnPostEditAsync(int id, int chatId)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            User? user = await _context.Users.FindAsync(lecturerId);
            if (user == null)
                return Forbid();

            CurrentUser = user;

            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

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
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            User? user = await _context.Users.FindAsync(lecturerId);
            if (user == null)
                return Forbid();

            CurrentUser = user;

            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

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