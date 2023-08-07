
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Models.Users;

/// <summary>
/// Class representing a many-to-many relationship between User and Role entities in the application. 
/// It inherits from the IdentityUserRole class provided by Microsoft.AspNetCore.Identity.
/// </summary>
public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser ApplicationUser { get; set; }
    public virtual ApplicationRole ApplicationRole { get; set; }
}
