using App.Domain.Models.Users;

namespace App.Application.Interfaces;

/// <summary>
/// Service for creating and managing JWT tokens.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Creates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is created.</param>
    /// <returns>A task representing the asynchronous operation that yields the JWT token as a string.</returns>
    Task<string> CreateToken(ApplicationUser user);

    /// <summary>
    /// Sets a refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the refresh token is set.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetRefreshToken(ApplicationUser user);
}
