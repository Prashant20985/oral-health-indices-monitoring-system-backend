using System.Net;
using System.Text.Json;

namespace App.API.Middleware;


/// <summary>
/// Middleware for handling exceptions in the application.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Initializes the ExceptionMiddleware with the specified dependencies.
    /// </summary>
    /// <param name="next">The next RequestDelegate in the pipeline.</param>
    /// <param name="logger">The ILogger instance for logging exceptions.</param>
    /// <param name="env">The IHostEnvironment instance representing the hosting environment.</param>
    public ExceptionMiddleware(RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The current HttpContext.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, ex.Message);

            // Set the response status code to 500 (Internal Server Error)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Create an instance of AppException to represent the exception details
            var res = new AppException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString());

            // Serialize the AppException to JSON format
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(res, options);

            // Write the JSON response to the HTTP response
            await context.Response.WriteAsync(json);
        }
    }
}
