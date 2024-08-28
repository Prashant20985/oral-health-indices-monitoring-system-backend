using App.Application.StudentExamOperations.TeacherOperations.Command.CommentBeweForm;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.CommentBeweForm;

public class CommentBeweFormHandlerTests : TestHelper
{
    private readonly CommentBeweFormHandler handler;
    private readonly CommentBeweFormCommand command;

    public CommentBeweFormHandlerTests()
    {
        handler = new CommentBeweFormHandler(studentExamRepositoryMock.Object);
        command = new CommentBeweFormCommand(Guid.NewGuid(), "doctorComment");
    }

    [Fact]
    public async Task Handle_WhenBeweFormDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetPracticeBeweByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Practice BEWE not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenBeweFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var practiceBewe = new PracticeBewe(22);

        studentExamRepositoryMock.Setup(x => x.GetPracticeBeweByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(practiceBewe);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.DoctorComment, practiceBewe.Comment);
    }
}
