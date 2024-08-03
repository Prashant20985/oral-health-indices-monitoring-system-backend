using App.Application.AdminOperations.Query.ActiveApplicationUsersList;
using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests;

public class GetActiveUsersTest
{
    private readonly TestableAdminController _adminController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetActiveUsersTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _adminController = new TestableAdminController();
        _adminController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetActiveUsers_Returns_OkResult()
    {
        //Arrange
        var activeUsers = new List<ApplicationUserResponseDto>()
        {
            new ApplicationUserResponseDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test",
                IsAccountActive = true,
            },
            new ApplicationUserResponseDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test2@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test2",
                IsAccountActive = true
            }
        };

        PaginatedApplicationUserResponseDto paginatedActiveUsers = new PaginatedApplicationUserResponseDto
        {
            Users = activeUsers,
            TotalUsersCount = activeUsers.Count
        };

        var searchParams = new ApplicationUserPaginationAndSearchParams();

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchActiveApplicationUsersListQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedApplicationUserResponseDto>.Success(paginatedActiveUsers));

        // Act
        var result = await _adminController.GetActiveUsers(searchParams);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PaginatedApplicationUserResponseDto>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var model = Assert.IsType<PaginatedApplicationUserResponseDto>(okObjectResult.Value);
        Assert.Equal(activeUsers.Count, model.Users.Count);
    }
}
