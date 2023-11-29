namespace App.Infrastructure.Configuration;

public class EmailSettings
{
    /// <summary>
    /// Gets or sets the email host.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the email port.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username for email authentication.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the password for email authentication.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the email address from which emails will be sent.
    /// </summary>
    public string FromAddress { get; set; }

    /// <summary>
    /// Gets or sets the name associated with the email sender.
    /// </summary>
    public string FromName { get; set; }
}
