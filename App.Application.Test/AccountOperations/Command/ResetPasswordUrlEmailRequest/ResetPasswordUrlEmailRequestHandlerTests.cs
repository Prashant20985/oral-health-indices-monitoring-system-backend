using App.Application.AccountOperations.Command.ResetPasswordUrlEmailRequest;
using App.Application.NotificationOperations;
using App.Domain.Models.Users;
using MediatR;
using Moq;

namespace App.Application.Test.AccountOperations.Command.ResetPasswordUrlEmailRequest;
public class ResetPasswordUrlEmailRequestHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResult()
    {
        // Arrange
        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz");

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(user.Email, CancellationToken.None))
                .ReturnsAsync(user);

        userRepositoryMock.Setup(u => u.GenerateResetPasswordToken(user))
            .ReturnsAsync("reset_token");

        var resetPasswordUrlEmailRequestCommand = new ResetPasswordUrlEmailRequestCommand(user.Email);

        var resetPasswordUrlEmailRequestHandler = new ResetPasswordUrlEmailRequestHandler(
            httpContextAccessorServiceMock.Object,
            mediatorMock.Object,
            userRepositoryMock.Object);

        // Act
        var result = await resetPasswordUrlEmailRequestHandler.Handle(resetPasswordUrlEmailRequestCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);

        // Verify that the email notification was published
        mediatorMock.Verify(m => m.Publish(It.IsAny<EmailNotification>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidUser_ReturnsFailureResult()
    {
        // Arrange
        var invalidEmail = "invaliduser@example.com";

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(invalidEmail, CancellationToken.None))
            .ReturnsAsync(value: null);

        var resetPasswordUrlEmailRequestCommand = new ResetPasswordUrlEmailRequestCommand(invalidEmail);

        var resetPasswordUrlEmailRequestHandler = new ResetPasswordUrlEmailRequestHandler(
            httpContextAccessorServiceMock.Object,
            mediatorMock.Object,
            userRepositoryMock.Object);

        // Act
        var result = await resetPasswordUrlEmailRequestHandler.Handle(resetPasswordUrlEmailRequestCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Email or Username", result.ErrorMessage);

        // Verify that the email was not sent
        mediatorMock.Verify(m => m.Publish(It.IsAny<EmailNotification>(), CancellationToken.None), Times.Never);
    }
}
