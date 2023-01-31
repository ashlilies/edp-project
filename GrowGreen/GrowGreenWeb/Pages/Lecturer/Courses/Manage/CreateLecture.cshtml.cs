using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GrowGreenWeb.Pages.Lecturer.Courses.Manage
{
    public class CreateLectureModel : PageModel
    {
        [BindProperty, DisplayName("Title*"), Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [BindProperty, DisplayName("Starting Availability Date*"), Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [BindProperty, DisplayName("Description*"), Required, MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public Course Course { get; set; } = null!;
        public Lecture? LectureEdit { get; set; } = null!;
        private readonly GrowGreenContext _context;

        public CreateLectureModel(GrowGreenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, int? lectureId = null)
        {
            // todo: add account system support
            int lecturerId = TemporaryConstants.LecturerId;

            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;
            ViewData["CourseId"] = course.Id;

            // load edit
            if (lectureId is not null)
            {
                LectureEdit = _context.Lectures.Find(lectureId);
                if (LectureEdit is null)
                    return NotFound();

                Title = LectureEdit.Name;
                StartDate = LectureEdit.StartDate;
                Description = LectureEdit.Description;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int? lectureId = null)
        {
            // todo: add account system support
            int lecturerId = TemporaryConstants.LecturerId;

            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;

            // load edit
            if (lectureId is not null)
            {
                LectureEdit = await _context.Lectures.FindAsync(lectureId);
                if (LectureEdit is null)
                    return NotFound();
            }

            Lecture? checkName = await _context.Lectures
                .Where(l => l.CourseId == Course.Id)
                .SingleOrDefaultAsync(l => l.Name == Title);

            if ((checkName is not null && LectureEdit is null)
                || (checkName is not null && LectureEdit is not null && checkName.Id != LectureEdit.Id))
            {
                ModelState.AddModelError(nameof(Title), "Existing lecture with same title already exists");
            }

            if (StartDate < DateTime.Today && StartDate != LectureEdit?.StartDate)
            {
                ModelState.AddModelError(nameof(StartDate), "Start date cannot be before today");
            }

            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description))
            {
                return OnGet(id);
            }

            Lecture lecture;
            if (LectureEdit is null)
            {
                lecture = new Lecture
                {
                    CourseId = Course.Id,
                    Name = Title,
                    Description = Description,
                    StartDate = StartDate,
                    Timestamp = DateTime.Now
                };

                await _context.AddAsync(lecture);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully added lecture " + Title;
            }
            else
            {
                LectureEdit.Name = Title;
                LectureEdit.Description = Description;
                LectureEdit.StartDate = StartDate;

                lecture = LectureEdit;
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully updated lecture " + Title;
            }

            await _context.SaveChangesAsync();


            return RedirectToPage("Contents", new { id, lectureId = lecture.Id });
        }
    }
}