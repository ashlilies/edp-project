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
    public class Index2Model : PageModel
    {
        public Course? WhatsNewCourse { get; set; }
        public List<Course> WhatsHotCourses { get; set; } = new();
        public List<Course> SignedUpCourses { get; set; } = new();
        public List<Course> PopularInSingaporeCourses { get; set; } = new();
        public List<Course> PastCourses { get; set; } = new();

        [BindProperty]
        public string? SearchQuery { get; set; } = null;
        
        public User? Learner { get; set; }
        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public Index2Model(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGet(string? SearchQuery = null)
        {
            User? learner = _accountService.GetCurrentUser(HttpContext);
            if (learner is not null)
            {
                _context.Attach(learner);
                Learner = learner;
            }

            this.SearchQuery = SearchQuery;

            var courses = await _context.Courses
                .Include(c => c.Lectures).ThenInclude(l => l.Videos).ThenInclude(v => v.VideoCompletions)
                .Include(c => c.CourseSignups).ThenInclude(cs => cs.Learner)
                .Include(c => c.Lecturer)
                .Include(c => c.Chats)
                .Where(c => (SearchQuery == null) || c.Name.Contains(SearchQuery)) // apply search query if any
                .ToListAsync();

            if (learner is not null)
                SignedUpCourses = courses.Where(c => c.CourseSignups.Select(cs => cs.Learner).Contains(learner)).ToList();
            
            PopularInSingaporeCourses = courses
                .Where(c => c.EndDate >= DateTime.Today /* && !SignedUpCourses.Contains(c) */)
                .OrderByDescending(c => c.CourseSignups.Count)
                .ToList();
            PastCourses = courses
                .Where(c => c.EndDate < DateTime.Today /* && !SignedUpCourses.Contains(c) */)
                .OrderByDescending(c => c.EndDate)
                .ToList();
            WhatsNewCourse = courses.MaxBy(c => c.LastUpdatedTimestamp);
            WhatsHotCourses = courses
                .SelectMany(c => c.CourseSignups)
                .Where(cs => cs.Timestamp.Date > DateTime.Today.AddDays(-2))
                .GroupBy(cs => cs.Course)
                .Select(g => new
                {
                    Course = g.Key,
                    Signups = g.Count()
                })
                .OrderByDescending(g => g.Signups)
                .Select(g => g.Course)
                .Distinct()
                .Take(2)
                .ToList();

            return Page();
        }
    }
}