using App.Application.Core;
using App.Application.UserRequestOperations.Command.CreateRequest;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.UserRequestControllerTests;

public class CreateRequestTest
{
    private readonly TestableUserRequestController _userRequestController;
    private readonly Mock<IMediator> _mediatorMock;

    public CreateRequestTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _userRequestController = new TestableUserRequestController();
        _userRequestController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateRequest_Returns_OkResult()
    {
        //Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "User")
        }));

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        _userRequestController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _userRequestController.CreateRequest("Test", "Test");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

    }

    [Fact]
    public async Task CreateRequest_Returns_BadRequestResult()
    {
        //Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "User")
        }));

        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        _userRequestController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _userRequestController.CreateRequest("Test", "Test");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task CreateRequest_NoUserInClaims_ReturnsNotFound()
    {
        // Arrange
        _userRequestController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _userRequestController.CreateRequest("Test", "Test");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
