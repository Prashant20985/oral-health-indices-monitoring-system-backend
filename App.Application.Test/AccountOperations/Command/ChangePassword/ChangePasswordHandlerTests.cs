using App.Application.AccountOperations.Command.ChangePassword;
using App.Application.AccountOperations.DTOs.Request;
using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace App.Application.Test.AccountOperations.Command.ChangePassword;
public class ChangePasswordHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ChangePassword_ReturnsSuccessResult()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto
        {
            Email = "test@example.com",
            CurrentPassword = "oldpassword",
            NewPassword = "newpassword"
        };

        var applicationUser = new ApplicationUser("test@example.com",
        "John",
        "Doe",
        "12345678",
        "xyz");

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(changePasswordDto.Email, CancellationToken.None))
                .ReturnsAsync(applicationUser);

        userRepositoryMock.Setup(u => u.ChangePassword(applicationUser, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword))
            .ReturnsAsync(IdentityResult.Success);

        var changePasswordCommand = new ChangePasswordCommand(changePasswordDto);
        var changePasswordHandler = new ChangePasswordHandler(userRepositoryMock.Object);

        // Act
        var result = await changePasswordHandler.Handle(changePasswordCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result);
    }
    [Fact]
    public async Task Handle_InavlidUser_FailureResult()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto
        {
            Email = "test@example.com",
            CurrentPassword = "oldpassword",
            NewPassword = "newpassword"
        };

        ApplicationUser? nullUser = null;

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(changePasswordDto.Email, CancellationToken.None))
            .ReturnsAsync(nullUser);

        var command = new ChangePasswordCommand(changePasswordDto);
        var handler = new ChangePasswordHandler(userRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Email or Username", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_SamePassword_FailureResultt()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto
        {
            Email = "test@example.com",
            CurrentPassword = "oldpassword",
            NewPassword = "oldpassword" // New password same as current password.
        };

        var applicationUser = new ApplicationUser("test@example.com",
            "John",
            "Doe",
            "12345678",
            "xyz");

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(changePasswordDto.Email, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        var changePasswordCommand = new ChangePasswordCommand(changePasswordDto);
        var changePasswordHandler = new ChangePasswordHandler(userRepositoryMock.Object);

        // Act
        var result = await changePasswordHandler.Handle(changePasswordCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("New password cannot be the same as the old password", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_ChangePasswordFailure_ReturnsFailureResultWithErrorMessage()
    {
        // Arrange
        var changePasswordDto = new ChangePasswordDto
        {
            Email = "test@example.com",
            CurrentPassword = "oldpassword",
            NewPassword = "newPassword"
        };

        var applicationUser = new ApplicationUser("test@example.com",
            "John",
            "Doe",
            "12345678",
            "xyz");

        var error = new IdentityError { Description = "Change password failed" };

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(changePasswordDto.Email, CancellationToken.None))
            .ReturnsAsync(applicationUser);

        userRepositoryMock.Setup(u => u.ChangePassword(applicationUser, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword))
            .ReturnsAsync(IdentityResult.Failed(error));

        var changePasswordCommand = new ChangePasswordCommand(changePasswordDto);
        var changePasswordHandler = new ChangePasswordHandler(userRepositoryMock.Object);

        // Act
        var result = await changePasswordHandler.Handle(changePasswordCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Change password failed", result.ErrorMessage);
    }
}
