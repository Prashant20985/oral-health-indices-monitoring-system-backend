using App.Application.AdminOperations.Query.ActiveApplicationUsersList;
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

        var searchParams = new SearchParams();

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchActiveApplicationUsersListQuery>(), default))
            .ReturnsAsync(OperationResult<List<ApplicationUserResponseDto>>.Success(activeUsers));

        // Act
        var result = await _adminController.GetActiveUsers(searchParams);

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<ApplicationUserResponseDto>>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var model = Assert.IsType<List<ApplicationUserResponseDto>>(okObjectResult.Value);
        Assert.Equal(activeUsers.Count, model.Count);
    }


    [Fact]
    public async Task GetActiveUsers_Returns_BadRequest()
    {
        // Arrange
        var searchParams = new SearchParams();

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchActiveApplicationUsersListQuery>(), default))
            .ReturnsAsync(OperationResult<List<ApplicationUserResponseDto>>.Failure("Failed to fetch active users"));

        // Act
        var result = await _adminController.GetActiveUsers(searchParams);

        // Assert
        var actionResult = Assert.IsType<ActionResult<List<ApplicationUserResponseDto>>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Equal("Failed to fetch active users", badRequestResult.Value);
    }
}
