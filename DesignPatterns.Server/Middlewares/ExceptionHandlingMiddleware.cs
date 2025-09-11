using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using DesignPatterns.Server.Exceptions;

namespace DesignPatterns.Server.Middlewares;

/// <summary>
/// Advanced middleware for handling global exceptions with detailed error responses
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred. Request: {Method} {Path}", 
                context.Request.Method, context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var (statusCode, response) = GetErrorResponse(exception);
        context.Response.StatusCode = (int)statusCode;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(jsonResponse);
    }

    private (HttpStatusCode statusCode, object response) GetErrorResponse(Exception exception)
    {
        return exception switch
        {
            // Custom application exceptions
            PatternDemonstrationException patternEx => (HttpStatusCode.BadRequest, new ErrorResponse
            {
                Success = false,
                Message = "Pattern demonstration failed",
                Details = patternEx.Message,
                ErrorType = "PatternDemonstrationError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? patternEx.StackTrace : null
            }),
            
            BusinessRuleException businessEx => (HttpStatusCode.BadRequest, new ErrorResponse
            {
                Success = false,
                Message = "Business rule violation",
                Details = businessEx.Message,
                ErrorType = "BusinessRuleError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? businessEx.StackTrace : null
            }),
            
            ConfigurationException configEx => (HttpStatusCode.InternalServerError, new ErrorResponse
            {
                Success = false,
                Message = "Configuration error",
                Details = _environment.IsDevelopment() ? configEx.Message : "System configuration issue",
                ErrorType = "ConfigurationError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? configEx.StackTrace : null
            }),
            
            ServiceUnavailableException serviceEx => (HttpStatusCode.ServiceUnavailable, new ErrorResponse
            {
                Success = false,
                Message = "Service unavailable",
                Details = serviceEx.Message,
                ErrorType = "ServiceUnavailableError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? serviceEx.StackTrace : null
            }),
            
            // Standard .NET exceptions (order matters - more specific first)
            ArgumentNullException nullEx => (HttpStatusCode.BadRequest, new ErrorResponse
            {
                Success = false,
                Message = "Required parameter is missing",
                Details = $"Parameter '{nullEx.ParamName}' cannot be null",
                ErrorType = "ArgumentNullError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? nullEx.StackTrace : null
            }),
            
            ArgumentException argEx => (HttpStatusCode.BadRequest, new ErrorResponse
            {
                Success = false,
                Message = "Invalid argument provided",
                Details = argEx.Message,
                ErrorType = "ArgumentError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? argEx.StackTrace : null
            }),
            
            ValidationException validationEx => (HttpStatusCode.BadRequest, new ErrorResponse
            {
                Success = false,
                Message = "Validation failed",
                Details = validationEx.Message,
                ErrorType = "ValidationError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? validationEx.StackTrace : null
            }),
            
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, new ErrorResponse
            {
                Success = false,
                Message = "Unauthorized access",
                Details = "You are not authorized to perform this action",
                ErrorType = "UnauthorizedError",
                Timestamp = DateTime.UtcNow
            }),
            
            KeyNotFoundException keyNotFoundEx => (HttpStatusCode.NotFound, new ErrorResponse
            {
                Success = false,
                Message = "Resource not found",
                Details = keyNotFoundEx.Message,
                ErrorType = "NotFoundError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? keyNotFoundEx.StackTrace : null
            }),
            
            TimeoutException timeoutEx => (HttpStatusCode.RequestTimeout, new ErrorResponse
            {
                Success = false,
                Message = "Request timeout",
                Details = timeoutEx.Message,
                ErrorType = "TimeoutError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? timeoutEx.StackTrace : null
            }),
            
            NotImplementedException notImplEx => (HttpStatusCode.NotImplemented, new ErrorResponse
            {
                Success = false,
                Message = "Feature not implemented",
                Details = notImplEx.Message,
                ErrorType = "NotImplementedError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? notImplEx.StackTrace : null
            }),
            
            _ => (HttpStatusCode.InternalServerError, new ErrorResponse
            {
                Success = false,
                Message = "An unexpected error occurred",
                Details = _environment.IsDevelopment() ? exception.Message : "Please try again later or contact support",
                ErrorType = "InternalServerError",
                Timestamp = DateTime.UtcNow,
                StackTrace = _environment.IsDevelopment() ? exception.StackTrace : null
            })
        };
    }
}

/// <summary>
/// Standardized error response model
/// </summary>
public class ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }
    public string ErrorType { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string? StackTrace { get; set; }
    public string? TraceId { get; set; }
}