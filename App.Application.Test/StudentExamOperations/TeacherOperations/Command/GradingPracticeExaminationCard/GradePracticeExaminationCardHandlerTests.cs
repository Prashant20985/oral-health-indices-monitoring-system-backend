using App.Application.StudentExamOperations.TeacherOperations.Command.GradingPracticeExaminationCard;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.GradingPracticeExaminationCard;

public class GradePracticeExaminationCardHandlerTests : TestHelper
{
    private readonly GradePracticeExaminationCardCommand command;
    private readonly GradePracticeExaminationCardHandler handler;

    public GradePracticeExaminationCardHandlerTests()
    {
        handler = new GradePracticeExaminationCardHandler(studentExamRepositoryMock.Object);
        command = new GradePracticeExaminationCardCommand(Guid.NewGuid(), 100);
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
        Assert.Equal("Examination Card not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenExamDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "studentId");

        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardById(It.IsAny<Guid>()))
            .ReturnsAsync(practicePatientExaminationCard);

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        Assert.False(result.IsSuccessful);
        Assert.Equal("Exam not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenStudentMarkIsNotValid_ShouldReturnFailureResult()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "studentId");
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 20, Guid.NewGuid());

        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardById(It.IsAny<Guid>()))
            .ReturnsAsync(practicePatientExaminationCard);

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(exam);

        // Act
        var result = await handler.Handle(new GradePracticeExaminationCardCommand(Guid.NewGuid(), 1000),
            CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Student mark is not valid", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenStudentMarkIsValid_ShouldReturnSuccessResult()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "studentId");
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue,
            TimeSpan.MaxValue, 100, Guid.NewGuid());

        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardById(It.IsAny<Guid>()))
            .ReturnsAsync(practicePatientExaminationCard);

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(exam);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal(command.StudentMark, practicePatientExaminationCard.StudentMark);
    }
}