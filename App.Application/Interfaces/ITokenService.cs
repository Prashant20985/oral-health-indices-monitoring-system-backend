using App.Domain.Models.Users;

namespace App.Application.Interfaces;

/// <summary>
/// Service for creating and managing JWT tokens.
/// </summary>
public interface ITokenService
{
    Task<string> CreateToken(User user);
}
