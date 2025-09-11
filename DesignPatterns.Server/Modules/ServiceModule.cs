using DesignPatterns.Application.Services;
using DesignPatterns.Application.Interfaces;
using DesignPatterns.Application.Factories;
using DesignPatterns.Application.Repositories;
using System.Reflection;

namespace DesignPatterns.Server.Modules;

/// <summary>
/// Module for configuring all application services
/// </summary>
public static class ServiceModule
{
    /// <summary>
    /// Configure all services for the application
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add Core Services
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Configure Swagger/OpenAPI
        services.AddSwaggerConfiguration();

        // Configure CORS
        services.AddCorsConfiguration();

        // Register Application Services
        services.AddPatternServices();

        // Configure JSON Serialization
        services.AddJsonConfiguration();

        return services;
    }

    /// <summary>
    /// Configure Swagger/OpenAPI documentation
    /// </summary>
    private static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Title = "Design Patterns Learning API",
                Version = "v1",
                Description = "A comprehensive .NET API demonstrating all major GoF design patterns with Clean Architecture",
                Contact = new()
                {
                    Name = "Design Patterns Demo",
                    Url = new Uri("https://github.com/your-repo")
                }
            });

            // Include XML comments for better documentation
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        return services;
    }

    /// <summary>
    /// Configure CORS policies
    /// </summary>
    private static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("DevelopmentPolicy", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        return services;
    }

    /// <summary>
    /// Register Design Pattern Services
    /// </summary>
    private static IServiceCollection AddPatternServices(this IServiceCollection services)
    {
        // Register repositories
        services.AddSingleton<IPatternRepository, PatternRepository>();
        
        // Register factories
        services.AddSingleton<IPatternDependencyFactory, PatternDependencyFactory>();
        
        // Register interfaces with their implementations
        services.AddScoped<IPatternInformationService, PatternInformationService>();
        services.AddScoped<ICreationalPatternsService, CreationalPatternsService>();
        services.AddScoped<IStructuralPatternsService, StructuralPatternsService>();
        services.AddScoped<IBehavioralPatternsService, BehavioralPatternsService>();

        return services;
    }

    /// <summary>
    /// Configure JSON serialization options
    /// </summary>
    private static IServiceCollection AddJsonConfiguration(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNamingPolicy = null; // Keep original property names
            options.SerializerOptions.WriteIndented = true; // Pretty print JSON
        });

        return services;
    }
}