using App.Application.Core;
using App.Domain.Models.Enums;
using MediatR;

namespace App.Application.Interfaces;

/// <summary>
/// Interface for sending emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The recipient email address.</param>
    /// <param name="subject">The email subject.</param>
    /// <param name="message">The email message body.</param>
    /// <param name="emailType">The type of email to be sent.</param>
    Task<OperationResult<Unit>> SendEmailAsync(string email, string subject, string message, EmailType emailType);
}