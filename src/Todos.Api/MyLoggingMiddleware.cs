namespace Todos.Api;

public class MyLoggingMiddleware
{
    private readonly ILogger<MyLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public MyLoggingMiddleware(ILogger<MyLoggingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Custom logic will go here, before passing the request object (context) on using the request delegate (next)
        _logger.LogInformation($"The client IP Address is: {context.Connection.RemoteIpAddress}");
        _logger.LogInformation($"{context?.Request.Method} {context?.Request.Path}");

        await _next(context);

        // More logic here on the way back out of the middleware pipeline
        _logger.LogInformation("This is printed (from my logging middleware) on the way back out");
    }
}
