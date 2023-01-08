using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Lecturer.Courses
{
    public class IndexModel : PageModel
    {
        public List<Course> OngoingCourses { get; set; } = new();
        public List<Course> PastCourses { get; set; } = new();
        private readonly GrowGreenContext _context;

        public IndexModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            // todo: change to support account system :"D
            int lecturerId = TemporaryConstants.LecturerId;

            List<Course> courses = await _context.Courses
                .Include(c => c.Lectures).ThenInclude(l => l.Videos)
                .Include(c => c.Learners)
                .Include(c => c.Chats)
                .Where(c => c.LecturerId == lecturerId)
                .ToListAsync();

            OngoingCourses = courses.Where(c => c.EndDate >= DateTime.Today).ToList();
            PastCourses = courses.Where(c => c.EndDate < DateTime.Today).ToList();

            return Page();
        }
    }
}