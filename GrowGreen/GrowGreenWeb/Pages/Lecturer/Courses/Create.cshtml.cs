using System;
using System.Collections.Generic;
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
        [BindProperty, Required]
        public Course Course { get; set; } = new();
        
        public IActionResult OnGet()
        {
            return Page();
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "There are one or more errors. Please correct and try again.";
                return;
            }
        }
    }
}
