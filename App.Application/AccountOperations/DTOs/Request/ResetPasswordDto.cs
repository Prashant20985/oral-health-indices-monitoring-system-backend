namespace App.Application.AccountOperations.DTOs.Request;

/// <summary>
/// Class representing a data transfer object (DTO) used for resetting a user's password.
/// </summary>
public class ResetPasswordDto
{
    /// <summary>
    /// Gets or sets the new password for the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the confirmation password for the user.
    /// It should match the value of the "Password" property.
    /// </summary>
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Gets or sets the email of the user requesting the password reset.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the token associated with the password reset request.
    /// </summary>
    public string Token { get; set; }
}