using App.Application.Core;
using App.Application.StudentExamOperations.StudentOperations.Query.ExamEligbility;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.StudentControllerTests;

public class CheckExamEligibilityTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentController _studentController;

    public CheckExamEligibilityTest()
    {
        _mediator = new Mock<IMediator>();
        _studentController = new TestableStudentController();
        _studentController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task CheckExamEligibility_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));

        var student = new ApplicationUser("student@test.com", "John", "Doe", "123456789", "test");
        var teacher = new ApplicationUser("teacher@test.com", "Jane", "Doe", "987654321", "test");

        var group = new Group(teacher.Id, "test123");
        group.Teacher = teacher;
        var studentGroup = new StudentGroup(group.Id, student.Id);
        var exam = new Exam(DateTime.Now, "test123", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group.Id);
        group.Exams.Add(exam);


        _mediator.Setup(x => x.Send(It.IsAny<ExamEligibiltyQuery>(), default))
            .ReturnsAsync(OperationResult<bool>.Success(true));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.CheckExamEligibility(exam.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<bool>(okResult.Value);
        Assert.True((bool)okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CheckExamEligibility_WithInvalidData_ShouldReturnOk()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "Student")
        }, "mock"));

        var student = new ApplicationUser("student@test.com", "John", "Doe", "123456789", "test");
        var teacher = new ApplicationUser("teacher@test.com", "Jane", "Doe", "987654321", "test");

        var group = new Group(teacher.Id, "test123");
        group.Teacher = teacher;
        var studentGroup = new StudentGroup(group.Id, student.Id);
        var exam = new Exam(DateTime.Now, "test123", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, group.Id);
        group.Exams.Add(exam);

        _mediator.Setup(x => x.Send(It.IsAny<ExamEligibiltyQuery>(), default))
        .ReturnsAsync(OperationResult<bool>.Success(false));

        _studentController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _studentController.CheckExamEligibility(exam.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<bool>(okResult.Value);
        Assert.False((bool)okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}
