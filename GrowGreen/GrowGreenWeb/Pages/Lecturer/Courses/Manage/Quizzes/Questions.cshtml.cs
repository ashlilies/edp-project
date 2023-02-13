using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Filters;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Lecturer.Courses.Manage.Quizzes
{
    [Authenticated(AccountType.Lecturer)]
    public class QuestionsModel : PageModel
    {
        public Course Course { get; set; }
        public Quiz Quiz { get; set; }
        public List<QuizQuestion> Questions { get; set; }
        private readonly GrowGreenContext _context;

        public QuestionsModel(GrowGreenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, int quizId)
        {
            Course? course = _context.Courses.Find(id);
            Quiz? quiz = _context.Quizzes.Find(quizId);
            if (course is null || quiz is null)
                return RedirectToPage("/Lecturer/Courses/Index");

            ViewData["CourseId"] = id;

            Questions = _context.QuizQuestions
                .Where(q => q.QuizId == quizId)
                .ToList();
            Course = course;
            Quiz = quiz;

            return Page();
        }
    }
}
