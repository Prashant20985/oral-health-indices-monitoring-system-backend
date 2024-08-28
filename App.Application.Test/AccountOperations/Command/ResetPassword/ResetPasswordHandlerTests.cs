using App.Application.AccountOperations.Command.ResetPassword;
using App.Application.AccountOperations.DTOs.Request;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace App.Application.Test.AccountOperations.Command.ResetPassword;
public class ResetPasswordHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResult()
    {
        // Arrange
        var validResetPassowrdToken = "valid_token";

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz");

        var resetPasswordCommand = new ResetPasswordCommand(new ResetPasswordDto
        {
            Email = user.Email,
            Token = validResetPassowrdToken,
            Password = "newpassword123"
        });

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(user.Email, CancellationToken.None))
                .ReturnsAsync(user);

        userRepositoryMock.Setup(u => u.ResetPassword(user, resetPasswordCommand.ResetPassword.Token, resetPasswordCommand.ResetPassword.Password))
            .ReturnsAsync(IdentityResult.Success);

        var resetPasswordHandler = new ResetPasswordHandler(mediatorMock.Object, userRepositoryMock.Object);

        // Act
        var result = await resetPasswordHandler.Handle(resetPasswordCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
    }

    [Fact]
    public async Task Handle_InvalidUser_ReturnsFailureResult()
    {
        // Arrange
        var invalidEmail = "invaliduser@example.com";

        var resetPasswordCommand = new ResetPasswordCommand(new ResetPasswordDto
        {
            Email = invalidEmail,
            Token = "invalid_reset_token",
            Password = "newpassword123"
        });

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(invalidEmail, CancellationToken.None))
            .ReturnsAsync(value: null);

        var resetPasswordHandler = new ResetPasswordHandler(mediatorMock.Object, userRepositoryMock.Object);

        // Act
        var result = await resetPasswordHandler.Handle(resetPasswordCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Email or Username", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_PasswordResetFailure_ReturnsFailureResult()
    {
        // Arrange
        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz")
        {
            RefreshTokens = new List<Domain.Models.Users.RefreshToken>()
        };

        var resetPasswordCommand = new ResetPasswordCommand(new ResetPasswordDto
        {
            Email = user.Email,
            Token = "reset_token",
            Password = "newpassword123"
        });

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(user.Email, CancellationToken.None))
            .ReturnsAsync(user);

        userRepositoryMock.Setup(u => u.ResetPassword(user, resetPasswordCommand.ResetPassword.Token, resetPasswordCommand.ResetPassword.Password))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password reset failed." }));

        var resetPasswordHandler = new ResetPasswordHandler(mediatorMock.Object, userRepositoryMock.Object);

        // Act
        var result = await resetPasswordHandler.Handle(resetPasswordCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Password reset failed.", result.ErrorMessage);
    }
}
