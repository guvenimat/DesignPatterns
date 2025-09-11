using DesignPatterns.Server.Middlewares;

namespace DesignPatterns.Server.Modules;

/// <summary>
/// Extension methods for configuring custom middlewares
/// </summary>
public static class MiddlewareModule
{
    /// <summary>
    /// Add custom middlewares to the pipeline
    /// </summary>
    public static WebApplication UseCustomMiddlewares(this WebApplication app)
    {
        // Add exception handling middleware (should be early in pipeline)
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        
        // Add request logging middleware
        app.UseMiddleware<RequestLoggingMiddleware>();

        return app;
    }
}