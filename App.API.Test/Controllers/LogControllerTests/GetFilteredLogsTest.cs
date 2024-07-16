using App.API.Controllers;
using App.API.LogServices;
using App.Domain.Models.Logs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;

namespace App.API.Test.Controllers.LogControllerTests
{
    public class GetFilteredLogsTest
    {
        private readonly Mock<ILogService> _mockLogService;
        private readonly LogController _logController;

        public GetFilteredLogsTest()
        {
            _mockLogService = new Mock<ILogService>();
            _logController = new LogController(_mockLogService.Object);
        }

        [Fact]
        public async Task GetFilteredLogs_ShouldReturnOkResult_WithFilteredLogs()
        {
            // Arrange
            var query = new LogQueryParameters
            {
                StartDate = DateTime.UtcNow.Date.AddDays(-1),
                EndDate = DateTime.UtcNow.Date,
                UserName = "User123",
                Level = "Info"
            };

            var mockFilteredLogs = new List<RequestLogDocument>
            {
                new RequestLogDocument
                {
                    Id = ObjectId.GenerateNewId(),
                    Timestamp = DateTime.UtcNow.Date.AddDays(-1),
                    MessageTemplate = "Test log 1",
                    RenderedMessage = "Test",
                    Level = "Info",
                    Properties = new RequestLogProperties
                    {
                        ExecutedBy = "User123",
                        RequestName = "GET /api/test",
                        DateTimeUtc = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    }
                }
            };

            _mockLogService.Setup(service => service.GetFilteredLogs(query))
                          .ReturnsAsync(mockFilteredLogs);

            // Act
            var result = await _logController.GetFilteredLogs(query);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var logs = Assert.IsAssignableFrom<List<RequestLogDocument>>(okResult.Value);
            Assert.Single(logs);
        }
    }
}
