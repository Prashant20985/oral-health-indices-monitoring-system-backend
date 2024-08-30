using App.Application.Interfaces;
using App.Infrastructure.Configuration;
using App.Infrastructure.Email;
using Microsoft.Extensions.Options;
using Moq;
using App.Domain.Models.Enums;
using App.Infrastructure.Email.SmtpClient;
using MediatR;
using MimeKit;

namespace App.Infrastructure.Test.Email;

public class EmailServiceTests
{
    private readonly Mock<IEmailTemplatePathProvider> _mockTemplateProvider;
    private readonly Mock<ISmtpClientWrapper> _mockSmtpClient;
    private readonly EmailService _emailService;

    public EmailServiceTests()
    {
        Mock<IOptions<EmailSettings>> mockEmailSettings = new();
        _mockTemplateProvider = new Mock<IEmailTemplatePathProvider>();
        _mockSmtpClient = new Mock<ISmtpClientWrapper>();

        var emailSettings = new EmailSettings
        {
            UserName = "testuser",
            FromAddress = "test@example.com",
            Host = "smtp.example.com",
            Port = 587,
            Password = "password"
        };

        mockEmailSettings.Setup(x => x.Value).Returns(emailSettings);
        _emailService = new EmailService(mockEmailSettings.Object, _mockTemplateProvider.Object, _mockSmtpClient.Object);
    }

    [Fact]
    public async Task SendEmailAsync_RegistrationEmail_Success()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Welcome!";
        var message = "username password";
        var emailType = EmailType.Registration;
        var registrationTemplate = "Welcome {{username}}, your password is {{password}}.";

        _mockTemplateProvider.Setup(x => x.GetTemplateContent(EmailType.Registration))
            .Returns(registrationTemplate);

        _mockSmtpClient.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.SendAsync(It.IsAny<MimeMessage>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.DisconnectAsync(It.IsAny<bool>())).Returns(Task.CompletedTask);

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
    }

    [Fact]
    public async Task SendEmailAsync_PasswordResetEmail_Success()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Reset your password";
        var message = "http://resetlink.com";
        var emailType = EmailType.PasswordReset;
        var resetTemplate = "Reset your password using the following link: {{callbackUrl}}.";

        _mockTemplateProvider.Setup(x => x.GetTemplateContent(EmailType.PasswordReset))
            .Returns(resetTemplate);

        _mockSmtpClient.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.SendAsync(It.IsAny<MimeMessage>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.DisconnectAsync(It.IsAny<bool>())).Returns(Task.CompletedTask);

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
    }

    [Fact]
    public async Task SendEmailAsync_PasswordResetConfirmationEmail_Success()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Your password has been reset";
        var message = string.Empty;
        var emailType = EmailType.PasswordResetConfirmation;
        var resetConfirmationTemplate = "Your password has been successfully reset.";

        _mockTemplateProvider.Setup(x => x.GetTemplateContent(EmailType.PasswordResetConfirmation))
            .Returns(resetConfirmationTemplate);

        _mockSmtpClient.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.SendAsync(It.IsAny<MimeMessage>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.DisconnectAsync(It.IsAny<bool>())).Returns(Task.CompletedTask);

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
    }

    [Fact]
    public async Task SendEmailAsync_InvalidEmailType_Failure()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Test";
        var message = "message";
        var emailType = (EmailType)999; // Invalid email type

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains("Invalid email type", result.ErrorMessage);
    }

    [Fact]
    public async Task SendEmailAsync_SmtpConnectionFailure_Failure()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Test";
        var message = "message";
        var emailType = EmailType.PasswordReset;
        var resetTemplate = "Password reset link: {{callbackUrl}}";

        _mockTemplateProvider.Setup(x => x.GetTemplateContent(EmailType.PasswordReset))
            .Returns(resetTemplate);

        // Mock SmtpClient to throw an exception on ConnectAsync
        _mockSmtpClient.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ThrowsAsync(new Exception("SMTP connection error"));

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains("Failed to send email: SMTP connection error", result.ErrorMessage);
    }

    [Fact]
    public async Task SendEmailAsync_SmtpAuthenticationFailure_Failure()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Test";
        var message = "message";
        var emailType = EmailType.PasswordReset;
        var resetTemplate = "Password reset link: {{callbackUrl}}";

        _mockTemplateProvider.Setup(x => x.GetTemplateContent(EmailType.PasswordReset))
            .Returns(resetTemplate);

        _mockSmtpClient.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("SMTP authentication error"));

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains("Failed to send email: SMTP authentication error", result.ErrorMessage);
    }

    [Fact]
    public async Task SendEmailAsync_TemplateProcessingFailure_Failure()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Test";
        var message = "message";
        var emailType = EmailType.PasswordReset;

        _mockTemplateProvider.Setup(x => x.GetTemplateContent(EmailType.PasswordReset))
            .Throws(new Exception("Template processing error"));

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains("Failed to send email: Template processing error", result.ErrorMessage);
    }

    [Fact]
    public async Task SendEmailAsync_SmtpDisconnectionFailure_Failure()
    {
        // Arrange
        var email = "recipient@example.com";
        var subject = "Test";
        var message = "message";
        var emailType = EmailType.PasswordReset;
        var resetTemplate = "Password reset link: {{callbackUrl}}";

        _mockTemplateProvider.Setup(x => x.GetTemplateContent(EmailType.PasswordReset))
            .Returns(resetTemplate);

        _mockSmtpClient.Setup(x => x.ConnectAsync(It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.SendAsync(It.IsAny<MimeMessage>())).Returns(Task.CompletedTask);
        _mockSmtpClient.Setup(x => x.DisconnectAsync(It.IsAny<bool>()))
            .ThrowsAsync(new Exception("SMTP disconnection error"));

        // Act
        var result = await _emailService.SendEmailAsync(email, subject, message, emailType);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains("Failed to send email: SMTP disconnection error", result.ErrorMessage);
    }
}