namespace App.Domain.DTOs.ApplicationUserDtos.Request;

/// <summary>
/// Data transfer object (DTO) for updating a user.
/// </summary>
public class UpdateApplicationUserRequestDto
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
    /// Gets or sets the phone number of the user.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the comment for the user.
    /// Default value is null.
    /// </summary>
    public string GuestUserComment { get; set; }

    /// <summary>
    /// Gets or sets a Role for a user.
    /// </summary>
    public string Role { get; set; }
}