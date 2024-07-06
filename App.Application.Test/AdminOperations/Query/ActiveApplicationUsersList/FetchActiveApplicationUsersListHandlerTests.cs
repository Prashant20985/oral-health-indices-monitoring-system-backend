﻿using App.Application.AdminOperations.Query.ActiveApplicationUsersList;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.Moq;
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
        var filteredUsers = new List<ApplicationUserResponseDto> { user2 };

        userRepositoryMock.Setup(u => u.GetActiveApplicationUsersQuery()).Returns(users.AsQueryable().BuildMock());

        queryFilterMock.Setup(filter =>
                filter.ApplyFilters(It.IsAny<IQueryable<ApplicationUserResponseDto>>(), It.IsAny<SearchParams>(), CancellationToken.None))
            .ReturnsAsync((IQueryable<ApplicationUserResponseDto> query, SearchParams param, CancellationToken ct) =>
            {
                return filteredUsers;
            });

        var query = new FetchActiveApplicationUsersListQuery(new SearchParams { SearchTerm = "Bruce" });
        var handler = new FetchActiveApplicationUsersListHandler(userRepositoryMock.Object, queryFilterMock.Object);

        // Act 
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Single(result.ResultValue);
        Assert.Equal(user2.FirstName, result.ResultValue[0].FirstName);
    }
}
