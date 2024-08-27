using App.Application.Core;
using App.Application.UserRequestOperations.Command.UpdateRequest;
using App.Domain.DTOs.UserRequestDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.UserRequestControllerTests;

public class UpdateRequestTitleAndDescriptionTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TestableUserRequestController _userRequestController;

    public UpdateRequestTitleAndDescriptionTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _userRequestController = new TestableUserRequestController();
        _userRequestController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task UpdateRequestTitleAndDescription_Returns_OkResult()
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

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _userRequestController.UpdateRequestTitleAndDescription(userRequest.Id, "test1", "test2");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdateRequestTitleAndDescription_Returns_BadRequestResult()
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

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        // Act
        var result = await _userRequestController.UpdateRequestTitleAndDescription(userRequest.Id, "test1", "test2");

        // Assert
        var badResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badResult.StatusCode);
    }
}