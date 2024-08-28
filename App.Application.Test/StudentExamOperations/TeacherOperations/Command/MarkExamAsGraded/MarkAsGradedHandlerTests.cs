using App.Application.StudentExamOperations.TeacherOperations.Command.MarkExamAsGraded;
using App.Domain.Models.CreditSchema;
using App.Domain.Models.Enums;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.MarkExamAsGraded;

public class MarkAsGradedHandlerTests : TestHelper
{
    private readonly MarkAsGradedHandler handler;
    private readonly MarkAsGradedCommand command;

    public MarkAsGradedHandlerTests()
    {
        handler = new MarkAsGradedHandler(studentExamRepositoryMock.Object);
        command = new MarkAsGradedCommand(Guid.NewGuid());
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
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal(ExamStatus.Graded, exam.ExamStatus);
    }
}
