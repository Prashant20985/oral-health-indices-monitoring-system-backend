using App.Application.Core;
using App.Application.DentistTeacherOperations.Command.UpdateGroupName;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class UpdateGroupNameTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public UpdateGroupNameTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task UpdateGroupName_Returns_OkResult()
    {
        //Arrange
        var groupId = Guid.NewGuid();
        var groupName = "test";

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateGroupNameCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _dentistTeacherController.UpdateGroupName(groupId, groupName);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateGroupName_Returns_BadRequestResult()
    {
        //Arrange
        var groupId = Guid.NewGuid();
        var groupName = "test";

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateGroupNameCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        // Act
        var result = await _dentistTeacherController.UpdateGroupName(groupId, groupName);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
