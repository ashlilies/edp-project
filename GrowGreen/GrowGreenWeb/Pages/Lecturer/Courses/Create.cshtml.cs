using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrowGreenWeb.Pages.Lecturer.Courses
{
    public class CreateModel : PageModel
    {
        [BindProperty, Required, MaxLength(100), DisplayName("Title")]
        public string Name { get; set; } = null!;

        [BindProperty, Required, MaxLength(1000)]
        public string Description { get; set; } = null!;

        [BindProperty, Required, DisplayName("Starting Availability Date"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [BindProperty, Required, DisplayName("Ending Availability Date"), DataType(DataType.Date)]

        public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1);
        
        private readonly GrowGreenContext _context;
        
        public CreateModel(GrowGreenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // todo: add login system support here
            int lecturerId = TemporaryConstants.LecturerId;

            if (StartDate < DateTime.Today)
            {
                ModelState.AddModelError(nameof(StartDate), "Start date cannot be before today");
            }
            
            if (EndDate <= StartDate)
            {
                ModelState.AddModelError(nameof(EndDate), "End date cannot be before start date");
            }
            
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "There are one or more errors. Please correct and try again.";
                
                return OnGet();
            }

            Course course = new Course()
            {
                Name = Name,
                Description = Description,
                StartDate = StartDate,
                EndDate = EndDate,
                LecturerId = lecturerId
            };
            
            // check if another course with a similar name already exists
            Course? check = _context.Courses.SingleOrDefault(c => c.Name == Name);
            if (check != null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "A course with a similar name already exists.";
                
                return OnGet();
            }
            
            _context.Add(course);
            await _context.SaveChangesAsync();
            
            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Successfully created course. Now add some content!";
            
            return RedirectToPage("Index");
        }
    }
}