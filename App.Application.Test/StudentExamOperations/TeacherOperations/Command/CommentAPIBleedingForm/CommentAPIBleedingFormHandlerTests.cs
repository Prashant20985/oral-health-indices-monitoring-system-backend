using App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIBleedingForm;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.CommentAPIBleedingForm;

public class CommentAPIBleedingFormHandlerTests : TestHelper
{
    private readonly CommentAPIBleedingFormHandler handler;
    private readonly CommentAPIBleedingFormCommand command;

    public CommentAPIBleedingFormHandlerTests()
    {
        handler = new CommentAPIBleedingFormHandler(studentExamRepositoryMock.Object);
        command = new CommentAPIBleedingFormCommand(Guid.NewGuid(), "doctorComment");
    }

    [Fact]
    public async Task Handle_WhenAPIBleedingFormDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetPracticeAPIBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Practice API Bleeding form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenAPIBleedingFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var practiceApiBleeding = new PracticeAPIBleeding(22, 22);

        studentExamRepositoryMock.Setup(x => x.GetPracticeAPIBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(practiceApiBleeding);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.DoctorComment, practiceApiBleeding.Comments);
    }
}
