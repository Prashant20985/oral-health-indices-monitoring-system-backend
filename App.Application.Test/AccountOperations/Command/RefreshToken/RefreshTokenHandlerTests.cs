using App.Application.AccountOperations.Command.RefreshToken;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.AccountOperations.Command.RefreshToken;
public class RefreshTokenHandlerTests : TestHelper
{

    [Fact]
    public async Task Handle_ValidRequest_ReturnsSuccessResultWithUserLoginResponseDto()
    {
        // Arrange
        var userName = "johndoe";
        var refreshToken = "valid_refresh_token";

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz")
        {
            RefreshTokens = new List<Domain.Models.Users.RefreshToken>
                {
                    new Domain.Models.Users.RefreshToken { Token = refreshToken }
                }
        };

        userRepositoryMock.Setup(u => u.GetUserByUserNameWithRefreshToken(userName, CancellationToken.None))
                .ReturnsAsync(user);

        tokenServiceMock.Setup(t => t.CreateToken(user))
            .ReturnsAsync("token");

        var refreshTokenCommand = new RefreshTokenCommand(userName);
        var refreshTokenHandler = new RefreshTokenHandler(
            httpContextAccessorServiceMock.Object,
            tokenServiceMock.Object,
            userRepositoryMock.Object);

        // Act
        var result = await refreshTokenHandler.Handle(refreshTokenCommand, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal($"{user.FirstName} {user.LastName}", result.ResultValue.Name);
        Assert.Equal(user.Email, result.ResultValue.Email);
        Assert.Equal("token", result.ResultValue.Token);
    }

    [Fact]
    public async Task Handle_ExpiredRefreshToken_FailureResult()
    {
        // Arrange
        var userName = "johndoe";
        var expiredRefreshToken = "expired_refresh_token";

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz")
        {
            RefreshTokens = new List<Domain.Models.Users.RefreshToken>()
        };

        var expiredOrRevokedToken = new Domain.Models.Users.RefreshToken
        {
            Token = expiredRefreshToken,
            Expires = DateTime.UtcNow.AddHours(-1)
        };

        user.RefreshTokens.Add(expiredOrRevokedToken);

        // Set up the expected behavior of the httpContextAccessorServiceMock
        httpContextAccessorServiceMock.Setup(x => x.GetRefreshTokenCookie())
            .Returns(expiredRefreshToken);

        // Set up the expected behavior of the userRepositoryMock
        userRepositoryMock.Setup(u => u.GetUserByUserNameWithRefreshToken(userName, CancellationToken.None))
            .ReturnsAsync(user);

        var refreshTokenCommand = new RefreshTokenCommand(userName);
        var refreshTokenHandler = new RefreshTokenHandler(
            httpContextAccessorServiceMock.Object,
            tokenServiceMock.Object,
            userRepositoryMock.Object);

        // Act
        var result = await refreshTokenHandler.Handle(refreshTokenCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Unauthorized", result.ErrorMessage);
        Assert.Null(result.ResultValue);
    }

    [Fact]
    public async Task Handle_RevokedRefreshToken_FailureResult()
    {
        // Arrange
        var userName = "johndoe";
        var revokedRefreshToken = "revoked_refresh_token";

        var user = new ApplicationUser("test@example.com", "John", "Doe", "12345678", "xyz")
        {
            RefreshTokens = new List<Domain.Models.Users.RefreshToken>()
        };

        var revokedToken = new Domain.Models.Users.RefreshToken
        {
            Token = revokedRefreshToken,
            Revoked = DateTime.UtcNow.AddMinutes(-10)
        };

        user.RefreshTokens.Add(revokedToken);

        httpContextAccessorServiceMock.Setup(x => x.GetRefreshTokenCookie())
            .Returns(revokedRefreshToken);

        userRepositoryMock.Setup(u => u.GetUserByUserNameWithRefreshToken(userName, CancellationToken.None))
            .ReturnsAsync(user);

        var refreshTokenCommand = new RefreshTokenCommand(userName);
        var refreshTokenHandler = new RefreshTokenHandler(
            httpContextAccessorServiceMock.Object,
            tokenServiceMock.Object,
            userRepositoryMock.Object);

        // Act
        var result = await refreshTokenHandler.Handle(refreshTokenCommand, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Unauthorized", result.ErrorMessage);
        Assert.Null(result.ResultValue);
    }
}
