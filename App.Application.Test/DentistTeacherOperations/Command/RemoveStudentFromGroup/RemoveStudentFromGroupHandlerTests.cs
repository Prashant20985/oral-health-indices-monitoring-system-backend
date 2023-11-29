using App.Application.DentistTeacherOperations.Command.RemoveStudentFromGroup;
using App.Domain.Models.Users;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.RemoveStudentFromGroup;

public class RemoveStudentFromGroupHandlerTests : TestHelper
{
    private readonly RemoveStudentFromGroupHandler handler;
    private readonly RemoveStudentFromGroupCommand command;

    public RemoveStudentFromGroupHandlerTests()
    {
        handler = new RemoveStudentFromGroupHandler(groupRepositoryMock.Object);
        command = new RemoveStudentFromGroupCommand(Guid.NewGuid(), "studentId");
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldRemoveStudentFromGroup()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetStudentGroup(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(new StudentGroup(Guid.NewGuid(), "studentId123"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue); // Check that the result has Unit.Value
        groupRepositoryMock.Verify(repo => repo.GetStudentGroup(command.StudentId, command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.RemoveStudentFromGroup(It.IsAny<StudentGroup>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingStudentGroup_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetStudentGroup(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Student not in group", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetStudentGroup(command.StudentId, command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.RemoveStudentFromGroup(It.IsAny<StudentGroup>()), Times.Never);
    }
}
