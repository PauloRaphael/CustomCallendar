using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class PasswordAuthorizeAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var session = context.HttpContext.Session;

        // Allow certain paths without authentication
        var path = context.HttpContext.Request.Path.Value?.ToLower();
        if (path == "/login" || path.StartsWith("/staticfiles")) // Add other paths if needed
        {
            await next();
            return;
        }

        // Check if user is authenticated
        if (session == null || session.GetString("Authenticated") != "true")
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
            return;
        }

        await next();
    }
}
