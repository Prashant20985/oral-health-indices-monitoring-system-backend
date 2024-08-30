using App.Infrastructure.Email;
using Moq;
using System.Reflection;
using App.Domain.Models.Enums;

namespace App.Infrastructure.Test.Email;

public class EmailTemplatePathProviderTests
{
    private readonly EmailTemplatePathProvider _emailTemplatePathProvider = new();
    private readonly Mock<Assembly> _mockAssembly = new();

    [Theory]
    [InlineData(EmailType.Registration, "App.Infrastructure.Email.EmailTemplates.registration_successfull.html")]
    [InlineData(EmailType.PasswordReset, "App.Infrastructure.Email.EmailTemplates.password_reset.html")]
    [InlineData(EmailType.PasswordResetConfirmation, "App.Infrastructure.Email.EmailTemplates.password_reset_confirmation.html")]
    public void GetTemplateContent_ShouldReturnCorrectResourceContent(EmailType emailType, string expectedResourceName)
    {
        // Arrange
        using var stream = new MemoryStream("<html>Mocked Content</html>"u8.ToArray());
        _mockAssembly.Setup(a => a.GetManifestResourceStream(expectedResourceName)).Returns(stream);

        // Act
        string actualContent = _emailTemplatePathProvider.GetTemplateContent(emailType);

        // Assert
        Assert.NotNull(actualContent);
    }


    [Fact]
    public void GetTemplateContent_ShouldThrowArgumentException_WhenInvalidEmailType()
    {
        // Arrange
        var invalidEmailType = (EmailType)999;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _emailTemplatePathProvider.GetTemplateContent(invalidEmailType));
    }
}
