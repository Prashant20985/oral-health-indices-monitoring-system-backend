using App.Application.AdminOperations.Command.ChangeActivationStatus;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.AdminOperations.Command.ChangeActivationStatus;

public class ChangeActivationStatusHandlerTests : TestHelper
{
    private readonly ApplicationUser applicationUser;
    private readonly ChangeActivationStatusCommand command;
    private readonly ChangeActivationStatusHandler handler;

    public ChangeActivationStatusHandlerTests()
    {
        applicationUser = new ApplicationUser(
            email: "test@example.com",
            firstName: "John",
            lastName: "Doe",
            phoneNumber: "12345678",
            guestUserComment: "xyz");

        command = new ChangeActivationStatusCommand(applicationUser.UserName);
        handler = new ChangeActivationStatusHandler(userRepositoryMock.Object);
    }

    [Fact]
    public async Task DeactivateAccount_Success_ReturnsSuccessResult()
    {
        // Arrange
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(applicationUser.UserName, CancellationToken.None))
           .ReturnsAsync(applicationUser);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.False(applicationUser.IsAccountActive);
    }

    [Fact]
    public async Task ActivateAccount_Success_ReturnsSuccessResult()
    {
        // Arrange
        applicationUser.ActivationStatusToggle();

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(applicationUser.UserName, CancellationToken.None))
           .ReturnsAsync(applicationUser);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.True(applicationUser.IsAccountActive);
    }

    [Fact]
    public async Task ChangeActivationStatus_Failure_ReturnsFailureResult()
    {
        // Arrange
        var invalidEmail = "test@test,com";
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(invalidEmail, CancellationToken.None))
           .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("User not found", result.ErrorMessage);
    }
}
