﻿using App.Application.Interfaces;
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
}
