using MimeKit;

namespace App.Infrastructure.Email.SmtpClient;

public class SmtpClientWrapper : ISmtpClientWrapper
{
    private readonly MailKit.Net.Smtp.SmtpClient _client = new();

    /// <inheritdoc />
    public async Task ConnectAsync(string host, int port) => await _client.ConnectAsync(host, port);

    /// <inheritdoc />
    public async Task AuthenticateAsync(string userName, string password) => await _client.AuthenticateAsync(userName, password);

    /// <inheritdoc />
    public async Task SendAsync(MimeMessage message) => await _client.SendAsync(message);

    /// <inheritdoc />
    public async Task DisconnectAsync(bool quit) => await _client.DisconnectAsync(quit);
}