using App.Application.DentistTeacherOperations.Command.AddStudentToGroup;
using App.Domain.Models.Users;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.AddStudentToGroup;

public class AddStudentToGroupHandlerTests : TestHelper
{
    private readonly AddStudentToGroupHandler handler;
    private readonly AddStudentToGroupCommand command;
    public AddStudentToGroupHandlerTests()
    {
        command = new AddStudentToGroupCommand(Guid.NewGuid(), "studentId");
        handler = new AddStudentToGroupHandler(groupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldAddStudentToGroup()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetStudentGroup(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        groupRepositoryMock.Verify(repo => repo.GetStudentGroup(command.StudentId, command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.AddStudentToGroup(It.IsAny<StudentGroup>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithExistingStudentInGroup_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetStudentGroup(It.IsAny<string>(), It.IsAny<Guid>()))
            .ReturnsAsync(new StudentGroup(Guid.NewGuid(), "existingStudentId"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Student already in group", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetStudentGroup(command.StudentId, command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.AddStudentToGroup(It.IsAny<StudentGroup>()), Times.Never);
    }
}
