using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Lecturer.Courses.Manage
{
    public class ContentsModel : PageModel
    {
        public Course Course { get; set; } = null!;
        public Lecture Lecture { get; set; } = null!;

        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public ContentsModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public IActionResult OnGet(int id, int lectureId)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;
            ViewData["CourseId"] = course.Id;

            Lecture? lecture = _context.Lectures
                .Include(l => l.Videos)
                .SingleOrDefault(l => l.Id == lectureId);

            if (lecture is null)
                return NotFound();
            if (lecture.CourseId != course.Id)
                return Forbid();

            Lecture = lecture;
            
            return Page();
        }

        public IActionResult OnPostDelete(int id, int lectureId)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;
            Lecture? lecture = _context.Lectures
                .Include(l => l.Videos)
                .SingleOrDefault(l => l.Id == lectureId);

            if (lecture is null)
                return NotFound();
            if (lecture.CourseId != course.Id)
                return Forbid();

            // delete the video
            _context.Remove(lecture);
            _context.SaveChanges();

            return RedirectToPage("Index", new { id = Course.Id });
        }
    }
}