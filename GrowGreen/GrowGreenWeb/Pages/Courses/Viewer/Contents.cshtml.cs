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
    public class ContentsModel : PageModel
    {
        public User Learner { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Lecture Lecture { get; set; } = null!;

        private readonly GrowGreenContext _context;

        public ContentsModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int courseId, int lectureId)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? learner = await _context.Users.FindAsync(learnerId);

            if (learner is null)
                return Forbid();

            Learner = learner;

            Course? course = await _context.Courses
                .Include(c => c.CourseSignups).ThenInclude(cs => cs.Learner)
                .SingleOrDefaultAsync(c => c.Id == courseId);

            if (course is null)
                return NotFound();

            Course = course;
            ViewData["CourseId"] = course.Id;

            // load lecture videos
            Lecture? lecture = await _context.Lectures
                .Include(l => l.Videos)
                .SingleOrDefaultAsync(l => l.Id == lectureId);

            if (lecture is null)
                return NotFound();
            
            Lecture = lecture;

            return Page();
        }
    }
}