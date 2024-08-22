
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Models.Users;

/// <summary>
/// Class representing a many-to-many relationship between User and Role entities in the application. 
/// It inherits from the IdentityUserRole class provided by Microsoft.AspNetCore.Identity.
/// </summary>
public class ApplicationUserRole : IdentityUserRole<string>
{
    /// <summary>
    /// Gets or sets the user associated with the role.
    /// </summary>
    public virtual ApplicationUser ApplicationUser { get; set; }
    
    /// <summary>
    ///  Gets or sets the role associated with the user.
    /// </summary>
    public virtual ApplicationRole ApplicationRole { get; set; }
}
