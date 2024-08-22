using App.Domain.Models.Enums;

namespace App.Application.Interfaces;

/// <summary>
/// Service to provide Email Template path based on Email types.
/// </summary>
public interface IEmailTemplatePathProvider
{
    /// <summary>
    /// Gets the template path based on the email type.
    /// </summary>
    /// <param name="emailType"></param>
    /// <returns></returns>
    string GetTemplateContent(EmailType emailType);
}