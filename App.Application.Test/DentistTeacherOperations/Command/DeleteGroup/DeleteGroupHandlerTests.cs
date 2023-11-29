using App.Application.DentistTeacherOperations.Command.DeleteGroup;
using App.Domain.Models.Users;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.DeleteGroup;

public class DeleteGroupHandlerTests : TestHelper
{
    private readonly DeleteGroupCommand command;
    private readonly DeleteGroupHandler handler;

    public DeleteGroupHandlerTests()
    {
        command = new DeleteGroupCommand(Guid.NewGuid());
        handler = new DeleteGroupHandler(groupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldDeleteGroup()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetGroupById(It.IsAny<Guid>()))
            .ReturnsAsync(new Group("teacherId123", "GroupToDelete"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        groupRepositoryMock.Verify(repo => repo.GetGroupById(command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.DeleteGroup(It.IsAny<Group>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingGroup_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetGroupById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);


        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Group Id not found", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetGroupById(command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.DeleteGroup(It.IsAny<Group>()), Times.Never);
    }
}
