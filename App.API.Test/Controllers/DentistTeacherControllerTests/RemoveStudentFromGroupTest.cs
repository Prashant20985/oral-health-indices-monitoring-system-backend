using App.Application.Core;
using App.Application.DentistTeacherOperations.Command.RemoveStudentFromGroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class RemoveStudentFromGroupTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public RemoveStudentFromGroupTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task RemoveStudentFromGroup_Returns_OkResult()
    {
        //Arrange
        var groupId = Guid.NewGuid();
        var studentId = "test";

        _mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStudentFromGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _dentistTeacherController.RemoveStudentFromGroup(groupId, studentId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task RemoveStudentFromGroup_Returns_BadRequestResult()
    {
        //Arrange
        var groupId = Guid.NewGuid();
        var studentId = "test";

        _mediatorMock.Setup(m => m.Send(It.IsAny<RemoveStudentFromGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        // Act
        var result = await _dentistTeacherController.RemoveStudentFromGroup(groupId, studentId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
