using Microsoft.AspNetCore.Identity;

namespace App.Domain.Models.Users;

/// <summary>
/// Class representing a Role in the application. It inherits from the IdentityRole class provided by Microsoft.AspNetCore.Identity.
/// </summary>
public class ApplicationRole : IdentityRole
{
    public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = new List<ApplicationUserRole>();
}
