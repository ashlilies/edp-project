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
using NuGet.Protocol;

namespace GrowGreenWeb.Pages.Courses
{
    public class IndexModel : PageModel
    {
        public List<Course> SignedUpCourses { get; set; } = new();
        public List<Course> OngoingCourses { get; set; } = new();
        public List<Course> PastCourses { get; set; } = new();
        
        public User? Learner { get; set; }
        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public IndexModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGet()
        {
            User? learner = _accountService.GetCurrentUser(HttpContext);
            if (learner is not null)
            {
                _context.Attach(learner);
                Learner = learner;
            }

            List<Course> courses = await _context.Courses
                .Include(c => c.Lectures).ThenInclude(l => l.Videos).ThenInclude(v => v.VideoCompletions)
                .Include(c => c.CourseSignups).ThenInclude(cs => cs.Learner)
                .Include(c => c.Lecturer)
                .Include(c => c.Chats)
                .ToListAsync();

            if (learner is not null)
                SignedUpCourses = courses.Where(c => c.CourseSignups.Select(cs => cs.Learner).Contains(learner)).ToList();
            OngoingCourses = courses.Where(c => c.EndDate >= DateTime.Today && !SignedUpCourses.Contains(c)).ToList();
            PastCourses = courses.Where(c => c.EndDate < DateTime.Today && !SignedUpCourses.Contains(c)).ToList();

            return Page();
        }
    }
}