using App.Domain.Models.Enums;

namespace App.Application.NotificationOperations.DTOs
{
    /// <summary>
    /// Represents the Data Transfer Object (DTO) for email content information used in notification operations.
    /// </summary>
    public class EmailContentDto
    {
        public EmailContentDto(string receiverEmail,
            string subject,
            string message,
            EmailType emailType)
        {
            ReceiverEmail = receiverEmail;
            Subject = subject;
            Message = message;
            EmailType = emailType;
        }

        /// <summary>
        /// Gets or sets the email address of the receiver.
        /// </summary>
        public string ReceiverEmail { get; private set; }

        /// <summary>
        /// Gets or sets the subject of the email.
        /// </summary>
        public string Subject { get; private set; }

        /// <summary>
        /// Gets or sets the message content of the email.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets or sets the type of the email.
        /// </summary>
        public EmailType EmailType { get; private set; }
    }
}
