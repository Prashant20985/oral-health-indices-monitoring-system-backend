using App.Application.StudentExamOperations.TeacherOperations.Command.CommentPracticeExaminationCard;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.CommentPracticeExaminationCard;

public class CommentPracticeExmaintionCardHandlerTests : TestHelper
{
    private readonly CommentPracticeExaminationCardCommand command;
    private readonly CommentPracticeExmaintionCardHandler handler;

    public CommentPracticeExmaintionCardHandlerTests()
    {
        handler = new CommentPracticeExmaintionCardHandler(studentExamRepositoryMock.Object);
        command = new CommentPracticeExaminationCardCommand(Guid.NewGuid(), "doctorComment");
    }

    [Fact]
    public async Task Handle_WhenPracticeExaminationCardDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("PracticePatientExaminationCard not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenPracticeExaminationCardExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var examId = Guid.NewGuid();
        var studentId = Guid.NewGuid().ToString();

        var practicePatientExaminationCard = new PracticePatientExaminationCard(examId, studentId);

        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardById(It.IsAny<Guid>()))
            .ReturnsAsync(practicePatientExaminationCard);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.DoctorComment, practicePatientExaminationCard.DoctorComment);
    }
}