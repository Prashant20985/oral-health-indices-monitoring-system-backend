using App.Domain.Models.Users;

namespace App.Domain.Test.Models.Users;

public class ApplicationUserRoleTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var applicationUserRole = new ApplicationUserRole();

        // Assert
        Assert.Null(applicationUserRole.ApplicationUser);
        Assert.Null(applicationUserRole.ApplicationRole);
    }
}