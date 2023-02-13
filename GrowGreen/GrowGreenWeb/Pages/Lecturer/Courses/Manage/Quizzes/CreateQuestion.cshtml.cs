using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Filters;
using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GrowGreenWeb.Pages.Lecturer.Courses.Manage.Quizzes
{
    [Authenticated(AccountType.Lecturer)]
    public class CreateQuestionModel : PageModel
    {
        [BindProperty, Required, DisplayName("Title")]
        public string Name { get; set; }

        [BindProperty, Required, Range(1, 100)]
        public int Points { get; set; }

        [BindProperty, DisplayName("Choice 1 (Correct)"), Required]
        public string Choice1 { get; set; }

        [BindProperty, DisplayName("Choice 2 (Wrong)"), Required]
        public string Choice2 { get; set; }

        [BindProperty, DisplayName("Choice 3 (Wrong)"), Required]
        public string Choice3 { get; set; }

        [BindProperty, DisplayName("Choice 4 (Wrong)"), Required]
        public string Choice4 { get; set; }

        [BindProperty, Required]
        public int CorrectChoiceNo { get; set; }

        public Course Course { get; set; }
        public Quiz Quiz { get; set; }

        private readonly GrowGreenContext _context;

        public CreateQuestionModel(GrowGreenContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, int quizId)
        {
            Course? course = _context.Courses.Find(id);
            Quiz? quiz = _context.Quizzes.Find(quizId);

            if (course is null || quiz is null)
                return RedirectToPage("/Lecturer/Courses/Index");

            Course = course;
            Quiz = quiz;
            ViewData["CourseId"] = Course.Id;


            return Page();
        }

        public IActionResult OnPost(int id, int quizId)
        {
            if (!ModelState.IsValid)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "There were some errors. Please try again.";
                return OnGet(id, quizId);
            }

            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Successfully added question";
            return OnGet(id, quizId);
        }
    }
}