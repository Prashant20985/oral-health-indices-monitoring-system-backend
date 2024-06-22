using App.Application.StudentExamOperations.TeacherOperations.Command.CommentBleedingForm;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.CommentBleedingForm;

public class CommentBleedingFormCommandTests : TestHelper
{
    private readonly CommentBleedingFormHandler handler;
    private readonly CommentBleedingFormCommand command;

    public CommentBleedingFormCommandTests()
    {
        handler = new CommentBleedingFormHandler(studentExamRepositoryMock.Object);
        command = new CommentBleedingFormCommand(Guid.NewGuid(), "Doctor comment");
    }

    [Fact]
    public async Task Handle_WhenBleedingFormDoesNotExist_ShouldReturnFailureRessult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetPracticeBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Practice Bleeding not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenBleedingFormExists_ShouldReturnSucessResult()
    {
        // Arrange
        var practiceBleeding = new PracticeBleeding(22,22,22);

        studentExamRepositoryMock.Setup(x => x.GetPracticeBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(practiceBleeding);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.DoctorComment, practiceBleeding.Comment);
    }
}
