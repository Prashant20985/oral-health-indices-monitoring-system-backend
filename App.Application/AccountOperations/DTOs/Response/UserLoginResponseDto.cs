namespace App.Application.AccountOperations.DTOs.Response;

/// <summary>
/// Data transfer object (DTO) used for user login response data.
/// </summary>
public class UserLoginResponseDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserLoginResponseDto"/> class.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <param name="userName">The username of the user.</param>
    /// <param name="email">The email of the user.</param>
    /// <param name="role">The role of the user.</param>
    /// <param name="token">The JWT token associated with the user.</param>
    public UserLoginResponseDto(string name, string userName, string email, string role, string token)
    {
        Name = name;
        UserName = userName;
        Email = email;
        Role = role;
        Token = token;
    }

    /// <summary>
    /// Gets the name of the user.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the username of the user.
    /// </summary>
    public string UserName { get; private set; }

    /// <summary>
    /// Gets the email of the user.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the role of the user.
    /// </summary>
    public string Role { get; private set; }

    /// <summary>
    /// Gets the JWT token associated with the user.
    /// </summary>
    public string Token { get; private set; }
}