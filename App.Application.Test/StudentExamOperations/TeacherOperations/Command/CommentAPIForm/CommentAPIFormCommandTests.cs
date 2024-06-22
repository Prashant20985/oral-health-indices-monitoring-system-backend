using App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIForm;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.CommentAPIForm;

public class CommentAPIFormCommandTests : TestHelper
{
    private readonly CommentAPIFormHandler handler;
    private readonly CommentAPIFormCommand command;

    public CommentAPIFormCommandTests()
    {
        handler = new CommentAPIFormHandler(studentExamRepositoryMock.Object);
        command = new CommentAPIFormCommand(Guid.NewGuid(), "Doctor comment");
    }

    [Fact]
    public async Task Handle_WhenAPIFormDoesNotExist_ShouldReturnFailureRessult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetPracticeAPIByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Practice API not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenAPIFormExists_ShouldReturnSucessResult()
    {
        // Arrange
        var practiceAPI = new PracticeAPI(22,22,22);

        studentExamRepositoryMock.Setup(x => x.GetPracticeAPIByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(practiceAPI);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.DoctorComment, practiceAPI.Comment);
    }
}
