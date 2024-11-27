public class PasswordMiddleware
{
    private readonly RequestDelegate _next;

    public PasswordMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Skip login page and static files
        if (context.Request.Path.StartsWithSegments("/Login") || context.Request.Path.StartsWithSegments("/css") || context.Request.Path.StartsWithSegments("/js"))
        {
            await _next(context);
            return;
        }

        // Check session or query for password
        if (!context.Session.Keys.Contains("Authenticated") || context.Session.GetString("Authenticated") != "true")
        {
            context.Response.Redirect("/Login");
            return;
        }

        await _next(context);
    }
}
