namespace Presentation.Common.Middleware;

public class RedirectMiddleware
{
    private readonly RequestDelegate _next;

    public RedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/")
        {
            context.Response.Redirect("/master-kennung");
            return;
        }

        await _next(context);
    }
}
