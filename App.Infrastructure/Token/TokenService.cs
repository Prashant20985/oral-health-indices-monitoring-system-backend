using App.Application.Interfaces;
using App.Domain.Models.Users;
using App.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Infrastructure.Token;

public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </summary>
    /// <param name="optionsMonitor">The monitor for JWT configuration options.</param>
    /// <param name="roleManager">The role manager.</param>
    /// <param name="userManager">The user manager.</param>
    public TokenService(IOptionsMonitor<JwtConfig> optionsMonitor,
        RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    /// <summary>
    /// Creates a JWT token for the specified user.
    /// </summary>
    /// <param name="appUser">The user for whom to create the token.</param>
    /// <returns>The generated JWT token.</returns>
    public virtual async Task<string> CreateToken(User appUser)
    {
        // Get all claims for the user
        var claims = await GetAllUserClaims(appUser);

        // Create the security key and signing credentials
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

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
    private async Task<List<Claim>> GetAllUserClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var userRole in userRoles)
        {
            var role = await _roleManager.FindByNameAsync(userRole);

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
        }

        return claims;
    }
}
