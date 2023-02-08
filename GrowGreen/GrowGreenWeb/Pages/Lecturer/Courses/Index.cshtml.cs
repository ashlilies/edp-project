using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace GrowGreenWeb.Pages.Lecturer.Courses
{
    public class IndexModel : PageModel
    {
        public List<Course> OngoingCourses { get; set; } = new();
        public List<Course> PastCourses { get; set; } = new();
        public List<Course>? SearchResults { get; set; }
        public IList<Course> CoursesList;
        public SelectList Courses;

        [BindProperty]
        public string SearchQuery {get;set;}

        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public IndexModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGet(string? SearchQuery = null)
        {
            // todo: change to support account system :"D
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            var courses = _context.Courses
                .Include(c => c.Lectures).ThenInclude(l => l.Videos)
                .Include(c => c.CourseSignups).ThenInclude(cs => cs.Learner)
                .Include(c => c.Chats)
                .Where(c => c.LecturerId == lecturerId);


            Console.WriteLine("search query: " + SearchQuery);
            // if there is a search query, we filter the results here :"D
            if (SearchQuery is not null)
                SearchResults = await courses.Where(c => c.Name.Contains(SearchQuery)).ToListAsync();
            else
            {
                OngoingCourses = await courses.Where(c => c.EndDate >= DateTime.Today).ToListAsync();
                PastCourses = await courses.Where(c => c.EndDate < DateTime.Today).ToListAsync();
            }

            return Page();
        }
       
    }
}