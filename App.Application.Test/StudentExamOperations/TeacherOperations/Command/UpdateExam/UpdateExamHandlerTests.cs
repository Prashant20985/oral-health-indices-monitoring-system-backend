using App.Application.StudentExamOperations.TeacherOperations.Command.UpdateExam;
using App.Domain.DTOs.ExamDtos.Request;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.UpdateExam;

public class UpdateExamHandlerTests : TestHelper
{
    private readonly UpdateExamHandler handler;
    private readonly UpdateExamCommand command;

    public UpdateExamHandlerTests()
    {
        var updateExamDto = new UpdateExamDto
        {
            DateOfExamination = DateTime.Now,
            Description = "description",
            ExamTitle = "title",
            StartTime = TimeOnly.MinValue,
            EndTime = TimeOnly.MaxValue,
            DurationInterval = TimeSpan.MaxValue,
            MaxMark = 20
        };

        handler = new UpdateExamHandler(studentExamRepositoryMock.Object);
        command = new UpdateExamCommand(Guid.NewGuid(), updateExamDto);
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "title", "description", TimeOnly.MinValue, TimeOnly.MaxValue, TimeSpan.MaxValue, 20, Guid.NewGuid());

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(exam);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(ExamStatus.Published, exam.ExamStatus);
    }

    [Fact]
    public async Task Handle_WhenExamDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Exam not found", result.ErrorMessage);
    }

}
