using App.Application.StudentExamOperations.TeacherOperations.Command.DeleteExam;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.DeleteExam;

public class DeleteExamHandlerTests : TestHelper
{
    private readonly DeleteExamHandler handler;
    private readonly DeleteExamCommand command;

    public DeleteExamHandlerTests()
    {
        handler = new DeleteExamHandler(studentExamRepositoryMock.Object);
        command = new DeleteExamCommand(Guid.NewGuid());
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
        studentExamRepositoryMock.Verify(x => x.DeleteExam(It.IsAny<Guid>()), Times.Once);
    }
}
