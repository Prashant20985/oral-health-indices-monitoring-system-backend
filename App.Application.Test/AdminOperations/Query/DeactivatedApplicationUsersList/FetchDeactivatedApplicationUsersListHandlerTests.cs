using App.Application.AdminOperations.Query.DeactivatedApplicationUsersList;
using App.Application.Core;
using App.Domain.DTOs;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.AdminOperations.Query.DeactivatedApplicationUsersList;

public class FetchDeactivatedApplicationUsersListHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsOperationResultWithDeactivatedUsers()
    {
        // Arrange
        var user1 = new ApplicationUserDto
        {
            FirstName = "Jhon",
            LastName = "Doe",
            IsAccountActive = true,
        };

        var user2 = new ApplicationUserDto
        {
            FirstName = "Bruce",
            LastName = "Wayne",
            IsAccountActive = false,
        };

        var users = new List<ApplicationUserDto> { user1, user2 };
        var filteredUsers = new List<ApplicationUserDto> { user1 };

        userRepositoryMock.Setup(u => u.GetActiveApplicationUsersQuery()).Returns(users.AsQueryable().BuildMock());

        queryFilterMock.Setup(filter =>
                filter.ApplyFilters(It.IsAny<IQueryable<ApplicationUserDto>>(), It.IsAny<SearchParams>(), CancellationToken.None))
            .ReturnsAsync((IQueryable<ApplicationUserDto> query, SearchParams param, CancellationToken ct) =>
            {
                return filteredUsers;
            });

        var query = new FetchDeactivatedApplicationUsersListQuery(new SearchParams());
        var handler = new FetchDeactivatedApplicationUsersListHandler(userRepositoryMock.Object, queryFilterMock.Object);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Single(result.ResultValue);
        Assert.Equal(user1.FirstName, result.ResultValue[0].FirstName);
    }
}
