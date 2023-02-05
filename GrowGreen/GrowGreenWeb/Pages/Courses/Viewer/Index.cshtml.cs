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
    public class IndexModel : PageModel
    {
        public User Learner { get; set; } = null!;
        public Course Course { get; set; } = null!;

        private readonly GrowGreenContext _context;

        public IndexModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? learner = await _context.Users.FindAsync(learnerId);

            if (learner is null)
                return Forbid();

            Learner = learner;

            Course? course = await _context.Courses.Include(c => c.Learners).SingleOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();

            ViewData["CourseId"] = course.Id;
            
            // add learner record if not found (todo: update to registration page)
            if (!course.Learners.Contains(Learner))
            {
                course.Learners.Add(Learner);
                await _context.SaveChangesAsync();

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully signed up for this course";
            }

            Course = course;
            return Page();
        }
    }
}