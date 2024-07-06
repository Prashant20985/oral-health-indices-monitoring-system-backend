using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.Moq;

namespace App.Infrastructure.Test.QueryFilter;

public class QueryFilterTests
{
    private readonly Infrastructure.QueryFilter.QueryFilter queryFilter;
    private readonly IQueryable<ApplicationUserResponseDto> users;

    public QueryFilterTests()
    {
        queryFilter = new Infrastructure.QueryFilter.QueryFilter();
        users = new List<ApplicationUserResponseDto>
        {
            new ApplicationUserResponseDto
            {
                UserName = "batman",
                FirstName = "Bruce",
                Role = "Admin",
                UserType = "RegularUser"
            },
            new ApplicationUserResponseDto
            {
                UserName = "superman",
                FirstName = "Clark",
                Role = "Student",
                UserType = "RegularUser"
            },
            new ApplicationUserResponseDto
            {
                UserName = "spiderman",
                FirstName = "Peter",
                Role = "Admin",
                UserType = "GuestUser"
            }
        }.AsQueryable().BuildMock();
    }

    [Fact]
    public async Task ApplyFilters_ShouldApplyFiltersCorrectly_ReturnsSingleUser()
    {
        // Arrange
        SearchParams searchParams = new()
        {
            SearchTerm = "batman",
            Role = "Admin",
            UserType = "RegularUser"
        };

        // Act
        var filteredUsers = await queryFilter.ApplyFilters(users, searchParams, CancellationToken.None);

        // Assert
        Assert.Single(filteredUsers);
        Assert.Equal("batman", filteredUsers[0].UserName);
        Assert.Equal("Admin", filteredUsers[0].Role);
        Assert.Equal("RegularUser", filteredUsers[0].UserType);
    }

    [Fact]
    public async Task ApplyFilters_ShouldApplyFiltersCorrectly_ReturnsMiltipleUsers()
    {
        // Arrange
        SearchParams searchParams = new()
        {
            SearchTerm = "",
            Role = "Admin",
            UserType = ""
        };

        // Act
        var filteredUsers = await queryFilter.ApplyFilters(users, searchParams, CancellationToken.None);

        // Assert
        Assert.Equal(2, filteredUsers.Count);
        Assert.Equal("batman", filteredUsers[0].UserName);
        Assert.Equal("Admin", filteredUsers[0].Role);
        Assert.Equal("RegularUser", filteredUsers[0].UserType);
        Assert.Equal("spiderman", filteredUsers[1].UserName);
        Assert.Equal("Admin", filteredUsers[1].Role);
        Assert.Equal("GuestUser", filteredUsers[1].UserType);
    }

    [Fact]
    public async Task ApplyFilters_ShouldApplyFiltersCorrectly_ReturnsNoUsers()
    {
        // Arrange
        SearchParams searchParams = new()
        {
            SearchTerm = "QWERTY",
            Role = "Admin",
            UserType = ""
        };

        // Act
        var filteredUsers = await queryFilter.ApplyFilters(users, searchParams, CancellationToken.None);

        // Assert
        Assert.Empty(filteredUsers);
    }
}


