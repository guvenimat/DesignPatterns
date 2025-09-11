using Microsoft.AspNetCore.Mvc;
using DesignPatterns.Domain.Constants;

namespace DesignPatterns.Server.Controllers;

/// <summary>
/// Base controller for standardized API responses
/// </summary>
[ApiController]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Creates a standardized success response
    /// </summary>
    /// <param name="data">Data to return</param>
    /// <param name="message">Success message</param>
    /// <returns>Standardized success response</returns>
    protected IActionResult Success(object data, string? message = null)
    {
        var response = CreateStandardResponse(true, message ?? PatternConstants.SUCCESS_MESSAGE, data);
        return Ok(response);
    }

    /// <summary>
    /// Creates a standardized success response without data
    /// </summary>
    /// <param name="message">Success message</param>
    /// <returns>Standardized success response</returns>
    protected IActionResult Success(string message = PatternConstants.SUCCESS_MESSAGE)
    {
        var response = CreateStandardResponse(true, message);
        return Ok(response);
    }

    /// <summary>
    /// Creates a standardized validation error response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="errors">Validation errors</param>
    /// <returns>Standardized validation error response</returns>
    protected IActionResult ValidationError(string message, object? errors = null)
    {
        var response = CreateStandardResponse(false, message, errors: errors);
        return BadRequest(response);
    }

    /// <summary>
    /// Creates a standardized not found response
    /// </summary>
    /// <param name="message">Not found message</param>
    /// <returns>Standardized not found response</returns>
    protected IActionResult NotFoundResult(string message = PatternConstants.NOT_FOUND_MESSAGE)
    {
        var response = CreateStandardResponse(false, message);
        return NotFound(response);
    }

    /// <summary>
    /// Creates a standardized forbidden response
    /// </summary>
    /// <param name="message">Forbidden message</param>
    /// <returns>Standardized forbidden response</returns>
    protected IActionResult ForbiddenResult(string message = PatternConstants.FORBIDDEN_MESSAGE)
    {
        var response = CreateStandardResponse(false, message);
        return StatusCode(StatusCodes.Status403Forbidden, response);
    }

    /// <summary>
    /// Creates a standardized response object
    /// </summary>
    /// <param name="success">Success status</param>
    /// <param name="message">Response message</param>
    /// <param name="data">Response data</param>
    /// <param name="errors">Error details</param>
    /// <returns>Standardized response object</returns>
    private object CreateStandardResponse(bool success, string message, object? data = null, object? errors = null)
    {
        var response = new
        {
            success,
            message,
            data,
            errors,
            timestamp = DateTime.UtcNow
        };

        return data == null && errors == null 
            ? new { success, message, timestamp = response.timestamp }
            : response;
    }
}