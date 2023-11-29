using App.Application.AdminOperations.Query.DeletedApplicationUsersList;
using App.Application.Core;
using App.Domain.DTOs;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.AdminOperations.Query.DeletedApplicationUsersList;

public class FetchDeletedApplicationUsersListHandlerTests : TestHelper
{
    [Fact]
    public async Task Handle_ValidRequest_ReturnsOperationResultWithDeletedUsers()
    {
        // Arrange
        var user1 = new ApplicationUserDto
        {
            FirstName = "Jhon",
            LastName = "Doe",
            DeleteUserComment = "Test Del"
        };

        var user2 = new ApplicationUserDto
        {
            FirstName = "Bruce",
            LastName = "Wayne",
            DeleteUserComment = "Test Del"
        };

        var users = new List<ApplicationUserDto> { user1, user2 };
        var filteredUsers = new List<ApplicationUserDto> { user1, user2 };

        userRepositoryMock.Setup(u => u.GetActiveApplicationUsersQuery()).Returns(users.AsQueryable().BuildMock());

        queryFilterMock.Setup(filter =>
                filter.ApplyFilters(It.IsAny<IQueryable<ApplicationUserDto>>(), It.IsAny<SearchParams>(), CancellationToken.None))
            .ReturnsAsync((IQueryable<ApplicationUserDto> query, SearchParams param, CancellationToken ct) =>
            {
                return filteredUsers;
            });

        var query = new FetchDeletedApplicationUsersListQuery(new SearchParams());
        var handler = new FetchDeletedApplicationUsersListHandler(userRepositoryMock.Object, queryFilterMock.Object);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.ResultValue.Count);
        Assert.Equal(user1.FirstName, result.ResultValue[0].FirstName);
        Assert.Equal(user2.FirstName, result.ResultValue[1].FirstName);
    }
}
