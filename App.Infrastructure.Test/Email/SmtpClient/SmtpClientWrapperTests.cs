using App.Infrastructure.Email.SmtpClient;
using MimeKit;
using Moq;

namespace App.Infrastructure.Test.Email.SmtpClient;

public class SmtpClientWrapperTests
{
    private readonly Mock<ISmtpClientWrapper> _mockSmtpClient;
    private readonly ISmtpClientWrapper _smtpClientWrapper;

    public SmtpClientWrapperTests()
    {
        // Mock the ISmtpClientWrapper interface
        _mockSmtpClient = new Mock<ISmtpClientWrapper>();
        _smtpClientWrapper = _mockSmtpClient.Object;
    }

    [Fact]
    public async Task ConnectAsync_ShouldCallConnectOnSmtpClient()
    {
        // Arrange
        var host = "smtp.example.com";
        var port = 587;

        // Act
        await _smtpClientWrapper.ConnectAsync(host, port);

        // Assert
        _mockSmtpClient.Verify(x => x.ConnectAsync(host, port), Times.Once);
    }

    [Fact]
    public async Task AuthenticateAsync_ShouldCallAuthenticateOnSmtpClient()
    {
        // Arrange
        var userName = "testuser";
        var password = "password";

        // Act
        await _smtpClientWrapper.AuthenticateAsync(userName, password);

        // Assert
        _mockSmtpClient.Verify(x => x.AuthenticateAsync(userName, password), Times.Once);
    }

    [Fact]
    public async Task SendAsync_ShouldCallSendOnSmtpClient()
    {
        // Arrange
        var message = new MimeMessage();

        // Act
        await _smtpClientWrapper.SendAsync(message);

        // Assert
        _mockSmtpClient.Verify(x => x.SendAsync(message), Times.Once);
    }

    [Fact]
    public async Task DisconnectAsync_ShouldCallDisconnectOnSmtpClient()
    {
        // Arrange
        var quit = true;

        // Act
        await _smtpClientWrapper.DisconnectAsync(quit);

        // Assert
        _mockSmtpClient.Verify(x => x.DisconnectAsync(quit), Times.Once);
    }
}
