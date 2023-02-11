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
        public string listOfCourseSignUpsJson { get; set; }
        public string listOfCoursesJson { get; set; }
        private readonly GrowGreenContext _context;
        public DashboardModel(GrowGreenContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            var listOfCourseSignUps = _context.CourseSignups
                .Include(c => c.Course).Where(c => c.Course.LecturerId == 1);

            // foreach (var test in listOfCourseSignUps)
            // {
            //     Console.WriteLine($"listOfCourseSignUps: {test.Course.Lecturer.Courses.}");
            // }

            // listOfCourseSignUpsJson = JsonSerializer.Serialize(listOfCourseSignUps);

            var listOfCourses = _context.Courses.Where(c => c.LecturerId == 1);
            foreach (var i in listOfCourses)
            {
                Console.WriteLine($"i: {i}");
                Console.WriteLine($"i: {i.Name}");
                Console.WriteLine($"i: {i.MaxCapacity}");
            }
            listOfCoursesJson = JsonSerializer.Serialize(listOfCourses);

            return Page();
        }
    }
}
