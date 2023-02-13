using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GrowGreenWeb.Filters;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Lecturer.Courses.Manage.Quizzes
{
    [Authenticated(AccountType.Lecturer)]
    public class CreateQuizModel : PageModel
    {
        public Quiz? QuizEdit { get; set; }

        public Course Course { get; set; }


        public List<SelectListItem> LecturesSelect { get; set; }

        [BindProperty, Required]
        public PostQuizModel PostQuizModel { get; set; } = new();

        private readonly GrowGreenContext _context;

        public CreateQuizModel(GrowGreenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);

            if (course is null)
            {
                return RedirectToPage("/Lecturer/Courses/Index");
            }

            Course = course;
            ViewData["CourseId"] = Course.Id;

            // load lectures
            LecturesSelect = Course.Lectures
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                })
                .ToList();

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);

            if (course is null)
            {
                return RedirectToPage("/Lecturer/Courses/Index");
            }

            Course = course;
            ViewData["CourseId"] = Course.Id;
            
            if (!ModelState.IsValid
                || (PostQuizModel.IsAttachedToLecture && PostQuizModel.LectureId is null))
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "There were some errors. Please fix and try again.";
                return OnGet(id);
            }
            
            // check existing quiz name
            if (_context.Quizzes
                .Any(q => q.Name == PostQuizModel.Name && q.CourseId == Course.Id))
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "There is another quiz with this name";
                return OnGet(id);
            }

            Quiz quiz = new Quiz
            {
                Name = PostQuizModel.Name,
                Description = PostQuizModel.Description,
                StartDate = PostQuizModel.StartDate,
                LectureId = PostQuizModel.LectureId,
                CourseId = Course.Id
            };

            _context.Add(quiz);
            _context.SaveChanges();
            
            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Successfully created quiz. Now add some questions!";

            return RedirectToPage("/Lecturer/Courses/Manage/Index", new { courseId = Course.Id});
        }
    }

    public class PostQuizModel
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(2000)]
        public string Description { get; set; } = null!;

        [Required, DisplayName("Start Date"), DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        public bool IsAttachedToLecture { get; set; } = false;

        public int? LectureId { get; set; }
    }
}