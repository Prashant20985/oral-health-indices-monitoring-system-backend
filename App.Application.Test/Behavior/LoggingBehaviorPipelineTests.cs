using App.Application.Behavior;
using Microsoft.Extensions.Logging;
using Moq;
using static App.Application.Test.Behavior.SampleData;

namespace App.Application.Test.Behavior;

public class LoggingBehaviorTests : TestHelper
{
    private readonly Mock<ILogger<LoggingBehaviorPipeline<SampleRequest, SampleResponse>>> loggerMock;
    private readonly SampleRequest request;

    public LoggingBehaviorTests()
    {
        loggerMock = new Mock<ILogger<LoggingBehaviorPipeline<SampleRequest, SampleResponse>>>();
        request = new SampleRequest();
        httpContextAccessorServiceMock.Setup(s => s.GetUserName()).Returns("TestUser");
    }

    [Fact]
    public async Task Handle_SuccessfulRequest_LogsStartAndCompletion()
    {
        // Arrange
        var behavior = new LoggingBehaviorPipeline<SampleRequest, SampleResponse>(
            loggerMock.Object,
            httpContextAccessorServiceMock.Object);

        // Act
        var response = await behavior.Handle(request, NextHandler, CancellationToken.None);

        // Assert
        loggerMock.VerifyLog(
             logger => logger.LogInformation(
                 "Starting Request {@ExecutedBy}, {@RequestName}, {@DateTimeUtc}",
                 "TestUser",
                 "SampleRequest",
                 It.IsAny<DateTime>()),
             Times.Once);

        loggerMock.VerifyLog(
            logger => logger.LogInformation(
                "Completed Request {@ExecutedBy}, {@RequestName}, {@DateTimeUtc}",
                "TestUser",
                "SampleRequest",
                It.IsAny<DateTime>()),
            Times.Once);

        loggerMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_Failure_LogsError()
    {
        // Arrange
        var behavior = new LoggingBehaviorPipeline<SampleRequest, SampleResponse>(
            loggerMock.Object,
            httpContextAccessorServiceMock.Object);

        // Act
        await Assert.ThrowsAsync<Exception>(() =>
            behavior.Handle(request, () => throw new Exception("Test exception"), CancellationToken.None));

        // Assert
        loggerMock.VerifyLog(
             logger => logger.LogInformation(
                 "Starting Request {@ExecutedBy}, {@RequestName}, {@DateTimeUtc}",
                 "TestUser",
                 "SampleRequest",
                 It.IsAny<DateTime>()),
             Times.Once);

        loggerMock.VerifyLog(
            logger => logger.LogCritical(
                "Exception on Request {@ExecutedBy}, {@RequestName}, {@ErrorMessage}, {@DateTimeUtc}",
                "TestUser",
                "SampleRequest",
                "Test exception",
                It.IsAny<DateTime>()),
            Times.Once);

        loggerMock.VerifyNoOtherCalls();
    }

    private Task<SampleResponse> NextHandler()
    {
        return Task.FromResult(new SampleResponse());
    }
}
