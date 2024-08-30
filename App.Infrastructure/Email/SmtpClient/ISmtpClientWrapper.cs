using MimeKit;

namespace App.Infrastructure.Email.SmtpClient;

/// <summary>
/// Represents an SMTP client.
/// </summary>
public interface ISmtpClientWrapper
{
    /// <summary>
    /// Connects to the SMTP server asynchronously.
    /// </summary>
    /// <param name="host">The SMTP server host.</param>
    /// <param name="port">The SMTP server port.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ConnectAsync(string host, int port);

    /// <summary>
    /// Authenticates the user asynchronously.
    /// </summary>
    /// <param name="userName">The username.</param>
    /// <param name="password">The password.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AuthenticateAsync(string userName, string password);

    /// <summary>
    /// Sends a message asynchronously.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendAsync(MimeMessage message);

    /// <summary>
    /// Disconnects from the SMTP server asynchronously.
    /// </summary>
    /// <param name="quit">True to send the QUIT command to the server, false to just close the connection.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DisconnectAsync(bool quit);
}