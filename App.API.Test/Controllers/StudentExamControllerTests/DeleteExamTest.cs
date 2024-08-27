using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Command.DeleteExam;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class DeleteExamTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public DeleteExamTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task DeleteExam_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);

        _mediator.Setup(x => x.Send(It.IsAny<DeleteExamCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _studentExamController.DeleteExam(exam.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}