using App.Application.Core;
using App.Application.DentistTeacherOperations.Command.AddStudentToGroup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class AddStudentToGroupTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public AddStudentToGroupTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task AddStudentToGroup_Returns_OkResult()
    {
        //Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var groupId = Guid.NewGuid();
        var studentId = "studentId";

        _mediatorMock.Setup(m => m.Send(It.IsAny<AddStudentToGroupCommand>(), default))
           .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.AddStudentToGroup(groupId, studentId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddStudentToGroup_Returns_BadRequestResult()
    {
        //Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var groupId = Guid.NewGuid();
        var studentId = "studentId";

        _mediatorMock.Setup(m => m.Send(It.IsAny<AddStudentToGroupCommand>(), default))
           .ReturnsAsync(OperationResult<Unit>.Failure("Error"));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.AddStudentToGroup(groupId, studentId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
