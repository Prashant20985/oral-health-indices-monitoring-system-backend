using App.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace App.Infrastructure.Email;

public class EmailTemplatePathProvider : IEmailTemplatePathProvider
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EmailTemplatePathProvider(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string GetTemplatePath(EmailType emailType)
    {
        string templateFolder = "EmailTemplates";
        string templateFileName = GetTemplateFileName(emailType);
        string templatePath = Path.Combine(_webHostEnvironment.ContentRootPath, templateFolder, templateFileName);

        return templatePath;
    }

    private static string GetTemplateFileName(EmailType emailType)
    {
        // Logic to determine the template file name based on the email type
        return emailType switch
        {
            EmailType.Registration => "registration_successfull.html",
            EmailType.PasswordReset => "password_reset.html",
            EmailType.PasswordResetConfirmation => "password_reset_confiramtion.html",
            _ => throw new ArgumentException("Invalid email type."),
        };
    }
}

