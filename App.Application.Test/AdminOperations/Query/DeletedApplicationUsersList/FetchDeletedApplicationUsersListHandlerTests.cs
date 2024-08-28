using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.AdminOperations.Query.DeletedApplicationUsersList;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.AdminOperations.Query.DeletedApplicationUsersList;

public class FetchDeletedApplicationUsersListHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsOperationResultWithDeletedUsers()
    {
        // Arrange
        var user1 = new ApplicationUserResponseDto
        {
            FirstName = "Jhon",
            LastName = "Doe",
            DeleteUserComment = "Test Del"
        };

        var user2 = new ApplicationUserResponseDto
        {
            FirstName = "Bruce",
            LastName = "Wayne",
            DeleteUserComment = "Test Del"
        };

        var users = new List<ApplicationUserResponseDto> { user1, user2 };
        var filteredUsers = new PaginatedApplicationUserResponseDto { Users = new List<ApplicationUserResponseDto> { user1, user2 }, TotalUsersCount = 1 };

        userRepositoryMock.Setup(u => u.GetActiveApplicationUsersQuery("testUser")).Returns(users.AsQueryable().BuildMock());

        queryFilterMock.Setup(filter =>
                filter.ApplyFilters(It.IsAny<IQueryable<ApplicationUserResponseDto>>(), It.IsAny<ApplicationUserPaginationAndSearchParams>(), CancellationToken.None))
            .ReturnsAsync((IQueryable<ApplicationUserResponseDto> query, ApplicationUserPaginationAndSearchParams param, CancellationToken ct) =>
            {
                return filteredUsers;
            });

        var query = new FetchDeletedApplicationUsersListQuery(new ApplicationUserPaginationAndSearchParams());
        var handler = new FetchDeletedApplicationUsersListHandler(userRepositoryMock.Object, queryFilterMock.Object);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.ResultValue.Users.Count);
        Assert.Equal(user1.FirstName, result.ResultValue.Users[0].FirstName);
        Assert.Equal(user2.FirstName, result.ResultValue.Users[1].FirstName);
    }
}
