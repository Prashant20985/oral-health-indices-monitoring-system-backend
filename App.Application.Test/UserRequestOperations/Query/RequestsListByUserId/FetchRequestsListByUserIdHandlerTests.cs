using App.Application.UserRequestOperations.Query.RequestsListByUserId;
using App.Domain.DTOs.UserRequestDtos.Response;
using App.Domain.Models.Enums;
using MockQueryable.EntityFrameworkCore;

namespace App.Application.Test.UserRequestOperations.Query.RequestsListByUserId;

public class FetchRequestsListByUserIdHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfUserRequestDtos()
    {
        // Arrange
        var handler = new FetchRequestsListByUserIdHandler(userRequestRepositoryMock.Object);

        var userId = Guid.NewGuid().ToString();
        var requestStatus = RequestStatus.Submitted.ToString();
        var dateSubmitted = DateTime.Now.Date;

        var query = new FetchRequestsListByUserIdQuery(userId, requestStatus, dateSubmitted);

        var userRequests = new List<UserRequestResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserName = "User 1",
                RequestTitle = "Request 1",
                Description = "Description 1",
                RequestStatus = RequestStatus.Submitted.ToString(),
                DateSubmitted = DateTime.Now
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserName = "User 1",
                RequestTitle = "Request 1",
                Description = "Description 1",
                RequestStatus = RequestStatus.Submitted.ToString(),
                DateSubmitted = DateTime.Now
            }
        }.AsQueryable().BuildMock();

        userRequestRepositoryMock.Setup(repo => repo.GetRequestsByUserIdAndStatus(userId, RequestStatus.Submitted))
            .Returns(userRequests);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(2, result.ResultValue.Count);
        Assert.Equal("Submitted", result.ResultValue[0].RequestStatus);
        Assert.Equal("Submitted", result.ResultValue[1].RequestStatus);
    }
}