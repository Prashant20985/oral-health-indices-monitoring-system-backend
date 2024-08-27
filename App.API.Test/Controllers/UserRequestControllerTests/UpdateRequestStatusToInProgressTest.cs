using App.Application.AdminOperations.Command.UpdateRequestStatusToInProgress;
using App.Application.Core;
using App.Domain.DTOs.UserRequestDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.UserRequestControllerTests;

public class UpdateRequestStatusToInProgressTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableUserRequestController _userRequestController;

    public UpdateRequestStatusToInProgressTest()
    {
        _mediator = new Mock<IMediator>();
        _userRequestController = new TestableUserRequestController();
        _userRequestController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UpdateRequestStatusToInProgress_Returns_OkResult()
    {
        //Arrange
        var userRequest = new UserRequestResponseDto
        {
            Id = Guid.NewGuid(),
            RequestTitle = "test",
            Description = "test",
            DateSubmitted = DateTime.Now,
            RequestStatus = "test"
        };

        _mediator.Setup(m => m.Send(It.IsAny<UpdateRequestStatusToInProgressCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _userRequestController.UpdateRequestStatusToInProgress(userRequest.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdateRequestStatusToInProgress_Returns_BadRequestResult()
    {
        //Arrange
        var userRequest = new UserRequestResponseDto
        {
            Id = Guid.NewGuid(),
            RequestTitle = "test",
            Description = "test",
            DateSubmitted = DateTime.Now,
            RequestStatus = "test"
        };

        _mediator.Setup(m => m.Send(It.IsAny<UpdateRequestStatusToInProgressCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        // Act
        var result = await _userRequestController.UpdateRequestStatusToInProgress(userRequest.Id);

        // Assert
        var badResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badResult.StatusCode);
    }
}