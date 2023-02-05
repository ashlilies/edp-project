using System.Text;
using GrowGreenWeb.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace GrowGreenWeb.Services;

public class SidebarService
{
    private readonly GrowGreenContext _context;

    public SidebarService(GrowGreenContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Put this in a col-3 div
    /// </summary>
    /// <param name="courseId"></param>
    /// <returns></returns>
    public async Task<HtmlString> GetLearnerCourseSidebar(int courseId)
    {
        // <h5>@Model.Course.Name</h5>
        //     <hr />
        //     <h5>
        //     <a asp-page="QnA" asp-route-id="@Model.Course.Id" class="text-reset text-decoration-none">Q&amp;A</a>
        //     </h5>
        //     <hr />
        //     <h5>Contents</h5>

        Course? course = await _context.Courses
            .Include(c => c.Lectures)
            .SingleOrDefaultAsync(c => c.Id == courseId);

        if (course is null)
            return new HtmlString("Error loading course");

        StringBuilder sb = new StringBuilder();

        // base ones
        sb.Append("<div class=\"mb-3 card p-2\"><div class=\"card-body\">");
        
        sb.Append($"<h5>{@course.Name}</h5>");
        sb.Append("<hr />");
        sb.Append("<h5><a asp-page=\"QnA\" asp-route-id=\"@Model.Course.Id\" " +
                  "class=\"text-reset text-decoration-none\">Q&amp;A</a></h5>");
        sb.Append("<hr />");
        sb.Append("<h5>Contents</h5>");

        foreach (Lecture lecture in course.Lectures)
        {
            sb.Append("<a asp-page=\"Contents\" asp-route-id=\"@lecture.Course.Id\"" +
                      "asp-route-lectureId=\"@lecture.Id\" class=\"text-reset text-decoration-none\">" +
                      "@lecture.Name" +
                      "</a><br/>");
        }
        
        sb.Append("</div></div>");
        return new HtmlString(sb.ToString());
    }
}