using GrowGreenWeb.Models;
using GrowGreenWeb.Services;

namespace GrowGreenWeb.Middleware;

/// <summary>
/// Ashlee: Only function of this middleware is to redirect users to the appropriate home URL
/// if they are signed in and trying to view another role's page
/// </summary>
public class UnauthenticatedOrAuthenticatedMiddleware
{
    private readonly RequestDelegate _next;

    public UnauthenticatedOrAuthenticatedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        AccountService svc = context.RequestServices.GetRequiredService<AccountService>();

        User? user = svc.GetCurrentUser(context);
        if (user != null)
        {
            string prevUrl = GetPrevUrl(user);
            
            if (!user.IsAdmin && context.Request.Path.StartsWithSegments(new PathString("/Admin")))
                context.Response.Redirect(prevUrl);
            else if (!user.IsLecturer && context.Request.Path.StartsWithSegments(new PathString("/Lecturer")))
                context.Response.Redirect(prevUrl);
            else if (!user.IsLearner && context.Request.Path.StartsWithSegments(new PathString("/")))
                context.Response.Redirect(prevUrl);
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }

    /// <summary>
    /// Based on the user type, we select the appropriate 'home' URL for this user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private string GetPrevUrl(User user)
    {
        if (user.IsAdmin)
        {
            return "/Admin/Index";
        }
        else if (user.IsLecturer)
        {
            return "/Lecturer/Index";
        }
        else
        {
            return "/";
        }
    }
}