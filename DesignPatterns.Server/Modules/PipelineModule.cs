namespace DesignPatterns.Server.Modules;

/// <summary>
/// Module for configuring the application pipeline and middleware
/// </summary>
public static class PipelineModule
{
    /// <summary>
    /// Configure the complete application pipeline
    /// </summary>
    public static WebApplication ConfigureApplicationPipeline(this WebApplication app)
    {
        // Configure Development Environment
        app.ConfigureDevelopmentEnvironment();

        // Configure Core Middleware
        app.ConfigureCoreMiddleware();

        // Configure API Endpoints
        app.ConfigureApiEndpoints();

        return app;
    }

    /// <summary>
    /// Configure development-specific middleware
    /// </summary>
    private static WebApplication ConfigureDevelopmentEnvironment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Design Patterns API v1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at app's root
                c.DocumentTitle = "Design Patterns Learning API";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            app.UseCors("DevelopmentPolicy");
        }

        return app;
    }

    /// <summary>
    /// Configure core middleware pipeline
    /// </summary>
    private static WebApplication ConfigureCoreMiddleware(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();

        return app;
    }

    /// <summary>
    /// Configure API endpoints
    /// </summary>
    private static WebApplication ConfigureApiEndpoints(this WebApplication app)
    {
        // Health check endpoint
        app.MapGet("/health", () => new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0",
            Message = "Design Patterns API is running successfully"
        })
        .WithName("HealthCheck")
        .WithTags("Health")
        .Produces(200);

        // Root endpoint redirect
        app.MapGet("/", () => Results.Redirect("/api/DesignPatterns"))
        .WithName("Root")
        .ExcludeFromDescription();

        // Fallback for unknown routes
        app.MapFallback(() => Results.Json(new
        {
            Error = "Endpoint not found",
            Message = "Please visit /api/DesignPatterns for available endpoints",
            AvailableEndpoints = new
            {
                Welcome = "/api/DesignPatterns",
                AllPatterns = "/api/DesignPatterns/all-patterns",
                CreationalPatterns = "/api/CreationalPatterns",
                StructuralPatterns = "/api/StructuralPatterns",
                BehavioralPatterns = "/api/BehavioralPatterns",
                Health = "/health",
                ApiDocs = "/swagger"
            }
        }, statusCode: 404));

        return app;
    }
}