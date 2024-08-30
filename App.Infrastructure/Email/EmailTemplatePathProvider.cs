using App.Application.Interfaces;
using App.Domain.Models.Enums;
using System.Reflection;

namespace App.Infrastructure.Email;

/// <summary>
/// A service that provides the content of the email template files based on the email type.
/// </summary>
public class EmailTemplatePathProvider : IEmailTemplatePathProvider
{
    private readonly Assembly _assembly;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplatePathProvider"/> class.
    /// </summary>
    public EmailTemplatePathProvider()
    {
        _assembly = Assembly.GetExecutingAssembly();
    }

    /// <summary>
    /// Gets the content of the email template based on the provided email type.
    /// </summary>
    /// <param name="emailType">The type of email for which the template content is needed.</param>
    /// <returns>The content of the email template.</returns>
    public string GetTemplateContent(EmailType emailType)
    {
        string templateResourceName = GetTemplateResourceName(emailType);
        return GetEmbeddedResourceContent(templateResourceName);
    }

    /// <summary>
    /// Reads the content of an embedded resource.
    /// </summary>
    /// <param name="resourceName">The name of the resource.</param>
    /// <returns>The content of the resource.</returns>
    private string GetEmbeddedResourceContent(string resourceName)
    {
        using (var stream = _assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new FileNotFoundException($"Resource {resourceName} not found.");
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Determines the template resource name based on the provided email type.
    /// </summary>
    /// <param name="emailType">The type of email for which the template resource name is needed.</param>
    /// <returns>The resource name of the email template.</returns>
    private static string GetTemplateResourceName(EmailType emailType)
    {
        string resourceBaseName = "App.Infrastructure.Email.EmailTemplates";
        return emailType switch
        {
            EmailType.Registration => $"{resourceBaseName}.registration_successfull.html",
            EmailType.PasswordReset => $"{resourceBaseName}.password_reset.html",
            EmailType.PasswordResetConfirmation => $"{resourceBaseName}.password_reset_confirmation.html",
            _ => throw new ArgumentException("Invalid email type."),
        };
    }
}
