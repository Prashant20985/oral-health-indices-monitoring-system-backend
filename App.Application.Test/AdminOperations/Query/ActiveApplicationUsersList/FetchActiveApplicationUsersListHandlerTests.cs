using App.Application.AdminOperations.Query.ActiveApplicationUsersList;
using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.EntityFrameworkCore;
using Moq;

namespace App.Application.Test.AdminOperations.Query.ActiveApplicationUsersList;

public class FetchActiveApplicationUsersHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsOperationResultWithActiveUsers()
    {
        // Arrange
        var user1 = new ApplicationUserResponseDto
        {
            FirstName = "Jhon",
            LastName = "Doe"
        };

        var user2 = new ApplicationUserResponseDto
        {
            FirstName = "Bruce",
            LastName = "Wayne"
        };

        var users = new List<ApplicationUserResponseDto> { user1, user2 };
        var filteredUsers = new PaginatedApplicationUserResponseDto
        {
            Users = new List<ApplicationUserResponseDto> { user2 },
            TotalUsersCount = 1
        };

        userRepositoryMock.Setup(u => u.GetActiveApplicationUsersQuery("testUser"))
            .Returns(users.AsQueryable().BuildMock());

        queryFilterMock.Setup(filter =>
                filter.ApplyFilters(It.IsAny<IQueryable<ApplicationUserResponseDto>>(),
                    It.IsAny<ApplicationUserPaginationAndSearchParams>(), CancellationToken.None))
            .ReturnsAsync((IQueryable<ApplicationUserResponseDto> query, ApplicationUserPaginationAndSearchParams param,
                CancellationToken ct) =>
            {
                return filteredUsers;
            });

        var query = new FetchActiveApplicationUsersListQuery(
            new ApplicationUserPaginationAndSearchParams { SearchTerm = "Bruce" }, "testUser");
        var handler = new FetchActiveApplicationUsersListHandler(userRepositoryMock.Object, queryFilterMock.Object);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Single(result.ResultValue.Users);
        Assert.Equal(user2.FirstName, result.ResultValue.Users[0].FirstName);
    }
}