using App.Application.Interfaces;
using App.Application.NotificationOperations.DTOs;
using App.Domain.Models.Enums;
using MediatR;
using Polly;

namespace App.Application.NotificationOperations;

/// <summary>
/// Class representing the handler for the email notification.
/// </summary>
internal sealed class EmailNotificationHandler : INotificationHandler<EmailNotification>
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
    public async Task Handle(EmailNotification notification, CancellationToken cancellationToken)
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
                    }, cancellationToken);
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
    public async Task SendBatch(List<EmailContentDto> batch)
    {
        // Send the batched emails concurrently
        var sendTasks = batch.Select(SendEmailAsync);
        await Task.WhenAll(sendTasks);
    }

    /// <summary>
    /// Sends an individual email using Polly for retrying failed attempts.
    /// </summary>
    /// <param name="emailContent">The email content to send.</param>
    public async Task SendEmailAsync(EmailContentDto emailContent)
    {
       
        // Retry 3 times with a delay of 5 seconds between each attempt
        int maxRetryAttempts = 3;
        TimeSpan retryDelay = TimeSpan.FromSeconds(5);
    
        // Retry sending the email in case of failure
        var retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(maxRetryAttempts, attempt => retryDelay, (exception, timeSpan, attempt, context) =>
            {
                Console.WriteLine($"Retry attempt {attempt} failed: {exception.Message}");
            });

        // Send the email using the email service
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
