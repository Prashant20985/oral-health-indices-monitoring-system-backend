using App.Application.Interfaces;
using App.Domain.Models.Enums;
using Microsoft.AspNetCore.Hosting;

namespace App.Infrastructure.Email;

/// <summary>
/// A service that provides the path to the email template files based on the email type.
/// </summary>
public class EmailTemplatePathProvider : IEmailTemplatePathProvider
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplatePathProvider"/> class.
    /// </summary>
    /// <param name="webHostEnvironment">The web host environment service.</param>
    public EmailTemplatePathProvider(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Gets the file path for the email template based on the provided email type.
    /// </summary>
    /// <param name="emailType">The type of email for which the template path is needed.</param>
    /// <returns>The file path to the email template.</returns>
    public string GetTemplatePath(EmailType emailType)
    {
        string templateFolder = "EmailTemplates";
        string templateFileName = GetTemplateFileName(emailType);
        string templatePath = Path.Combine(_webHostEnvironment.ContentRootPath, templateFolder, templateFileName);

        return templatePath;
    }

    /// <summary>
    /// Determines the template file name based on the provided email type.
    /// </summary>
    /// <param name="emailType">The type of email for which the template file name is needed.</param>
    /// <returns>The file name of the email template.</returns>
    private static string GetTemplateFileName(EmailType emailType)
    {
        // Logic to determine the template file name based on the email type
        return emailType switch
        {
            EmailType.Registration => "registration_successfull.html",
            EmailType.PasswordReset => "password_reset.html",
            EmailType.PasswordResetConfirmation => "password_reset_confirmation.html",
            _ => throw new ArgumentException("Invalid email type."),
        };
    }
}
