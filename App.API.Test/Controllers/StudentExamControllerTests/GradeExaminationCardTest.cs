using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Command.GradingPracticeExaminationCard;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class GradeExaminationCardTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public GradeExaminationCardTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GradeExaminationCard_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);

        var studentMark = 20;

        _mediator.Setup(x => x.Send(It.IsAny<GradePracticeExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _studentExamController.GradeExaminationCard(practicePatientExaminationCard.Id, studentMark);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GradeExaminationCard_WithInvalidPatientExaminationCard_ShouldReturnBadRequest()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);

        var studentMark = 20;

        _mediator.Setup(x => x.Send(It.IsAny<GradePracticeExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Patient Examination form not found"));

        // Act
        var result = await _studentExamController.GradeExaminationCard(Guid.NewGuid(), studentMark);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task GradeExaminationCard_WithInvalidExam_ShouldReturnBadRequest()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), studentId);

        var studentMark = 20;

        _mediator.Setup(x => x.Send(It.IsAny<GradePracticeExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Exam not Found"));

        // Act
        var result = await _studentExamController.GradeExaminationCard(practicePatientExaminationCard.Id, studentMark);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}