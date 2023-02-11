using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Filters;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Courses.Viewer
{
    [Authenticated(AccountType.Learner)]
    public class IndexModel : PageModel
    {
        public User Learner { get; set; } = null!;
        public Course Course { get; set; } = null!;

        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public IndexModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User? learner = _accountService.GetCurrentUser(HttpContext);
            if (learner is null)
                return Page();
            _context.Attach(learner);
            Learner = learner;

            Course? course = await _context.Courses
                .Include(c => c.CourseSignups)
                .ThenInclude(cs => cs.Learner)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (course is null)
                return NotFound();

            ViewData["CourseId"] = course.Id;

            // add learner record if not found (todo: update to registration page)
            if (!course.CourseSignups.Select(cs => cs.Learner).Contains(Learner))
            {
                // course.Learners.Add(Learner);
                course.CourseSignups.Add(new CourseSignup
                {
                    Course = course,
                    Learner = Learner
                });

                await _context.SaveChangesAsync();

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully signed up for this course";
            }

            Course = course;
            return Page();
        }
    }
}