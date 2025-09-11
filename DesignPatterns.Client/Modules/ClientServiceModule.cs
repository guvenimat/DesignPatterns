using DesignPatterns.Client.Components;
using DesignPatterns.Client.Services;

namespace DesignPatterns.Client.Modules;

/// <summary>
/// Module for configuring all client services
/// </summary>
public static class ClientServiceModule
{
    /// <summary>
    /// Configure all services for the Blazor client application
    /// </summary>
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        // Add Blazor Services
        services.AddBlazorServices();

        // Configure HTTP Client
        services.AddHttpClientConfiguration();

        // Add Additional Services
        services.AddAdditionalServices();

        return services;
    }

    /// <summary>
    /// Configure Blazor-specific services
    /// </summary>
    private static IServiceCollection AddBlazorServices(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        return services;
    }

    /// <summary>
    /// Configure HTTP Client for API communication
    /// </summary>
    private static IServiceCollection AddHttpClientConfiguration(this IServiceCollection services)
    {
        services.AddHttpClient<IDesignPatternsApiService, DesignPatternsApiService>(client =>
        {
            // Configure the base address for the API
            client.BaseAddress = new Uri("http://localhost:5071/"); // Your API server address
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        return services;
    }

    /// <summary>
    /// Configure additional services
    /// </summary>
    private static IServiceCollection AddAdditionalServices(this IServiceCollection services)
    {
        // Add logging
        services.AddLogging();

        return services;
    }
}