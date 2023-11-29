namespace App.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration settings for JWT (JSON Web Token).
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// Gets or sets the secret key used for JWT token generation and validation.
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    /// Gets or sets the expiration time in minutes for the access token.
    /// </summary>
    public int AccessTokenExpiration { get; set; }
}
