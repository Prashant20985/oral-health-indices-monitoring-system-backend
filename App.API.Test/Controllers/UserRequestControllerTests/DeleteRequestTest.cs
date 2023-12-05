using App.Application.Core;
using App.Application.UserRequestOperations.Command.DeleteRequest;
using App.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.UserRequestControllerTests;

public class DeleteRequestTest
{
    private readonly TestableUserRequestController _userRequestController;
    private readonly Mock<IMediator> _mediatorMock;

    public DeleteRequestTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _userRequestController = new TestableUserRequestController();
        _userRequestController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task DeleteRequest_Returns_OkResult()
    {
        //Arrange
        var userRequest = new UserRequestDto
        {
            Id = Guid.NewGuid(),
            RequestTitle = "test",
            Description = "test",
            DateSubmitted = DateTime.Now,
            RequestStatus = "test",
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _userRequestController.DeleteRequest(userRequest.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task DeleteRequest_Returns_BadRequestResult()
    {
        //Arrange
        var userRequest = new UserRequestDto
        {
            Id = Guid.NewGuid(),
            RequestTitle = "test",
            Description = "test",
            DateSubmitted = DateTime.Now,
            RequestStatus = "test",
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteRequestCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        // Act
        var result = await _userRequestController.DeleteRequest(userRequest.Id);

        // Assert
        var badResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badResult.StatusCode);
    }
}
