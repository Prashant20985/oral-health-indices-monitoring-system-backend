using App.API.Controllers;
using App.API.LogServices;
using App.Domain.DTOs.Common.Response;
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
                Level = "Info",
                PageNumber = 1,
                PageSize = 50
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

            var totalCount = mockFilteredLogs.Count;

            _mockLogService.Setup(service => service.GetFilteredLogs(query))
                          .ReturnsAsync(new LogResponseDto { Logs = mockFilteredLogs, TotalCount = totalCount });

            // Act
            var result = await _logController.GetFilteredLogs(query) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var resultValue = result.Value as LogResponseDto;
            Assert.NotNull(resultValue);
            Assert.Equal(totalCount, resultValue.TotalCount);
            Assert.Single(resultValue.Logs);

            var firstLog = resultValue.Logs.First();
            Assert.Equal(mockFilteredLogs[0].Timestamp, firstLog.Timestamp);
            Assert.Equal(mockFilteredLogs[0].Level, firstLog.Level);
            Assert.Equal(mockFilteredLogs[0].Properties.ExecutedBy, firstLog.Properties.ExecutedBy);
        }
    }
}
