namespace App.Application.AccountOperations.DTOs.Response;

/// <summary>
/// Data transfer object (DTO) used for user login response data.
/// </summary>
public class UserLoginResponseDto
{
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Gets or sets the JWT token associated with the user.
    /// </summary>
    public string Token { get; set; }
}

