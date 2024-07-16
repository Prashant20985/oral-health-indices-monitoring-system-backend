using App.API.Controllers;
using App.API.LogServices;
using App.Domain.Models.Logs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;

namespace App.API.Test.Controllers.LogControllerTests
{
    public class GetLogsForTodayAsyncTest
    {
        private readonly Mock<ILogService> _mockLogService;
        private readonly LogController _logController;

        public GetLogsForTodayAsyncTest()
        {
            _mockLogService = new Mock<ILogService>();

            _logController = new LogController(_mockLogService.Object);
        }

        [Fact]
        public async Task GetLogsForTodayAsync_ShouldReturnOkResult_WithListOfLogs()
        {
            // Arrange
            var mockLogs = new List<RequestLogDocument>
            {
                new RequestLogDocument
                {
                    Id = ObjectId.GenerateNewId(),
                    Timestamp = DateTime.UtcNow.Date,
                    MessageTemplate = "Test log 1",
                    RenderedMessage = "Test",
                    Level = "Info",
                    Properties = new RequestLogProperties
                    {
                        ExecutedBy = "User123",
                        RequestName = "GET /api/test",
                        DateTimeUtc = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    }
                },
                new RequestLogDocument
                {
                    Id = ObjectId.GenerateNewId(),
                    Timestamp = DateTime.UtcNow.Date,
                    MessageTemplate = "Test log 2",
                    RenderedMessage = "Test",
                    Level = "Debug",
                    Properties = new RequestLogProperties
                    {
                        ExecutedBy = "Admin456",
                        RequestName = "POST /api/data",
                        DateTimeUtc = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    }
                }
            };

            _mockLogService.Setup(service => service.GetLogsForTodayAsync())
                          .ReturnsAsync(mockLogs);

            // Act
            var result = await _logController.GetLogsForTodayAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var logs = Assert.IsAssignableFrom<List<RequestLogDocument>>(okResult.Value);
            Assert.Equal(2, logs.Count);
        }
    }
}
