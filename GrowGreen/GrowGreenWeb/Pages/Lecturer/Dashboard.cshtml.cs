using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GrowGreenWeb.Pages.Lecturer
{
    public class DashboardModel : PageModel
    {
        public string listOfCoursesJson { get; set; }
        public string listOfEventsJson { get; set; }
        private readonly GrowGreenContext _context;
        public DashboardModel(GrowGreenContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            List<Course> courses = await _context.Courses.ToListAsync();

            var listOfCourses = courses.DistinctBy(course => course.Id).ToList();

            listOfCoursesJson = JsonSerializer.Serialize(listOfCourses);

            List<Event> events = await _context.Events.ToListAsync();

            var listOfEvents = events.DistinctBy(events => events.Id).ToList();

            listOfEventsJson = JsonSerializer.Serialize(listOfEvents);

            return Page();
        }
    }
}
