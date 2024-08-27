using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.EntityFrameworkCore;
using Moq;

namespace App.Application.Test.AdminOperations.Query.DeactivatedApplicationUsersList;

public class FetchDeactivatedApplicationUsersListHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsOperationResultWithDeactivatedUsers()
    {
        // Arrange
        var user1 = new ApplicationUserResponseDto
        {
            FirstName = "Jhon",
            LastName = "Doe",
            IsAccountActive = true
        };

        var user2 = new ApplicationUserResponseDto
        {
            FirstName = "Bruce",
            LastName = "Wayne",
            IsAccountActive = false
        };

        var users = new List<ApplicationUserResponseDto> { user1, user2 };
        var filteredUsers = new PaginatedApplicationUserResponseDto
            { Users = new List<ApplicationUserResponseDto> { user1 }, TotalUsersCount = 1 };

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

        var query = new FetchDeactivatedApplicationUsersListQuery(new ApplicationUserPaginationAndSearchParams());
        var handler =
            new FetchDeactivatedApplicationUsersListHandler(userRepositoryMock.Object, queryFilterMock.Object);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Single(result.ResultValue.Users);
        Assert.Equal(user1.FirstName, result.ResultValue.Users[0].FirstName);
    }
}