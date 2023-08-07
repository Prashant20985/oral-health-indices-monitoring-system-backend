using App.Application.AccountOperations.Query.CurrentUser;
using App.Application.Interfaces;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.AccountOperations.Query.CurrentUser;
public class CurrentUserHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidUserName_ReturnsSuccessResultWithUserLoginResponseDto()
    {
        // Arrange
        var userName = "johndoe";

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz");
        user.UserName = userName;

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(userName, CancellationToken.None))
            .ReturnsAsync(user);

        var tokenServiceMock = new Mock<ITokenService>();
        tokenServiceMock.Setup(t => t.CreateToken(user)).ReturnsAsync("token");

        var query = new CurrentUserQuery(userName);
        var handler = new CurrentUserHandler(userRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("John Doe", result.ResultValue.Name);
        Assert.Equal("test@example.com", result.ResultValue.Email);
        Assert.Equal("johndoe", result.ResultValue.UserName);
        Assert.Equal("token", result.ResultValue.Token);
    }

    [Fact]
    public async Task Handle_InvalidUserName_ReturnsFailureResultWithErrorMessage()
    {
        // Arrange
        var userName = "unknown_user";

        userRepositoryMock.Setup(u => u.GetUserByUserNameOrEmail(userName, CancellationToken.None))
            .ReturnsAsync(value: null);

        var query = new CurrentUserQuery(userName);
        var handler = new CurrentUserHandler(userRepositoryMock.Object, tokenServiceMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Invalid Email or Username", result.ErrorMessage);
        Assert.Null(result.ResultValue);
    }
}
