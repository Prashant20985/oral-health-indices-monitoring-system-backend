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
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly IEmailTemplatePathProvider _emailTemplatePathProvider;

    public EmailService(IOptions<EmailSettings> emailSettings, IEmailTemplatePathProvider emailTemplatePathProvider)
    {
        _emailSettings = emailSettings.Value;
        _emailTemplatePathProvider = emailTemplatePathProvider;
    }

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

            var body = new BodyBuilder(); ;

            // Build the email body
            switch (emailType)
            {
                case EmailType.Registration:
                    // Retrieve the template path for successfull registration email
                    var registrationSuccessfullTemplatePath = _emailTemplatePathProvider.GetTemplatePath(EmailType.Registration);
                    var registrationSuccessfullTemplate = await File.ReadAllTextAsync(registrationSuccessfullTemplatePath);

                    var username = message.Split(" ")[0];
                    var password = message.Split(" ")[1];

                    var registrationSuccessfullEmailContent = registrationSuccessfullTemplate
                        .Replace("{{username}}", username)
                        .Replace("{{password}}", password);

                    body.HtmlBody = registrationSuccessfullEmailContent;
                    break;

                case EmailType.PasswordResetConfirmation:
                    // Retrieve the template path for successfull password change email
                    var passwordResetConfirmationTemplatePath = _emailTemplatePathProvider.GetTemplatePath(EmailType.Registration);
                    var passwordResetConfirmationTemplate = await File.ReadAllTextAsync(passwordResetConfirmationTemplatePath);

                    username = message.Split(" ")[0];
                    password = message.Split(" ")[1];

                    var passwordResetConfirmationEmailContent = passwordResetConfirmationTemplate
                        .Replace("{{username}}", username)
                        .Replace("{{password}}", password);

                    body.HtmlBody = passwordResetConfirmationEmailContent;
                    break;

                case EmailType.PasswordReset:
                    // Retrieve the template path for password reset email
                    var passwordResetTemplatePath = _emailTemplatePathProvider.GetTemplatePath(EmailType.PasswordReset);
                    var passwordResetTemplate = await File.ReadAllTextAsync(passwordResetTemplatePath);
                    var passwordResetEmailContent = passwordResetTemplate.Replace("{{callbackUrl}}", message);
                    body.HtmlBody = passwordResetEmailContent;
                    break;
                default:
                    break;
            }

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
