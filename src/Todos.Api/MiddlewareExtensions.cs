namespace Todos.Api;

public static class MiddlewareExtensions
{
    public static WebApplication UseRequestLogger(this WebApplication app)
    {
        app.UseMiddleware<MyLoggingMiddleware>();
        return app;
    }
}
