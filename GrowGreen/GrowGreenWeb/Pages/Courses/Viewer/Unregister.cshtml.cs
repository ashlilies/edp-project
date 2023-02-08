using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Courses.Viewer
{
    public class UnregisterModel : PageModel
    {
        public User Learner { get; set; } = null!;
        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public UnregisterModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            User learner = _accountService.GetCurrentUser(HttpContext)!;
            Learner = learner;

            CourseSignup? courseSignup = await _context.CourseSignups
                .Include(cs => cs.Course)
                .Include(cs => cs.Learner)
                .SingleOrDefaultAsync(cs => cs.CourseId == id);

            if (courseSignup is null)
                return NotFound();

            // delete registration record
            _context.Remove(courseSignup);
            await _context.SaveChangesAsync();

            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "Successfully deregistered from course";
            return RedirectToPage("../Details", new
            {
                id
            });
        }
    }
}