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
            List<ApplicationUser> appUsers = new List<ApplicationUser>
            {
                // Create an instance of Administrator user.
                new ApplicationUser
                {
                    FirstName = "Clark",
                    LastName = "Kent",
                    Email = "superman@test.com",
                    UserName = "superman",
                    CreatedAt = DateTime.Now.AddDays(1),
                    IsAccountActive = true,
                    DeletedAt = null,
                },
                // Create an instance of Administrator user.
                new ApplicationUser
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    Email = "batman@test.com",
                    CreatedAt = DateTime.Now.AddDays(5),
                    UserName = "batman",
                    IsAccountActive = true,
                    DeletedAt = null,
                },
                // Create an instance of Student user.
                new ApplicationUser
                {
                    FirstName = "Hal",
                    LastName = "Jordan",
                    Email = "lantern@test.com",
                    CreatedAt = DateTime.Now.AddDays(5),
                    UserName = "lantern",
                    IsAccountActive = true,
                    DeletedAt = null,
                },
                // Create an instance of Student user.
                new ApplicationUser
                {
                    FirstName = "Barry",
                    LastName = "Allen",
                    Email = "flash@test.com",
                    CreatedAt = DateTime.Now.AddDays(7),
                    UserName = "flash",
                    IsAccountActive = true,
                    DeletedAt = null,
                },
                // Create an instance of Dentist_Teacher_Examiner user.
                new ApplicationUser
                {
                    FirstName = "Victor",
                    LastName = "Stone",
                    Email = "cyborg@test.com",
                    UserName = "cyborg",
                    CreatedAt = DateTime.Now.AddDays(3),
                    IsAccountActive = false,
                    DeletedAt = null,
                },
                // Create an instance of Dentist_Teacher_Examiner user.
                new ApplicationUser
                {
                    FirstName = "Arthur",
                    LastName = "Curry",
                    Email = "aquaman@test.com",
                    UserName = "aquaman",
                    CreatedAt = DateTime.Now.AddDays(1),
                    IsAccountActive = true,
                    DeletedAt = null,
                },
                // Create an instance of Dentist_Teacher_Researcher user.
                new ApplicationUser
                {
                    FirstName = "Oliver",
                    LastName = "Queen",
                    Email = "greenarrow@test.com",
                    UserName = "greenarrow",
                    CreatedAt = DateTime.Now.AddDays(7),
                    IsAccountActive = false,
                    DeletedAt = null,
                },
                // Create an instance of Dentist_Teacher_Researcher user.
                new ApplicationUser
                {
                    FirstName = "Kara",
                    LastName = "Denvers",
                    Email = "supergirl@test.com",
                    UserName = "supergirl",
                    IsAccountActive = false,
                    CreatedAt = DateTime.Now.AddDays(4),
                    DeletedAt = DateTime.Now,
                    GuestUserComment = "Guest Examiner",
                },
                new ApplicationUser
                {
                    FirstName = "Jhon",
                    LastName = "wick",
                    Email = "wick@test.com",
                    UserName = "wick",
                    IsAccountActive = true,
                    DeletedAt = DateTime.Now.AddDays(3),
                }
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
