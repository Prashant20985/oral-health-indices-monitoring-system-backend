using Microsoft.AspNetCore.Identity;

namespace App.Domain.Models.Users;

/// <summary>
/// Class representing a user in the application. It inherits from the IdentityUser class provided by Microsoft.AspNetCore.Identity.
/// </summary>
public class User : IdentityUser
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
    /// Gets or sets a value indicating whether the user's account is active or not.
    /// </summary>
    public bool IsAccountActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the date and time when the user's account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user's account was deleted.
    /// This property is nullable, meaning it can have a null value.
    /// </summary>
    public DateTime? DeletedAt { get; set; } = null;

    /// <summary>
    /// Gets or sets the comment of account was deletion.
    /// </summary>
    public string DeleteUserComment { get; set; } = null;

    /// <summary>
    /// Gets or sets an comment associated with the guest user.
    /// The initial value of this property is set to null.
    /// </summary>
    public string GuestUserComment { get; set; } = null;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}

