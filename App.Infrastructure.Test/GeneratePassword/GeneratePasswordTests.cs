using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace App.Infrastructure.Test.GeneratePassword;

public class GeneratePasswordTests
{
    [Fact]
    public void GenerateRandomPassword_ShouldGenerateValidPassword()
    {
        // Arrange
        var identityOptions = new IdentityOptions
        {
            Password = new PasswordOptions
            {
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonAlphanumeric = true,
                RequiredLength = 8
            }
        };

        var nonAlphanumericChars = "!@#$%^&*()-_=+[]{}<>:;,.?";

        var optionsMock = new Mock<IOptions<IdentityOptions>>();
        optionsMock.Setup(x => x.Value).Returns(identityOptions);

        var generatePassword = new Infrastructure.GeneratePassword.GeneratePassword(optionsMock.Object);

        // Act
        var password = generatePassword.GenerateRandomPassword();

        // Assert
        Assert.NotNull(password);
        Assert.True(password.Length >= identityOptions.Password.RequiredLength);
        Assert.Contains(password, char.IsDigit);
        Assert.Contains(password, char.IsLower);
        Assert.Contains(password, char.IsUpper);
        Assert.Contains(password, nonAlphanumericChars.Contains);
    }
}
