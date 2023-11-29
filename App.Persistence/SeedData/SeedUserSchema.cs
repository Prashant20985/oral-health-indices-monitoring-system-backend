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
    public static async Task SeedData(UserContext userContext,
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
            List<ApplicationUser> appUsers = new List<ApplicationUser>
            {
                new ApplicationUser("superman@test.com", "Clark", "Kent", null, null),
                new ApplicationUser("batman@test.com", "Bruce", "Wayne", null, null),
                new ApplicationUser("lantern@test.com", "Hal", "Jordan", null, null),
                new ApplicationUser("flash@test.com", "Barry", "Allen", null, null),
                new ApplicationUser("cyborg@test.com", "Victor", "Stone", null, null),
                new ApplicationUser("aquaman@test.com", "Arthur", "Kent", null, null),
                new ApplicationUser("greenarrow@test.com", "Oliver", "Queen", null, null),
                new ApplicationUser("supergirl@test.com", "Kara", "Denvers", null, "Guest Examiner"),
                new ApplicationUser("wick@test.com", "Jhon", "wick", null, null),
            };

            // Iterate over the appUsers list and create each user using the user manager.
            foreach (var appUser in appUsers)
            {
                await userManager.CreateAsync(appUser, "P@ssw0rd");

                if (appUser.UserName == "superman")
                {
                    await userManager.AddToRoleAsync(appUser, Role.Admin.ToString());
                }
                else if (appUser.UserName == "lantern" || appUser.UserName == "flash")
                {
                    await userManager.AddToRoleAsync(appUser, Role.Student.ToString());
                }
                else if (appUser.UserName == "cyborg" || appUser.UserName == "aquaman")
                {
                    await userManager.AddToRoleAsync(appUser, Role.Dentist_Teacher_Researcher.ToString());
                }
                else if (appUser.UserName == "greenarrow" || appUser.UserName == "supergirl")
                {
                    await userManager.AddToRoleAsync(appUser, Role.Dentist_Teacher_Examiner.ToString());
                }
            }
        }
    }
}
