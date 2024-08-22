using App.Application.Interfaces;
using App.Domain.Models.Users;
using App.Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace App.Infrastructure.Token;

/// <summary>
/// Service for creating and managing JWT tokens.
/// </summary>
public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessorService _httpContextAccessorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </summary>
    /// <param name="optionsMonitor">The monitor for JWT configuration options.</param>
    /// <param name="roleManager">The role manager.</param>
    /// <param name="userManager">The user manager.</param>
    /// <param name="httpContextAccessorService">The service providing access to the current HttpContext.</param>
    public TokenService(IOptionsMonitor<JwtConfig> optionsMonitor,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessorService httpContextAccessorService)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _jwtConfig = optionsMonitor.CurrentValue;
        _httpContextAccessorService = httpContextAccessorService;
    }

    /// <summary>
    /// Creates a JWT token for the specified user.
    /// </summary>
    /// <param name="appUser">The user for whom to create the token.</param>
    /// <returns>The generated JWT token.</returns>
    public virtual async Task<string> CreateToken(ApplicationUser appUser)
    {
        // Get all claims for the user
        var claims = await GetAllUserClaims(appUser);

        // Create the security key and signing credentials
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpiration),
            SigningCredentials = creds
        };

        // Create a new JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }

    /// <summary>
    /// Gets all claims for the specified user, including email, name identifier, and roles.
    /// </summary>
    /// <param name="userhe user for whom to retrieve the claims.</param>
    /// <returns>A list of claims for the user.</returns>
    internal async Task<List<Claim>> GetAllUserClaims(ApplicationUser user)
    {
        // Create a list of claims for the user
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Get the roles for the user
        var userRoles = await _userManager.GetRolesAsync(user);

        // Add the roles to the claims
        foreach (var userRole in userRoles)
        {
            var role = await _roleManager.FindByNameAsync(userRole);

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
        }

        // Return the claims
        return claims;
    }


    /// <summary>
    /// Sets a refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the refresh token is set.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task SetRefreshToken(ApplicationUser user)
    {
        // Generate a new refresh token
        var refreshToken = GenerateRefreshToken();

        // Add the refresh token to the user
        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        // Set the refresh token as a cookie
        var httpResponse = _httpContextAccessorService.GetResponse();
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
        };

        httpResponse.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }


    /// <summary>
    /// Generates a new refresh token.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    private RefreshToken GenerateRefreshToken()
    {
        // Generate a random number for the refresh token
        var randomNumber = new byte[32];
        
        // Use a random number generator to create the token
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        // Return the refresh token
        return new RefreshToken { Token = Convert.ToBase64String(randomNumber) };
    }
}
