using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.AdminOperations.Query.ApplicationUsersListQuesryFilter;

public class ApplicationUsersListQueryFilterTests
{
    private readonly ApplicationUsersListQueryFilter _quesryFilter;
    private readonly IQueryable<ApplicationUserResponseDto> _response;



    public ApplicationUsersListQueryFilterTests()
    {
        _quesryFilter = new ApplicationUsersListQueryFilter();
        _response = new List<ApplicationUserResponseDto>
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
        ApplicationUserPaginationAndSearchParams searchParams = new()
        {
            SearchTerm = "batman",
            Role = "Admin",
            UserType = "RegularUser"
        };

        // Act

        var filteredUsers = await _quesryFilter.ApplyFilters(_response, searchParams, CancellationToken.None);

        // Assert
        Assert.Single(filteredUsers.Users);
        Assert.Equal("batman", filteredUsers.Users[0].UserName);
        Assert.Equal("Admin", filteredUsers.Users[0].Role);
        Assert.Equal("RegularUser", filteredUsers.Users[0].UserType);
        Assert.Equal(1, filteredUsers.TotalUsersCount);
    }

    [Fact]
    public async Task ApplyFilters_ShouldApplyFiltersCorrectly_ReturnsNoUsers()
    {
        // Arrange
        ApplicationUserPaginationAndSearchParams searchParams = new()
        {
            SearchTerm = "QWERTY",
            Role = "Admin",
            UserType = ""
        };

        // Act
        var filteredUsers = await _quesryFilter.ApplyFilters(_response, searchParams, CancellationToken.None);

        // Assert
        Assert.Empty(filteredUsers.Users);
        Assert.Equal(0, filteredUsers.TotalUsersCount);
    }
}
