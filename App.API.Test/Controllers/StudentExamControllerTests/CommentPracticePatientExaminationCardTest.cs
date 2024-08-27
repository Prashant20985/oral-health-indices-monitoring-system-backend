using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentPracticeExaminationCard;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class CommentPracticePatientExaminationCardTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public CommentPracticePatientExaminationCardTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task CommentPracticePatientExaminationCard_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);
        var comment = "This is a test comment.";

        _mediator.Setup(x => x.Send(It.IsAny<CommentPracticeExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result =
            await _studentExamController.CommentPracticePatientExaminationCard(practicePatientExaminationCard.Id,
                comment);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CommentPracticePatientExaminationCard_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, groupId);
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);
        var comment = "";

        _mediator.Setup(x => x.Send(It.IsAny<CommentPracticeExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Comment cannot be empty."));

        // Act
        var result =
            await _studentExamController.CommentPracticePatientExaminationCard(practicePatientExaminationCard.Id,
                comment);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task
        CommentPracticePatientExaminationCard_WithInvalidPracticePatientExaminationCard_ShouldReturnBadRequest()
    {
        // Arrange
        var practicePatientExaminationCardId = Guid.NewGuid();

        _mediator.Setup(x => x.Send(It.IsAny<CommentPracticeExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("PracticePatientExaminationCard not found."));

        // Act
        var result =
            await _studentExamController.CommentPracticePatientExaminationCard(practicePatientExaminationCardId,
                "This is a test comment.");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}