using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Pages.Courses.Viewer
{
    public class VideoViewerModel : PageModel
    {
        public User Learner { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Video Video { get; set; } = null!;

        private readonly GrowGreenContext _context;

        public VideoViewerModel(GrowGreenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int videoId)
        {
            // todo: add account system support
            int learnerId = TemporaryConstants.LearnerId;

            User? learner = await _context.Users.FindAsync(learnerId);

            if (learner is null)
                return Forbid();

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
