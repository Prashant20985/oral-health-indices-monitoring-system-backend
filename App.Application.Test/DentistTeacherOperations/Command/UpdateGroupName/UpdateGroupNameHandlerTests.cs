using App.Application.DentistTeacherOperations.Command.UpdateGroupName;
using App.Domain.Models.Users;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.UpdateGroupName;

public class UpdateGroupNameHandlerTests : TestHelper
{
    private readonly UpdateGroupNameCommand command;
    private readonly UpdateGroupNameHandler handler;

    public UpdateGroupNameHandlerTests()
    {
        command = new UpdateGroupNameCommand(Guid.NewGuid(), "New GroupName");
        handler = new UpdateGroupNameHandler(groupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldUpdateGroupName()
    {
        // Arrange
        Group group = new Group("teacherId123", "GroupName123");
        groupRepositoryMock.Setup(repo => repo.GetGroupById(It.IsAny<Guid>()))
            .ReturnsAsync(group);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal("New GroupName", group.GroupName);
        groupRepositoryMock.Verify(repo => repo.GetGroupById(command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.GetGroupByName(command.GroupName), Times.Once);
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
        Assert.Equal("Group not found", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetGroupById(command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.GetGroupByName(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WithExistingGroupName_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetGroupById(It.IsAny<Guid>()))
            .ReturnsAsync(new Group("teacherId123", "ExistingGroupName"));

        groupRepositoryMock.Setup(repo => repo.GetGroupByName(It.IsAny<string>()))
            .ReturnsAsync(new Group("teacherId456", "ExistingGroupName"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Group name already taken", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetGroupById(command.GroupId), Times.Once);
        groupRepositoryMock.Verify(repo => repo.GetGroupByName(command.GroupName), Times.Once);
    }
}
