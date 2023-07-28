﻿namespace App.Application.Interfaces;

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
}
