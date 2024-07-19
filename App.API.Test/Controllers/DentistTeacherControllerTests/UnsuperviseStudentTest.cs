using App.Application.Core;
using App.Application.DentistTeacherOperations.Command.UnsuperviseStudent;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class UnsuperviseStudentTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public UnsuperviseStudentTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task UnsuperviseStudent_Returns_OkResult()
    {
        //Arrange
        var teacher = new ApplicationUser("teacher@test.com", "john", "doe", "1234567890", "test");
        var student = new ApplicationUser("student@test.com", "jane", "doe", "987654321", "test123");

        var group = new Group(teacher.Id, "test");

        var supervise = new Supervise(teacher.Id, student.Id);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Dentist_Teacher_Researcher"),
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Examiner"),
        }, "mock"));

        _mediatorMock.Setup(m => m.Send(It.IsAny<UnsuperviseStudentCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        _dentistTeacherController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _dentistTeacherController.UnsuperviseStudent(student.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task UnsuperviseStudent_Returns_BadRequestResult()
    {
        //Arrange
        var teacher = new ApplicationUser("teacher@test.com", "john", "doe", "1234567890", "test");
        var student = new ApplicationUser("student@test.com", "jane", "doe", "987654321", "test123");

        var group = new Group(teacher.Id, "test");

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Dentist_Teacher_Researcher"),
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Examiner"),
        }, "mock"));

        _mediatorMock.Setup(m => m.Send(It.IsAny<UnsuperviseStudentCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Student not in supervison"));

        _dentistTeacherController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user },
        };

        // Act
        var result = await _dentistTeacherController.UnsuperviseStudent(student.Id);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("Student not in supervison", badRequestResult.Value);
    }
}
