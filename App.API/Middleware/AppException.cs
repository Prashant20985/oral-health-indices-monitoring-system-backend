namespace App.API.Middleware;

/// <summary>
/// Represents an exception with its status code, message, and details.
/// </summary>
public class AppException
{
    /// <summary>
    /// Initializes an instance of AppException with the specified status code, message, and details.
    /// </summary>
    /// <param name="statusCode">The status code of the exception.</param>
    /// <param name="message">The message of the exception.</param>
    /// <param name="details">The details of the exception.</param>
    public AppException(int statusCode, string message, string details = null)
    {
        Details = details;
        StatusCode = statusCode;
        Message = message;
    }

    /// <summary>
    /// Gets or sets the status code of the exception.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the message of the exception.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the details of the exception.
    /// </summary>
    public string Details { get; set; }
}

