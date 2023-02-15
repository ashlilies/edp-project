using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        public Course Course { get; set; } = null!;
        private readonly GrowGreenContext _context;
        private readonly AccountService _accountService;

        public DetailsModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
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

            // redirect to viewer if already signed up
            User? user = _accountService.GetCurrentUser(HttpContext, AccountType.Learner);
            if (user is not null)
            {
                _context.Attach(user);
                if (course.CourseSignups.Select(cs => cs.Learner).Contains(user))
                {
                    return RedirectToPage("Viewer/Index", new { id = course.Id });
                }
            }

            return Page();
        }
    }
}