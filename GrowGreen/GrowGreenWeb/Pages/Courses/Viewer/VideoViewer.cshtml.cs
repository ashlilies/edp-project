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

namespace GrowGreenWeb.Pages.Courses.Viewer
{
    [Authenticated(AccountType.Learner)]
    public class VideoViewerModel : PageModel
    {
        public User Learner { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Video Video { get; set; } = null!;

        private readonly GrowGreenContext _context;
        private AccountService _accountService;

        public VideoViewerModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGetAsync(int videoId)
        {
            User learner = _accountService.GetCurrentUser(HttpContext)!;

            Learner = learner;
            
            Video? video = await _context.Videos
                .Include(v=> v.Lecture).ThenInclude(l => l.Course).ThenInclude(c => c.Lecturer)
                .SingleOrDefaultAsync(v => v.Id == videoId);
            
            if (video is null)
                return NotFound();

            Course = video.Lecture.Course;
            Video = video;
            ViewData["CourseId"] = Course.Id;

            // load lecture video
;

            return Page();
        }
    }
}
