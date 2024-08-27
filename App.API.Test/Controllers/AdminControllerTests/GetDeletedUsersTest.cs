using App.Application.AdminOperations.Query.ApplicationUsersListQueryFilter;
using App.Application.AdminOperations.Query.DeletedApplicationUsersList;
using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.AdminControllerTests;

public class GetDeletedUsersTest
{
    private readonly TestableAdminController _adminController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetDeletedUsersTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _adminController = new TestableAdminController();
        _adminController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetDeletedUsers_Returns_OkResult()
    {
        //Arrange
        var deletedUsers = new List<ApplicationUserResponseDto>
        {
            new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test",
                IsAccountActive = false,
                DeletedAt = DateTime.Now,
                DeleteUserComment = "Testing"
            },
            new()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test2@test.com",
                UserType = "RegularUser",
                Role = "Admin",
                UserName = "test2",
                IsAccountActive = false,
                DeletedAt = DateTime.Now,
                DeleteUserComment = "Testing"
            }
        };

        var paginatedDeletedUsers = new PaginatedApplicationUserResponseDto
        {
            Users = deletedUsers,
            TotalUsersCount = deletedUsers.Count
        };

        var searchParams = new ApplicationUserPaginationAndSearchParams();

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchDeletedApplicationUsersListQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedApplicationUserResponseDto>.Success(paginatedDeletedUsers));

        // Act
        var result = await _adminController.GetDeletedUsers(searchParams);

        // Assert
        var actionResult = Assert.IsType<ActionResult<PaginatedApplicationUserResponseDto>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var model = Assert.IsType<PaginatedApplicationUserResponseDto>(okObjectResult.Value);
        Assert.Equal(deletedUsers.Count, model.Users.Count);
    }
}