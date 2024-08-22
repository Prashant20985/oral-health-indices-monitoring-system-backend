using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Models.Enums;
using App.Infrastructure.Configuration;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;

namespace App.Infrastructure.Email;

/// <summary>
/// Service for sending emails using the configured email settings.
/// </summary>
public class EmailService(IOptions<EmailSettings> emailSettings,
    IEmailTemplatePathProvider emailTemplatePathProvider) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    private readonly IEmailTemplatePathProvider _emailTemplatePathProvider = emailTemplatePathProvider;

    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The recipient email address.</param>
    /// <param name="subject">The email subject.</param>
    /// <param name="message">The email message body.</param>
    /// <param name="emailType">The type of email to be sent.</param>
    public async Task<OperationResult<Unit>> SendEmailAsync(string email, string subject, string message, EmailType emailType)
    {
        try
        {
            // Create a new MimeMessage
            var mail = new MimeMessage();

            // Set the sender and recipient
            mail.From.Add(new MailboxAddress(_emailSettings.UserName, _emailSettings.FromAddress));
            mail.Sender = new MailboxAddress(_emailSettings.UserName, _emailSettings.FromAddress);
            mail.To.Add(MailboxAddress.Parse(email));

            var body = new BodyBuilder();

            // Build the email body
            switch (emailType)
            {
                // Build the email body based on the email type
                case EmailType.Registration:
                    var registrationTemplate = _emailTemplatePathProvider.GetTemplateContent(EmailType.Registration);
                    var username = message.Split(" ")[0];
                    var password = message.Split(" ")[1];

                    var registrationEmailContent = registrationTemplate
                        .Replace("{{username}}", username)
                        .Replace("{{password}}", password);

                    body.HtmlBody = registrationEmailContent;
                    break;

                case EmailType.PasswordResetConfirmation:
                    var resetConfirmationTemplate = _emailTemplatePathProvider.GetTemplateContent(EmailType.PasswordResetConfirmation);
                    body.HtmlBody = resetConfirmationTemplate;
                    break;

                case EmailType.PasswordReset:
                    var resetTemplate = _emailTemplatePathProvider.GetTemplateContent(EmailType.PasswordReset);
                    var resetEmailContent = resetTemplate.Replace("{{callbackUrl}}", message);
                    body.HtmlBody = resetEmailContent;
                    break;

                default:
                    throw new ArgumentException("Invalid email type.");
            }

            // Set the email subject and body
            mail.Subject = subject;
            mail.Body = body.ToMessageBody();

            // Connect to the SMTP server and send the email
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port);
            await smtp.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);

            // Indicate the successful email sending as a completed task result
            return OperationResult<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return OperationResult<Unit>.Failure($"Failed to send email: {ex.Message}");
        }
    }
}
