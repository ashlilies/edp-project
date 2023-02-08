using GrowGreenWeb.Models;
using GrowGreenWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GrowGreenWeb.Filters;

/// <summary>
/// added by ashlee, irfan you can edit this if needed
/// use [Authenticated(AccountType.Learner)] to check if learner is logged in,
/// and so on for other user types such as Lecturer and Admin.
/// </summary>
public class AuthenticatedAttribute : ActionFilterAttribute
{
    private AccountType AccountType { get; init; }

    public AuthenticatedAttribute(AccountType accountType)
    {
        AccountType = accountType;
    }


    public override async void OnResultExecuting(ResultExecutingContext context)
    {
        base.OnResultExecuting(context);

        AccountService svc = context.HttpContext.RequestServices.GetRequiredService<AccountService>();

        User? user = svc.GetCurrentUser(context.HttpContext, AccountType);
        if (user != null)
        {
            context.HttpContext.Items.Add("User", user);
            return;
        }

        string prevUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
        context.Result = new RedirectToPageResult(Constants.UnauthorizedRedirect, new
        {
            prevUrl = prevUrl
        });
        await context.Result.ExecuteResultAsync(context);
    }
}