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

namespace GrowGreenWeb.Pages.Admin
{
    [Authenticated(AccountType.Admin)]
    public class DashboardModel : PageModel
    {
        public string listOfCoursesJson { get; set; }
        public string listOfEventsJson { get; set; }
        public List<Course> listOfCourses = new List<Course>();
        public List<Event> listOfEvents = new List<Event>();
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

            listOfCourses = await _context.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.CourseSignups)
                .ToListAsync();

            listOfCoursesJson = JsonConvert.SerializeObject(listOfCourses, jsonSettings);

            listOfEvents = await _context.Events.ToListAsync();

            listOfEventsJson = JsonConvert.SerializeObject(listOfEvents, jsonSettings);

            return Page();
        }
    }
}
