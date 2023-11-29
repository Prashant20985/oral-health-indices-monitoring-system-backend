using App.Application.NotificationOperations.DTOs;
using MediatR;

namespace App.Application.NotificationOperations;

/// <summary>
/// Represents an email notification to be sent.
/// </summary>
/// <param name="EmailContent">The data transfer object containing neccessary information to send email</param>
public record EmailNotification(EmailContentDto EmailContent) : INotification;
