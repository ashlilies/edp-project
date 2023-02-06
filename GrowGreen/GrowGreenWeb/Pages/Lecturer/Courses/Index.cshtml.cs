using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace GrowGreenWeb.Pages.Lecturer.Courses
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Course> OngoingCourses { get; set; } = new();
        public List<Course> PastCourses { get; set; } = new();
        public IList<Course> CoursesList;
        public SelectList Courses;
        public string SearchQuery { get; set; }
        
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
            SearchQuery = courses.Where(c => c.Name.Contains());

            return Page();
        }
        
        public async Task OnGetAsync(string searchQuery)
        {
            IQueryable<string> genreQuery = from c in _context.Courses
                orderby c.Name
                select c.Name;

            var courses = from c in _context.Courses
                select c;

            if (!String.IsNullOrEmpty(searchQuery))
            {
                courses = courses.Where(c => c.Name.Contains(searchQuery));
            }
            
            courses = await c.ToListAsync();
        }
    }
}