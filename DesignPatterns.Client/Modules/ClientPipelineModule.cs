using DesignPatterns.Client.Components;

namespace DesignPatterns.Client.Modules;

/// <summary>
/// Module for configuring the client application pipeline
/// </summary>
public static class ClientPipelineModule
{
    /// <summary>
    /// Configure the complete client application pipeline
    /// </summary>
    public static WebApplication ConfigureClientPipeline(this WebApplication app)
    {
        // Configure Development Environment
        app.ConfigureClientEnvironment();

        // Configure Core Middleware
        app.ConfigureClientMiddleware();

        // Configure Blazor Components
        app.ConfigureBlazorComponents();

        return app;
    }

    /// <summary>
    /// Configure environment-specific settings
    /// </summary>
    private static WebApplication ConfigureClientEnvironment(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios.
            app.UseHsts();
        }

        return app;
    }

    /// <summary>
    /// Configure core middleware pipeline
    /// </summary>
    private static WebApplication ConfigureClientMiddleware(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        return app;
    }

    /// <summary>
    /// Configure Blazor components and rendering
    /// </summary>
    private static WebApplication ConfigureBlazorComponents(this WebApplication app)
    {
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        return app;
    }
}