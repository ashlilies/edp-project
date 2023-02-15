using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GrowGreenWeb.Filters;
using GrowGreenWeb.Services;
using Newtonsoft.Json;

namespace GrowGreenWeb.Pages.Lecturer
{
    [Authenticated(AccountType.Lecturer)]
    public class DashboardModel : PageModel
    {
        public string listOfCoursesJson { get; set; }
        public List<Course> listOfCourses = new List<Course>();
        private readonly GrowGreenContext _context;
        private readonly AccountService _accountService;
        public DashboardModel(GrowGreenContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = _accountService.GetCurrentUser(HttpContext);

            if (user == null)
                return Page();

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            listOfCourses = await _context.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.CourseSignups)
                .Where(c => c.LecturerId == user!.Id)
                .ToListAsync();

            listOfCoursesJson = JsonConvert.SerializeObject(listOfCourses, jsonSettings);

            return Page();
        }

        public FileResult OnPost(string PdfHtml)
        {
            Byte[] res = null;
            // PdfHtml = PdfHtml.Insert(0, "<style>* { font-family: \"Arial\", \"sans-serif\"</style>");
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(PdfHtml, PdfSharp.PageSize.A4);
                // VetCV.HtmlRendererCore.PdfSharpCore.PdfGenerator.AddFontFamilyMapping();
                // var pdf = VetCV.HtmlRendererCore.PdfSharpCore.PdfGenerator.GeneratePdf(PdfHtml, PdfSharpCore.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }

            return File(res, "application/octet-stream", "course_report.pdf");
        }

    }
}
