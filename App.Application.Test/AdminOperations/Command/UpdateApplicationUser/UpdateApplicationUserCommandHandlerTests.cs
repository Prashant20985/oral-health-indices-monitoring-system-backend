using App.Application.AdminOperations.Command.UpdateApplicationUser;
using App.Domain.DTOs.ApplicationUserDtos.Request;
using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace App.Application.Test.AdminOperations.Command.UpdateApplicationUser;

public class UpdateApplicationUserCommandHandlerTests : TestHelper
{
    private readonly UpdateApplicationUserRequestDto updateApplicationUserDto;
    private readonly UpdateApplicationUserCommand command;
    private readonly UpdateApplicationUserCommandHandler handler;
    private readonly ApplicationUser applicationUser;
    public UpdateApplicationUserCommandHandlerTests()
    {
        updateApplicationUserDto = new UpdateApplicationUserRequestDto()
        {
            FirstName = "Test",
            LastName = "User",
            GuestUserComment = null,
            PhoneNumber = "1234567890"
        };

        applicationUser = new ApplicationUser(
            email: "test@example.com",
            firstName: "John",
            lastName: "Doe",
            phoneNumber: "12345678",
            guestUserComment: "xyz");

        command = new UpdateApplicationUserCommand(applicationUser.UserName, updateApplicationUserDto);
        handler = new UpdateApplicationUserCommandHandler(userRepositoryMock.Object);
    }

    [Fact]
    public async Task UpdateApplicationUser_Success_ReturnsSuccessResult()
    {
        // Arrange
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(applicationUser.UserName, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        var userRoles = applicationUser.ApplicationUserRoles
            .Select(x => x.ApplicationRole.Name)
            .ToList();

        userRepositoryMock.Setup(u => u.RemoveApplicationUserFromRolesAsync(applicationUser, userRoles))
            .ReturnsAsync(IdentityResult.Success);

        userRepositoryMock.Setup(u => u.AddApplicationUserToRoleAsync(applicationUser, updateApplicationUserDto.Role))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Test", applicationUser.FirstName);
        Assert.Equal("User", applicationUser.LastName);
        Assert.Null(applicationUser.GuestUserComment);
        Assert.Equal("1234567890", applicationUser.PhoneNumber);
    }

    [Fact]
    public async Task UpdateApplicationUser_Failure_UserNotFound_ReturnsFailureResult()
    {
        // Arrange
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(applicationUser.UserName, CancellationToken.None))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("User not found", result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateApplicationUser_Failure_RemoveFromRole_ReturnsFailureResult()
    {
        // Arrange
        var applicationUserRole = new ApplicationUserRole { ApplicationRole = new ApplicationRole { Name = "Admin" } };
        applicationUser.ApplicationUserRoles.Add(applicationUserRole);

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(applicationUser.UserName, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        var userRoles = applicationUser.ApplicationUserRoles
            .Select(x => x.ApplicationRole.Name)
            .ToList();

        userRepositoryMock.Setup(u => u.RemoveApplicationUserFromRolesAsync(applicationUser, userRoles))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Failed to remove existing role" }));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Failed to remove existing role", result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateApplicationUser_Failure_DeletedUser_ReturnsFailureResult()
    {
        // Arrange
        applicationUser.DeleteUser("Test Delete");
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(applicationUser.UserName, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Deleted user", result.ErrorMessage);
    }
}
