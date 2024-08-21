using App.Domain.Models.Users;

namespace App.Domain.Test.Models.Users;

public class RefreshTokenTests
{
    [Fact]
    public void IsExpired_ShouldReturnTrue_WhenTokenIsExpired()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Expires = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var isExpired = refreshToken.IsExpired;

        // Assert
        Assert.True(isExpired);
    }

    [Fact]
    public void IsExpired_ShouldReturnFalse_WhenTokenIsNotExpired()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Expires = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var isExpired = refreshToken.IsExpired;

        // Assert
        Assert.False(isExpired);
    }

    [Fact]
    public void IsActive_ShouldReturnFalse_WhenTokenIsExpired()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Expires = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var isActive = refreshToken.IsActive;

        // Assert
        Assert.False(isActive);
    }

    [Fact]
    public void IsActive_ShouldReturnFalse_WhenTokenIsRevoked()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Expires = DateTime.UtcNow.AddDays(1),
            Revoked = DateTime.UtcNow
        };

        // Act
        var isActive = refreshToken.IsActive;

        // Assert
        Assert.False(isActive);
    }

    [Fact]
    public void IsActive_ShouldReturnTrue_WhenTokenIsNotExpiredAndNotRevoked()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Expires = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var isActive = refreshToken.IsActive;

        // Assert
        Assert.True(isActive);
    }

}
