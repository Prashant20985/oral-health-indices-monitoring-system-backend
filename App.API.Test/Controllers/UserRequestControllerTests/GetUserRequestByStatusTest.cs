using App.Application.AdminOperations.Query.UserRequests;
using App.Application.Core;
using App.Domain.DTOs.UserRequestDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.UserRequestControllerTests;

public class GetUserRequestByStatusTest
{
    private readonly TestableUserRequestController _userRequestController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetUserRequestByStatusTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _userRequestController = new TestableUserRequestController();
        _userRequestController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetUserRequestByStatus_Returns_OkResult()
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
        ;
        _mediatorMock.Setup(m => m.Send(It.IsAny<UserRequestQuery>(), default))
            .ReturnsAsync(OperationResult<List<UserRequestResponseDto>>.Success(new List<UserRequestResponseDto>()));

        // Act
        var result = await _userRequestController.GetUserRequestByStatus(userRequest.RequestStatus, userRequest.DateSubmitted);

        // Assert
        Assert.IsType<ActionResult<List<UserRequestResponseDto>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsAssignableFrom<List<UserRequestResponseDto>>(((OkObjectResult)result.Result).Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetUserRequestByStatus_Returns_BadRequestResult()
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

        _mediatorMock.Setup(m => m.Send(It.IsAny<UserRequestQuery>(), default))
            .ReturnsAsync(OperationResult<List<UserRequestResponseDto>>.Failure("test"));

        // Act
        var result = await _userRequestController.GetUserRequestByStatus(userRequest.RequestStatus, userRequest.DateSubmitted);

        // Assert
        Assert.IsType<ActionResult<List<UserRequestResponseDto>>>(result);
        var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.IsAssignableFrom<string>(((BadRequestObjectResult)result.Result).Value);
        Assert.Equal(StatusCodes.Status400BadRequest, badResult.StatusCode);
    }
}
