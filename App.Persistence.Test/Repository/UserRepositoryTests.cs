using App.Domain.DTOs;
using App.Domain.Models.Users;
using App.Persistence.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;

namespace App.Persistence.Test.Repository;

public class UserRepositoryTests
{
    private readonly UserRepository userRepository;
    private readonly Mock<UserManager<ApplicationUser>> userManagerMock;
    private readonly ApplicationUser applicationUser;

    public UserRepositoryTests()
    {
        var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, ApplicationUserDto>());
        var mapper = mapperConfig.CreateMapper();
        userManagerMock = GetUserManagerMock();
        userRepository = new UserRepository(userManagerMock.Object, mapper);
        applicationUser = new("user@test.com", "Test", "User", "741852963", null);
    }

    [Fact]
    public async Task AddApplicationUserToRoleAsync_ValidData_ReturnsIdentityResultSuccess()
    {
        // Arrange
        var role = "UserRole";

        userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), role))
                       .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await userRepository.AddApplicationUserToRoleAsync(applicationUser, role);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task AddApplicationUserToRoleAsync_ReturnsIdentityResultFailure()
    {
        // Arrange
        var role = "UserRole";

        userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), role))
                       .ReturnsAsync(IdentityResult.Success);

        userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), role))
                           .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Role assignment failed" }));

        // Act
        var result = await userRepository.AddApplicationUserToRoleAsync(applicationUser, role);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Single(result.Errors);
        Assert.Equal("Role assignment failed", result.Errors.First().Description);
    }

    [Fact]
    public async Task ChangePassword_ValidData_ReturnsIdentityResultSuccess()
    {
        // Arrange
        var currentPassword = "CurrentPass";
        var newPassword = "NewPass";

        userManagerMock.Setup(x => x.ChangePasswordAsync(It.IsAny<ApplicationUser>(), currentPassword, newPassword))
                       .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await userRepository.ChangePassword(applicationUser, currentPassword, newPassword);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task ChangePassword_ReturnsIdentityResultFailure()
    {
        // Arrange
        var currentPassword = "CurrentPass";
        var newPassword = "NewPass";

        userManagerMock.Setup(x => x.ChangePasswordAsync(It.IsAny<ApplicationUser>(), currentPassword, newPassword))
                       .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to change password" }));

        // Act
        var result = await userRepository.ChangePassword(applicationUser, currentPassword, newPassword);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Single(result.Errors);
        Assert.Equal("Failed to change password", result.Errors.First().Description);
    }

    [Fact]
    public async Task CheckPassword_CorrectPassword_ReturnsTrue()
    {
        // Arrange
        var password = "CorrectPassword";

        userManagerMock.Setup(x => x.CheckPasswordAsync(applicationUser, password))
                       .ReturnsAsync(true);

        // Act
        var result = await userRepository.CheckPassword(applicationUser, password);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckPassword_IncorrectPassword_ReturnsFalse()
    {
        // Arrange
        var password = "IncorrectPassword";

        userManagerMock.Setup(x => x.CheckPasswordAsync(applicationUser, password))
                       .ReturnsAsync(false);

        // Act
        var result = await userRepository.CheckPassword(applicationUser, password);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task CreateApplicationUserAsync_Success_ReturnsSuccessResult()
    {
        // Arrange
        var password = "TestPassword";

        userManagerMock.Setup(x => x.CreateAsync(applicationUser, password))
                       .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await userRepository.CreateApplicationUserAsync(applicationUser, password);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task CreateApplicationUserAsync_Failure_ReturnsFailureResult()
    {
        // Arrange
        var password = "TestPassword";

        userManagerMock.Setup(x => x.CreateAsync(applicationUser, password))
                       .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "An error occurred." }));

        // Act
        var result = await userRepository.CreateApplicationUserAsync(applicationUser, password);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("An error occurred.", result.Errors.First().Description);
    }

    [Fact]
    public async Task GenerateResetPasswordToken_Success_ReturnsToken()
    {
        // Arrange
        var resetPasswordToken = "TestToken";

        userManagerMock.Setup(x => x.GeneratePasswordResetTokenAsync(applicationUser))
                       .ReturnsAsync(resetPasswordToken);

        // Act
        var result = await userRepository.GenerateResetPasswordToken(applicationUser);

        // Assert
        Assert.Equal(resetPasswordToken, result);
    }

    [Fact]
    public void GetActiveApplicationUsersQuery_Success_ReturnsQueryable()
    {
        // Arrange
        ApplicationUser deactivatedUser1 = new("user@test.com", "Test", "User", "741852963", null);
        ApplicationUser deactivatedUser2 = new("user2@test.com", "Test2", "User2", "741852964", null);

        var userQueryable = new[] { deactivatedUser1, deactivatedUser2 }.AsQueryable();

        userManagerMock.Setup(x => x.Users).Returns(userQueryable);

        // Act
        var result = userRepository.GetActiveApplicationUsersQuery();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetDeactivatedApplicationUsersQuery_Success_ReturnsQueryable()
    {
        // Arrange
        ApplicationUser deactivatedUser1 = new("user@test.com", "Test", "User", "741852963", null);
        ApplicationUser deactivatedUser2 = new("user2@test.com", "Test2", "User2", "741852964", null);

        deactivatedUser1.ActivationStatusToggle();
        deactivatedUser2.ActivationStatusToggle();

        var userQueryable = new[] { deactivatedUser1, deactivatedUser2 }.AsQueryable();

        userManagerMock.Setup(x => x.Users).Returns(userQueryable);

        // Act
        var result = userRepository.GetDeactivatedApplicationUsersQuery();

        // Assert

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetDeletedApplicationUsersQuery_Success_ReturnsQueryable()
    {
        // Arrange
        ApplicationUser deletedUser1 = new("user@test.com", "Test", "User", "741852963", null);
        ApplicationUser deletedUser2 = new("user2@test.com", "Test2", "User2", "741852964", null);

        deletedUser1.DeleteUser("Test Delete1");
        deletedUser2.DeleteUser("Test Delete2");

        var userQueryable = new[] { deletedUser1, deletedUser2 }.AsQueryable();

        userManagerMock.Setup(x => x.Users).Returns(userQueryable);

        // Act
        var result = userRepository.GetDeletedApplicationUsersQuery();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetUserByUserNameOrEmail_Success_ReturnsApplicationUser()
    {
        // Arrange 
        var role = new ApplicationRole { Name = "Admin" };
        applicationUser.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = role });

        var applicationUsers = new List<ApplicationUser>() { applicationUser };
        var mockUserQueryable = applicationUsers.AsQueryable().BuildMock();

        userManagerMock.Setup(um => um.Users).Returns(mockUserQueryable);

        // Act 
        var result = await userRepository.GetUserByUserNameOrEmail(applicationUser.Email, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(applicationUser.FirstName, result.FirstName);
        Assert.Equal("Admin", result.ApplicationUserRoles.Select(x => x.ApplicationRole.Name).FirstOrDefault());
    }

    [Fact]
    public async Task GetUserByUserNameWithRefreshToken_Success_ReturnsApplicationUserWithRefreshToken()
    {
        // Arrange 
        var refreshToken = new RefreshToken { Token = "token" };
        var role = new ApplicationRole { Name = "Admin" };

        applicationUser.ApplicationUserRoles.Add(new ApplicationUserRole { ApplicationRole = role });
        applicationUser.RefreshTokens.Add(refreshToken);

        var applicationUsers = new List<ApplicationUser>() { applicationUser };
        var mockUserQueryable = applicationUsers.AsQueryable().BuildMock();

        userManagerMock.Setup(um => um.Users).Returns(mockUserQueryable);

        // Act 
        var result = await userRepository.GetUserByUserNameWithRefreshToken("user", CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(applicationUser.FirstName, result.FirstName);
        Assert.Equal("Admin", result.ApplicationUserRoles.Select(x => x.ApplicationRole.Name).FirstOrDefault());
        Assert.Equal("token", result.RefreshTokens.Select(x => x.Token).FirstOrDefault());
    }

    [Fact]
    public async Task RemoveApplicationUserFromRolesAsync_Success_ReturnsSuccessResult()
    {
        // Arrange
        List<string> roles = new() { "Admin", "Student" };

        userManagerMock.Setup(um => um.RemoveFromRolesAsync(applicationUser, roles))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await userRepository.RemoveApplicationUserFromRolesAsync(applicationUser, roles);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task RemoveApplicationUserFromRolesAsync_Failure_ReturnsFailureResult()
    {
        // Arrange
        List<string> roles = new() { "Admin", "Student" };

        userManagerMock.Setup(um => um.RemoveFromRolesAsync(applicationUser, roles))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error removing user from roles" }));

        // Act
        var result = await userRepository.RemoveApplicationUserFromRolesAsync(applicationUser, roles);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Error removing user from roles", result.Errors.First().Description);
    }

    [Fact]
    public async Task ResetPassword_Success_ReturnsSuccessResult()
    {
        // Arrange
        var password = "password";
        var token = "token";

        userManagerMock.Setup(um => um.ResetPasswordAsync(applicationUser, token, password))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await userRepository.ResetPassword(applicationUser, token, password);

        // Assert
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task ResetPassword_Failure_ReturnsFailureResult()
    {
        // Arrange
        var password = "password";
        var token = "token";

        userManagerMock.Setup(um => um.ResetPasswordAsync(applicationUser, token, password))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error reseting password" }));

        // Act
        var result = await userRepository.ResetPassword(applicationUser, token, password);

        // Assert
        Assert.False(result.Succeeded);
        Assert.Equal("Error reseting password", result.Errors.First().Description);
    }

    private static Mock<UserManager<ApplicationUser>> GetUserManagerMock()
    {
        var storeMock = new Mock<IUserStore<ApplicationUser>>();
        Mock<UserManager<ApplicationUser>> userManagerMock = new(storeMock.Object, null, null, null, null, null, null, null, null);

        return userManagerMock;
    }
}
