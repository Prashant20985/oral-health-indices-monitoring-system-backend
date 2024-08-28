using App.Application.AdminOperations.Command.DeleteApplicationUser;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.AdminOperations.Command.DeleteApplicationUser;

public class DeleteApplicationUserHandlerTests : TestHelper
{
    private readonly ApplicationUser applicationUser;
    private readonly DeleteApplicationUserCommand command;
    private readonly DeleteApplicationUserHandler handler;

    public DeleteApplicationUserHandlerTests()
    {
        applicationUser = new ApplicationUser(
            email: "test@example.com",
            firstName: "John",
            lastName: "Doe",
            phoneNumber: "12345678",
            guestUserComment: "xyz");

        command = new DeleteApplicationUserCommand(applicationUser.UserName, "Test Delete");
        handler = new DeleteApplicationUserHandler(userRepositoryMock.Object);
    }

    [Fact]
    public async Task DeleteApplicationUser_Success_ReturnsSuccessResult()
    {
        // Arrange
        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(applicationUser.UserName, CancellationToken.None))
           .ReturnsAsync(applicationUser);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Test Delete", applicationUser.DeleteUserComment);
        Assert.NotNull(applicationUser.DeletedAt);
    }

    [Fact]
    public async Task DeleteApplicationUser_Failure_ReturnsFailureResult()
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
}
