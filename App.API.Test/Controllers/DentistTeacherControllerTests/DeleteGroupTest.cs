using App.Application.Core;
using App.Application.DentistTeacherOperations.Command.DeleteGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class DeleteGroupTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public DeleteGroupTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task DeleteGroup_Returns_OkResult()
    {
        //Arrange

        var groupId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _dentistTeacherController.DeleteGroup(groupId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteGroup_Returns_BadRequestResult()
    {
        //Arrange

        var groupId = Guid.NewGuid();

        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        // Act
        var result = await _dentistTeacherController.DeleteGroup(groupId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}