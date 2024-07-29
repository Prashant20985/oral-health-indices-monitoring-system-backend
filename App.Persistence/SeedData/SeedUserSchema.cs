using App.Domain.Models.Enums;
using App.Domain.Models.Users;
using App.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;

namespace App.Persistence.SeedData;

public class SeedUserSchema
{
    /// <summary>
    /// This method seeds data to the user schema in the database.
    /// </summary>
    /// <param name="userContext">An instance of the UserContext class for accessing the user database context.</param>
    /// <param name="userManager">An instance of the UserManager<AppUser> class for managing user-related operations.</param>
    /// <param name="roleManager">An instance of the RoleManager<IdentityRole> class for managing role-related operations.</param>
    public static async Task SeedData(OralEhrContext userContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        // Check if roles exist in the role manager. If not, create the roles.
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new ApplicationRole { Name = Role.Admin.ToString() });
            await roleManager.CreateAsync(new ApplicationRole { Name = Role.Dentist_Teacher_Examiner.ToString() });
            await roleManager.CreateAsync(new ApplicationRole { Name = Role.Dentist_Teacher_Researcher.ToString() });
            await roleManager.CreateAsync(new ApplicationRole { Name = Role.Student.ToString() });
        }

        // Check if any users exist in the user manager. If not, create the users.
        if (!userManager.Users.Any())
        {
            // Create a list of AppUser objects representing different users.
            // Create a list of AppUser objects representing different users.
            ApplicationUser appUser = new("superman@test.com", "Clark", "Kent", null, null);

            // Iterate over the appUsers list and create each user using the user manager.
            await userManager.CreateAsync(appUser, "P@ssw0rd");
            await userManager.AddToRoleAsync(appUser, Role.Admin.ToString());
        }
    }
}
