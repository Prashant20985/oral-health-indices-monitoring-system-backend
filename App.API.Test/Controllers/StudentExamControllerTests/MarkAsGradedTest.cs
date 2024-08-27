using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Command.MarkExamAsGraded;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class MarkAsGradedTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public MarkAsGradedTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task MarkAsGraded_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);


        _mediator.Setup(x => x.Send(It.IsAny<MarkAsGradedCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _studentExamController.MarkAsGraded(exam.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task MarkAsGraded_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        _mediator.Setup(x => x.Send(It.IsAny<MarkAsGradedCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Exam not found"));

        // Act
        var result = await _studentExamController.MarkAsGraded(Guid.NewGuid());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}