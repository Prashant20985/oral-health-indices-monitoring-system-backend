using App.Application.Interfaces;

namespace App.Application.NotificationOperations.DTOs
{
    /// <summary>
    /// Represents the Data Transfer Object (DTO) for email content information used in notification operations.
    /// </summary>
    public class EmailContentDto
    {
        /// <summary>
        /// Gets or sets the email address of the receiver.
        /// </summary>
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the message content of the email.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the email.
        /// </summary>
        public EmailType EmailType { get; set; }
    }
}
