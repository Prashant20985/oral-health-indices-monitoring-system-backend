using System.Reflection;
using App.Application.Core;
using App.Application.NotificationOperations;
using App.Application.NotificationOperations.DTOs;
using App.Domain.Models.Enums;
using MediatR;
using Moq;
using Polly;

namespace App.Application.Test.NotificationOperations;

public class EmailNotificationHandlerTests : TestHelper
{
    private readonly EmailContentDto emailContent;
    private readonly EmailNotificationHandler emailNotificationHandler;
    private readonly EmailContentDto passwordResetEmailContent;

    public EmailNotificationHandlerTests()
    {
        emailNotificationHandler = new EmailNotificationHandler(emailServiceMock.Object);

        emailContent = new EmailContentDto(
            "testemail@test.com",
            "Test Email",
            "TestUser Password",
            EmailType.Registration);

        passwordResetEmailContent = new EmailContentDto(
            "testemail@test.com",
            "Reset Link",
            "TestUser Password",
            EmailType.PasswordReset);
    }

    [Fact]
    public async Task Handle_PasswordResetEmailType_SendEmailWithoutBatching()
    {
        // Act
        var emailNotification = new EmailNotification(passwordResetEmailContent);
        await emailNotificationHandler.Handle(emailNotification, CancellationToken.None);

        // Assert
        emailServiceMock.Verify(e => e.SendEmailAsync(
            passwordResetEmailContent.ReceiverEmail,
            passwordResetEmailContent.Subject,
            passwordResetEmailContent.Message,
            passwordResetEmailContent.EmailType
        ), Times.Once);

        var batchedEmailsField = typeof(EmailNotificationHandler)
            .GetField("_batchedEmails",
                BindingFlags.NonPublic |
                BindingFlags.Instance)?
            .GetValue(emailNotificationHandler) as List<EmailContentDto>;

        Assert.NotNull(batchedEmailsField);
        Assert.Empty(batchedEmailsField); // No emails should be present in batch
    }

    [Fact]
    public async Task Handle_NonPasswordResetEmailType_AddsEmailToBatch()
    {
        // Act
        var emailNotification = new EmailNotification(emailContent);
        await emailNotificationHandler.Handle(emailNotification, CancellationToken.None);

        // Assert 
        var batchedEmailsField = typeof(EmailNotificationHandler)
            .GetField("_batchedEmails",
                BindingFlags.NonPublic |
                BindingFlags.Instance)?
            .GetValue(emailNotificationHandler) as List<EmailContentDto>;

        Assert.NotNull(batchedEmailsField);
        Assert.Single(batchedEmailsField);
        Assert.Equal(emailContent.ReceiverEmail, batchedEmailsField[0].ReceiverEmail);
        Assert.Equal(emailContent.Subject, batchedEmailsField[0].Subject);
        Assert.Equal(emailContent.Message, batchedEmailsField[0].Message);
        Assert.Equal(emailContent.EmailType, batchedEmailsField[0].EmailType);
    }

    [Fact]
    public async Task Handle__NonPasswordResetEmailType_ReachesBatchLimit_SendsBatch()
    {
        // Act
        var emailNotification = new EmailNotification(emailContent);

        for (var i = 0; i < 10; i++) await emailNotificationHandler.Handle(emailNotification, CancellationToken.None);

        // Assert
        emailServiceMock.Verify(e => e.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<EmailType>()
        ), Times.Exactly(10));

        var batchedEmailsField = typeof(EmailNotificationHandler)
            .GetField("_batchedEmails",
                BindingFlags.NonPublic |
                BindingFlags.Instance)?
            .GetValue(emailNotificationHandler) as List<EmailContentDto>;

        Assert.NotNull(batchedEmailsField);
        Assert.Empty(batchedEmailsField); // Batch should be cleared after sending
    }

    [Fact]
    public async Task Handle_NonPasswordResetEmailType_DoesNotReachBatchLimit_SchedulesBatch()
    {
        // Act
        var emailNotification = new EmailNotification(emailContent);
        await emailNotificationHandler.Handle(emailNotification, CancellationToken.None);

        // Assert
        var isBatchScheduledField = typeof(EmailNotificationHandler)
            .GetField("_isBatchScheduled",
                BindingFlags.NonPublic |
                BindingFlags.Instance)?
            .GetValue(emailNotificationHandler) as bool?;

        Assert.True(isBatchScheduledField);

        emailServiceMock.Verify(e => e.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<EmailType>()
        ), Times.Never);
    }

    [Fact]
    public async Task SendBatchAsync_SendEmailSuccessfully_ClearsBatch()
    {
        // Arrange
        var batchToSend = new List<EmailContentDto> { emailContent };

        // Act
        await emailNotificationHandler.SendBatch(batchToSend);

        // Assert
        var batchedEmailsField = typeof(EmailNotificationHandler)
            .GetField("_batchedEmails",
                BindingFlags.NonPublic |
                BindingFlags.Instance)?
            .GetValue(emailNotificationHandler) as List<EmailContentDto>;

        Assert.NotNull(batchedEmailsField);
        Assert.Empty(batchedEmailsField); // Batch should be cleared after sending
    }

    [Fact]
    public async Task SendEmailAsync_RetryPolicy_SucceedsAfterRetries()
    {
        // Arrange
        var maxRetryAttempts = 3;
        var retryDelay = TimeSpan.FromSeconds(1);
        var attemptCounter = 0;

        emailServiceMock.Setup(e => e.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<EmailType>()
        )).Callback<string, string, string, EmailType>((email, subject, message, type) =>
        {
            attemptCounter++;
            if (attemptCounter < maxRetryAttempts) throw new Exception("Failed to send email");
        }).ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        var retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(maxRetryAttempts, attempt => retryDelay,
                (exception, timeSpan, attempt, context) =>
                {
                    Console.WriteLine($"Retry attempt {attempt} failed: {exception.Message}");
                });

        var emailNotificationHandler = new EmailNotificationHandler(emailServiceMock.Object);

        // Act
        await retryPolicy.ExecuteAsync(async () => { await emailNotificationHandler.SendEmailAsync(emailContent); });

        // Assert
        emailServiceMock.Verify(e => e.SendEmailAsync(
            emailContent.ReceiverEmail,
            emailContent.Subject,
            emailContent.Message,
            emailContent.EmailType
        ), Times.Exactly(maxRetryAttempts)); // Should be retried maxRetryAttempts times

        emailServiceMock.Verify(e => e.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<EmailType>()
        ), Times.AtMost(maxRetryAttempts)); // At most maxRetryAttempts attempts should be made
    }
}