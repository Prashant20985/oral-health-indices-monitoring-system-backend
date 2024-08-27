using App.Application.StudentExamOperations.TeacherOperations.Command.CommentDMFT_DMFSForm;
using App.Domain.Models.CreditSchema;
using MediatR;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Command.CommentDMFT_DMFSForm;

public class CommentDMFT_DMFSFormHandlerTests : TestHelper
{
    private readonly CommentDMFT_DMFSFormCommand command;
    private readonly CommentDMFT_DMFSFormHandler handler;

    public CommentDMFT_DMFSFormHandlerTests()
    {
        handler = new CommentDMFT_DMFSFormHandler(studentExamRepositoryMock.Object);
        command = new CommentDMFT_DMFSFormCommand(Guid.NewGuid(), "doctorComment");
    }

    [Fact]
    public async Task Handle_WhenDMFT_DMFSFormDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetPracticeDMFT_DMFSByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Practice DMFT/DMFS not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenDMFT_DMFSFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var practiceDmftDmfs = new PracticeDMFT_DMFS(22, 22);

        studentExamRepositoryMock.Setup(x => x.GetPracticeDMFT_DMFSByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(practiceDmftDmfs);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.DoctorComment, practiceDmftDmfs.Comment);
    }
}