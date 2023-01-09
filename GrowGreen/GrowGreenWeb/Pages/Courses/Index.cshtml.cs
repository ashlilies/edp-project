using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Courses
{
    public class IndexModel : PageModel
    {
        public List<Course> SignedUpCourses { get; set; } = new();
        public List<Course> OngoingCourses { get; set; } = new();
        public List<Course> PastCourses { get; set; } = new();
        private readonly GrowGreenContext _context;

        public IndexModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? learner = await _context.Users.FindAsync(learnerId);

            List<Course> courses = await _context.Courses
                .Include(c => c.Lectures).ThenInclude(l => l.Videos)
                .Include(c => c.Learners)
                .Include(c => c.Lecturer)
                .Include(c => c.Chats)
                .ToListAsync();

            if (learner is not null)
                SignedUpCourses = courses.Where(c => c.Learners.Contains(learner)).ToList();
            OngoingCourses = courses.Where(c => c.EndDate >= DateTime.Today && !SignedUpCourses.Contains(c)).ToList();
            PastCourses = courses.Where(c => c.EndDate < DateTime.Today && !SignedUpCourses.Contains(c)).ToList();

            return Page();
        }
    }
}