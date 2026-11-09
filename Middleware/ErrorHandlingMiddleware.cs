using System.Net;
using System.Text.Json;


namespace Core.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    _next = next;
    _logger = logger;
}

private Task HandleExceptionAsync(HttpContext context, Exception exception){
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = 500;
 return context.Response.WriteAsync(JsonSerializer.Serialize(new
  { error = "something went wrong", status = 500 }));
}

 public async Task Invoke(HttpContext context){   
try
    {
        await _next(context);
    }
    catch (Exception ex)
    {    
        _logger.LogError(ex, "error occurred while processing the request.");

        await HandleExceptionAsync(context, ex);
    }
}
}