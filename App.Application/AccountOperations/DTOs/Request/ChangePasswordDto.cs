namespace App.Application.AccountOperations.DTOs.Request;

/// <summary>
/// Class representing a data transfer object (DTO) used for changing a user's password.
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Gets or sets the email of the user requesting a password change.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the current password of the user.
    /// </summary>
    public string CurrentPassword { get; set; }

    /// <summary>
    /// Gets or sets the new password that the user wants to set.
    /// </summary>
    public string NewPassword { get; set; }
}
