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
    public class ManageModel : PageModel
    {
        public int CourseId { get; set; }
        [BindProperty, Required, MaxLength(100), DisplayName("Title")]
        public string Name { get; set; } = null!;

        [BindProperty, Required, MaxLength(1000)]
        public string Description { get; set; } = null!;

        [BindProperty, Required, DisplayName("Starting Availability Date"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [BindProperty, Required, DisplayName("Ending Availability Date"), DataType(DataType.Date)]

        public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1);
        private readonly GrowGreenContext _context;

        public ManageModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            // todo: add account system support
            int lecturerId = TemporaryConstants.LecturerId;

            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            CourseId = course.Id;
            Name = course.Name;
            Description = course.Description;
            StartDate = course.StartDate;
            EndDate = course.EndDate;

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // todo: add login system support here
            int lecturerId = TemporaryConstants.LecturerId;
            
            if (EndDate <= StartDate)
            {
                ModelState.AddModelError(nameof(EndDate), "End date cannot be before start date");
            }
            
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "There are one or more errors. Please correct and try again.";

                return await OnGet(id);
            }

            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return NotFound();
            
            if (course.LecturerId != lecturerId)
                return Forbid();
            
            // check if another course with a similar name already exists
            Course? check = _context.Courses.SingleOrDefault(c => c.Name == Name && c.Id != course.Id);
            if (check != null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Another course with a similar name already exists.";
                
                return await OnGet(id);
            }
            
            // update course details
            course.Name = Name;
            course.Description = Description;
            course.StartDate = StartDate;
            course.EndDate = EndDate;
            
            _context.Update(course);
            await _context.SaveChangesAsync();
            
            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Successfully updated course details.";

            return await OnGet(id);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // todo: add account system support
            int lecturerId = TemporaryConstants.LecturerId;

            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            // delete course
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Successfully deleted course.";

            return RedirectToPage("Index");
        }
    }
}