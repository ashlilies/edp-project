using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GrowGreenWeb.Helpers;
using GrowGreenWeb.Services;

namespace GrowGreenWeb.Pages.Lecturer.Courses.Manage
{
    public class CreateVideoModel : PageModel
    {
        [BindProperty, DisplayName("Video File (mp4, ogg)")]
        public IFormFile? VideoFile { get; set; } = null!;

        [BindProperty, Required, DisplayName("Name")]
        public string Title { get; set; } = string.Empty;

        [BindProperty, Required, DisplayName("Transcript")]
        public string Description { get; set; } = string.Empty;

        public Course Course { get; set; } = null!;
        public Lecture Lecture { get; set; } = null!;

        public Video? VideoEdit { get; set; }

        private readonly GrowGreenContext _context;
        private readonly IWebHostEnvironment _environment;
        private AccountService _accountService;

        public CreateVideoModel(GrowGreenContext context, IWebHostEnvironment environment, AccountService accountService)
        {
            _context = context;
            _environment = environment;
            _accountService = accountService;
        }

        public IActionResult OnGet(int id, int lectureId, int? videoId = null)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;
            ViewData["CourseId"] = course.Id;

            Lecture? lecture = _context.Lectures
                .Include(l => l.Videos)
                .SingleOrDefault(l => l.Id == lectureId);

            if (lecture is null)
                return NotFound();
            if (lecture.CourseId != course.Id)
                return Forbid();

            Lecture = lecture;

            if (videoId is not null)
            {
                // retrieve the video and set the properties accordingly
                VideoEdit = _context.Videos.Find(videoId);

                if (VideoEdit is null)
                    return NotFound();

                Title = VideoEdit.Name;
                Description = VideoEdit.Transcript;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, int lectureId, int? videoId = null)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;
            Lecture? lecture = _context.Lectures
                .Include(l => l.Videos)
                .SingleOrDefault(l => l.Id == lectureId);

            if (lecture is null)
                return NotFound();
            if (lecture.CourseId != course.Id)
                return Forbid();

            Lecture = lecture;

            string? webRootPath = null;
            if (VideoFile is null && videoId is null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = "Error uploading video";
                return OnGet(id, lectureId);
            }
            else if (VideoFile is not null)
            {
                if (!Constants.AllowedVideoExtensions.Contains(Path.GetExtension(VideoFile.FileName)))
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = "Video file type not allowed!";
                    return OnGet(id, lectureId);
                }

                string random = Guid.NewGuid().ToString();
                webRootPath = "/uploads/course/" + Course.Id + "/lecture/" + Lecture.Id + "/" + random + "-" +
                              VideoFile.FileName;
                var directory = Path.Combine(
                    _environment.WebRootPath, "uploads", "course", Course.Id.ToString(), "lecture",
                    Lecture.Id.ToString());

                var file = Path.Combine(directory, random + "-" + VideoFile.FileName);

                Directory.CreateDirectory(directory);

                await using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await VideoFile.CopyToAsync(fileStream);
                }

                // create a preview image
                string outputImgPath = "wwwroot" + webRootPath + ".jpg";
                string ffmpegVideoPath = "wwwroot" + webRootPath;

                FfmpegHelper.GetThumbnail(ffmpegVideoPath, outputImgPath, null);
            }


            //ImageUrl = webRootPath;

            // upload new video into db
            if (videoId is null)
            {
                Video video = new Video
                {
                    Name = Title,
                    Timestamp = DateTime.Now,
                    Transcript = Description,
                    Url = webRootPath!,
                    LectureId = Lecture.Id,
                    PreviewUrl = webRootPath! + ".jpg"
                };

                _context.Add(video);

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully uploaded video";
            }
            else
            {
                Video? video = await _context.Videos.FindAsync(videoId);
                if (video is null)
                    return NotFound();

                video.Name = Title;
                video.Transcript = Description;

                if (webRootPath is not null)
                {
                    video.Url = webRootPath;
                    video.Timestamp = DateTime.Now;
                    video.PreviewUrl = webRootPath + ".jpg";
                }

                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = "Successfully updated video";
            }

            await _context.SaveChangesAsync();


            return RedirectToPage("Contents", new { id, lectureId });
        }

        public IActionResult OnPostDelete(int id, int lectureId, int videoId)
        {
            int lecturerId = _accountService.GetCurrentUser(HttpContext)!.Id;

            Course? course = _context.Courses
                .Include(c => c.Lectures)
                .SingleOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();

            if (course.LecturerId != lecturerId)
                return Forbid();

            Course = course;
            Lecture? lecture = _context.Lectures
                .Include(l => l.Videos)
                .SingleOrDefault(l => l.Id == lectureId);

            if (lecture is null)
                return NotFound();
            if (lecture.CourseId != course.Id)
                return Forbid();

            // delete the video
            Video? video = _context.Videos.Find(videoId);
            if (video is null)
            {
                return NotFound();
            }

            _context.Remove(video);
            _context.SaveChanges();

            return RedirectToPage("Contents", new { id, lectureId });
        }
    }
}