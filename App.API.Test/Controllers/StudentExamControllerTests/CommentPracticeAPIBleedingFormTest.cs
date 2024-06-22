using App.Application.Core;
using App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIBleedingForm;
using App.Domain.Models.CreditSchema;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.StudentExamControllerTests;

public class CommentPracticeAPIBleedingFormTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestableStudentExamController _studentExamController;

    public CommentPracticeAPIBleedingFormTest()
    {
        _mediator = new Mock<IMediator>();
        _studentExamController = new TestableStudentExamController();
        _studentExamController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task CommentPracticeAPIBleedingForm_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, groupId);
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);
        var practiceAPIBleeding = new PracticeAPIBleeding(22, 22);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id, practiceDMFT_DMFS.Id, practiceAPIBleeding.Id);

        var comment = "This is a test comment.";

        _mediator.Setup(x => x.Send(It.IsAny<CommentAPIBleedingFormCommand>(), default))
                 .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _studentExamController.CommentPracticeAPIBleedingForm(practicePatientExaminationResult.Id, comment);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task CommentPracticeAPIBleedingForm_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var exam = new Exam(DateTime.Now, "exam", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, groupId);
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(exam.Id, studentId);
        var practiceAPIBleeding = new PracticeAPIBleeding(22, 22);
        var practiceBewe = new PracticeBewe(22);
        var practiceDMFT_DMFS = new PracticeDMFT_DMFS(22, 22);
        var practicePatientExaminationResult = new PracticePatientExaminationResult(practiceBewe.Id, practiceDMFT_DMFS.Id, practiceAPIBleeding.Id);

        var comment = "";

        _mediator.Setup(x => x.Send(It.IsAny<CommentAPIBleedingFormCommand>(), default))
                 .ReturnsAsync(OperationResult<Unit>.Failure("Comment cannot be empty."));

        // Act
        var result = await _studentExamController.CommentPracticeAPIBleedingForm(practicePatientExaminationResult.Id, comment);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task CommentPracticeAPIBleedingForm_WithInvalidPracticePatientExaminationCard_ShouldReturnBadRequest()
    {
        // Arrange
        var practicePatientExaminationResultId = Guid.NewGuid();

        _mediator.Setup(x => x.Send(It.IsAny<CommentAPIBleedingFormCommand>(), default))
                 .ReturnsAsync(OperationResult<Unit>.Failure("PracticePatientExaminationCard not found."));

        // Act
        var result = await _studentExamController.CommentPracticeAPIBleedingForm(Guid.NewGuid(), "This is a test comment.");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}
