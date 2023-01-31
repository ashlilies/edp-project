using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Courses.Viewer
{
    public class UnregisterModel : PageModel
    {
        public User Learner { get; set; } = null!;
        private readonly GrowGreenContext _context;
        public UnregisterModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? learner = await _context.Users.FindAsync(learnerId);

            if (learner is null)
                return Forbid();
            Learner = learner;

            Course? course = await _context.Courses.Include(c => c.Learners).SingleOrDefaultAsync(c => c.Id == id);
            if (course is null)
                return NotFound();

            // delete registration record
            if (course.Learners.Contains(Learner))
            {
                course.Learners.Remove(Learner);
                await _context.SaveChangesAsync();

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully deregistered from course";
            }

            return RedirectToPage("../Details", new { id });
        }
    }
}
