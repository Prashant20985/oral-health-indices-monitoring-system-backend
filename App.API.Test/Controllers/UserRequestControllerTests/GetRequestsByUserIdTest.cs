using App.Application.Core;
using App.Application.UserRequestOperations.Query.RequestsListByUserId;
using App.Domain.DTOs.UserRequestDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.UserRequestControllerTests;

public class GetRequestsByUserIdTest
{
    private readonly TestableUserRequestController _userRequestController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetRequestsByUserIdTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _userRequestController = new TestableUserRequestController();
        _userRequestController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetRequestsByUserId_Returns_OkResult()
    {
        //Arrange
        var userRequest = new UserRequestResponseDto
        {
            Id = Guid.NewGuid(),
            RequestTitle = "test",
            Description = "test",
            DateSubmitted = DateTime.Now,
            RequestStatus = "test",
        };

        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "User")
        }));

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchRequestsListByUserIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<UserRequestResponseDto>>.Success(new List<UserRequestResponseDto> { userRequest }));

        _userRequestController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _userRequestController.GetRequestsByUserId(userRequest.RequestStatus, userRequest.DateSubmitted);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.IsType<List<UserRequestResponseDto>>(((OkObjectResult)result).Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetRequestsByUserId_Returns_BadRequestResult()
    {
        //Arrange
        var userRequest = new UserRequestResponseDto
        {
            Id = Guid.NewGuid(),
            RequestTitle = "test",
            Description = "test",
            DateSubmitted = DateTime.Now,
            RequestStatus = "test",
        };

        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "User")
        }));

        _mediatorMock.Setup(m => m.Send(It.IsAny<FetchRequestsListByUserIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<UserRequestResponseDto>>.Failure("test"));

        _userRequestController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _userRequestController.GetRequestsByUserId(userRequest.RequestStatus, userRequest.DateSubmitted);

        // Assert
        var badResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badResult.StatusCode);
    }
}
