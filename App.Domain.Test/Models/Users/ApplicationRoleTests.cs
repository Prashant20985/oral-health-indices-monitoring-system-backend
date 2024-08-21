using App.Domain.Models.Users;
namespace App.Domain.Test.Models.Users;

public class ApplicationRoleTests
{
    [Fact]
    public void Constructor_ShouldInitializeApplicationUserRoles()
    {
        // Arrange & Act
        var applicationRole = new ApplicationRole();

        // Assert
        Assert.NotNull(applicationRole.ApplicationUserRoles);
        Assert.Empty(applicationRole.ApplicationUserRoles);
    }
}
