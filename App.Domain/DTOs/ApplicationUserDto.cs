namespace App.Domain.DTOs;

/// <summary>
/// Data transfer object (DTO) for user details.
/// </summary>
public class ApplicationUserDto
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the user name of the user.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user's account is active or not.
    /// </summary>
    public bool IsAccountActive { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user's account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user's account was deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the comment of account was deletion.
    /// </summary>
    public string DeleteUserComment { get; set; }

    /// <summary>
    /// Gets or sets an comment associated with the guest user.
    /// </summary>
    public string GuestUserComment { get; set; }

    /// <summary>
    /// Gets or sets a PhoneNumber for a user.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets a Role for a user.
    /// </summary>
    public string Role { get; set; }
}
