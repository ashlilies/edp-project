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
    public class DetailsModel : PageModel
    {
        public Course Course { get; set; } = null!;
        private readonly GrowGreenContext _context;

        public DetailsModel(GrowGreenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Course? course = _context.Courses
                .Include(c => c.CourseReviews)
                .Include(c => c.Lecturer)
                .Include(c => c.CourseSignups).ThenInclude(c => c.Learner)
                .SingleOrDefault(c => c.Id == id);

            if (course == null)
                return NotFound();

            Course = course;
            return Page();
        }
    }
}