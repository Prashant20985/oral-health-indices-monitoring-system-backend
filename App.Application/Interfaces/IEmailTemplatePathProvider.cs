using App.Domain.Models.Enums;

namespace App.Application.Interfaces;

/// <summary>
/// Service to provide Email Template path based on Email types.
/// </summary>
public interface IEmailTemplatePathProvider
{
    string GetTemplatePath(EmailType emailType);
}