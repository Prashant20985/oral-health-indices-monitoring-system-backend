namespace App.Application.AccountOperations.DTOs.Request;

/// <summary>
/// Data transfer object (DTO) used for user login credentials.
/// </summary>
public class UserCredentialsDto
{
    /// <summary>
    /// Gets or sets the email of the user for login.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user for login.
    /// </summary>
    public string Password { get; set; }
}
