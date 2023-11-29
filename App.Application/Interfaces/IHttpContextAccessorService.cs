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
    /// Gets the cookie for Refresh Token
    /// </summary>
    /// <returns>The refresh token string of the current HttpContext.</returns>
    string GetRefreshTokenCookie();

    /// <summary>
    /// Gets the username instance of the current user.
    /// </summary>
    /// <returns>The username instance of the current user.</returns>
    string GetUserName();
}
