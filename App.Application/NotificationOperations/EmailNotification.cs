using App.Application.Interfaces;
using App.Application.NotificationOperations.DTOs;
using MediatR;
using Polly;

namespace App.Application.NotificationOperations
{
    /// <summary>
    /// Represents the notification for sending an email.
    /// </summary>
    public class EmailNotification
    {
        /// <summary>
        /// Class representing the email notification.
        /// </summary>
        public class Email : INotification
        {
            /// <summary>
            /// Gets or sets the email content data transfer object.
            /// </summary>
            public EmailContentDto EmailContent { get; set; }
        }

        /// <summary>
        /// Class representing the handler for the email notification.
        /// </summary>
        public class EmailNotificationHandler : INotificationHandler<Email>
        {
            private readonly IEmailService _emailService;
            private readonly List<EmailContentDto> _batchedEmails = new();
            private readonly object _batchLock = new();
            private readonly int _maxBatchSize = 10;
            private readonly TimeSpan _sendInterval = TimeSpan.FromHours(1);
            private bool _isBatchScheduled = false;

            /// <summary>
            /// Initializes a new instance of the <see cref="EmailNotificationHandler"/> class with the specified email service.
            /// </summary>
            /// <param name="emailService">The email service for sending emails.</param>
            public EmailNotificationHandler(IEmailService emailService)
            {
                _emailService = emailService;
            }

            /// <summary>
            /// Handles the email notification and sends the email.
            /// Implements a retry mechanism using Polly for resiliency in case of failures during email sending.
            /// </summary>
            /// <param name="notification">The email notification to handle.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            public async Task Handle(Email notification, CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // If it's a password reset email, send it immediately
                if (notification.EmailContent.EmailType == EmailType.PasswordReset)
                {
                    await SendEmailAsync(notification.EmailContent);
                }
                else
                {
                    // For other email types, batch them
                    lock (_batchLock)
                    {
                        _batchedEmails.Add(notification.EmailContent);

                        // If batch size is reached, send the batch immediately
                        if (_batchedEmails.Count >= _maxBatchSize)
                        {
                            _ = SendBatchAsync();
                        }
                        else if (!_isBatchScheduled) // If not already scheduled, schedule the batch sending after an hour
                        {
                            _isBatchScheduled = true;
                            Task.Run(async () =>
                            {
                                await Task.Delay(_sendInterval);
                                await SendBatchAsync();
                            });
                        }
                    }
                }
            }

            /// <summary>
            /// Sends the batch of emails.
            /// </summary>
            private async Task SendBatchAsync()
            {
                // Reset the batch scheduling flag
                lock (_batchLock)
                {
                    _isBatchScheduled = false;
                }

                // Create a copy of the batched emails to be sent
                var batchToSend = new List<EmailContentDto>(_batchedEmails);
                _batchedEmails.Clear();

                // Send the batched emails concurrently
                await SendBatch(batchToSend);
            }

            /// <summary>
            /// Sends a batch of emails concurrently using Polly for retrying failed attempts.
            /// </summary>
            /// <param name="batch">The batch of emails to send.</param>
            private async Task SendBatch(List<EmailContentDto> batch)
            {
                var sendTasks = batch.Select(SendEmailAsync);
                await Task.WhenAll(sendTasks);
            }

            /// <summary>
            /// Sends an individual email using Polly for retrying failed attempts.
            /// </summary>
            /// <param name="emailContent">The email content to send.</param>
            private async Task SendEmailAsync(EmailContentDto emailContent)
            {
                int maxRetryAttempts = 3;
                TimeSpan retryDelay = TimeSpan.FromSeconds(5);

                var retryPolicy = Policy.Handle<Exception>()
                    .WaitAndRetryAsync(maxRetryAttempts, attempt => retryDelay, (exception, timeSpan, attempt, context) =>
                    {
                        Console.WriteLine($"Retry attempt {attempt} failed: {exception.Message}");
                    });

                await retryPolicy.ExecuteAsync(async () =>
                {
                    var result = await _emailService.SendEmailAsync(
                        emailContent.ReceiverEmail,
                        emailContent.Subject,
                        emailContent.Message,
                        emailContent.EmailType
                    );
                });
            }
        }
    }
}
