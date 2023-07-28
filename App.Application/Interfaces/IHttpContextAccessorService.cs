using Microsoft.AspNetCore.Http;

namespace App.Application.Interfaces;

/// <summary>
/// Represents a service for accessing the HttpContext.
/// </summary>
public interface IHttpContextAccessorService
{
    /// <summary>
    /// Gets the origin URL of the current HttpContext.
    /// </summary>
    /// <returns>The origin URL of the current HttpContext.</returns>
    string GetOrigin();

    /// <summary>
    /// Gets the HttpResponse instance of the current HttpContext.
    /// </summary>
    /// <returns>The HttpResponse instance of the current HttpContext.</returns>
    HttpResponse GetResponse();

    /// <summary>
    /// Gets the HttpRequest instance of the current HttpContext.
    /// </summary>
    /// <returns>The HttpRequest instance of the current HttpContext.</returns>
    HttpRequest GetRequest();
}
