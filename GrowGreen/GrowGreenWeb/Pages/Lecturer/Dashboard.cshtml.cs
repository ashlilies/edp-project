using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GrowGreenWeb.Filters;
using GrowGreenWeb.Services;
using Newtonsoft.Json;

namespace GrowGreenWeb.Pages.Lecturer
{
    [Authenticated(AccountType.Lecturer)]
    public class DashboardModel : PageModel
    {
        public string listOfCourseSignUpsJson { get; set; }
        public string listOfCoursesJson { get; set; }
        private readonly GrowGreenContext _context;
        private readonly AccountService _accountService;
        public DashboardModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = _accountService.GetCurrentUser(HttpContext);
            if (user == null)
                return Page();
            
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            
            var listOfCourseSignUps = await _context.CourseSignups
                .Include(c => c.Course)
                .Where(c => c.Course.LecturerId == 1)
                .ToListAsync();

            // foreach (var test in listOfCourseSignUps)
            // {
                // Console.WriteLine($"listOfCourseSignUps: {test.Course.Le}");
            // }

            listOfCourseSignUpsJson = JsonConvert.SerializeObject(listOfCourseSignUps, jsonSettings);

            var listOfCourses = await _context.Courses
                .Include(c => c.Lecturer)
                .Where(c => c.LecturerId == 1)
                .ToListAsync();
            
            foreach (var i in listOfCourses)
            {
                Console.WriteLine($"i: {i}");
                Console.WriteLine($"i: {i.Name}");
                Console.WriteLine($"i: {i.MaxCapacity}");
            }

            listOfCoursesJson = JsonConvert.SerializeObject(listOfCourses, jsonSettings);

            return Page();
        }
    }
}
