namespace Core.Middleware;

public class SemesterClosedMiddleware
{
    private readonly RequestDelegate next;

    public SemesterClosedMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context, IConfiguration config)
    {
        var isClosed = config.GetValue<bool>("SemesterClosed");

        if (isClosed && context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable; // 503
            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync("הסמסטר סגור. נא לחזור מאוחר יותר.");
            return; 
        }

        await next.Invoke(context);
    }
}

public static partial class MiddlewareExtensions
{
    public static IApplicationBuilder UseSemesterClosed(this IApplicationBuilder builder)
        => builder.UseMiddleware<SemesterClosedMiddleware>();
}
