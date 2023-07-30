using App.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace App.Infrastructure.ContextAccessor;

/// <summary>
/// Represents a service for accessing the HttpContext.
/// </summary>
public class HttpContextAccessorService : IHttpContextAccessorService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpContextAccessorService"/> class with the specified HttpContext accessor.
    /// </summary>
    /// <param name="httpContextAccessor">The HttpContext accessor used to access the current HttpContext.</param>
    public HttpContextAccessorService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Gets the origin URL of the current HttpContext.
    /// </summary>
    /// <returns>The origin URL of the current HttpContext.</returns>
    public string GetOrigin()
    {
        return _httpContextAccessor.HttpContext.Request.Headers["origin"];
    }

    /// <summary>
    /// Gets the HttpRequest instance of the current HttpContext.
    /// </summary>
    /// <returns>The HttpRequest instance of the current HttpContext.</returns>
    public HttpRequest GetRequest()
    {
        return _httpContextAccessor.HttpContext.Request;
    }

    /// <summary>
    /// Gets the HttpResponse instance of the current HttpContext.
    /// </summary>
    /// <returns>The HttpResponse instance of the current HttpContext.</returns>
    public HttpResponse GetResponse()
    {
        return _httpContextAccessor.HttpContext.Response;
    }

    /// <summary>
    /// Gets the username instance of the current user.
    /// </summary>
    /// <returns>The username instance of the current user.</returns>
    public string GetUserName()
    {
        return _httpContextAccessor.HttpContext?.User.Identity.Name;
    }
}

