using App.Application.DentistTeacherOperations.Command.CreateGroup;
using App.Domain.Models.Users;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.CreateGroup;

public class CreateGroupHandlerTests : TestHelper
{
    private readonly CreateGroupCommand command;
    private readonly CreateGroupHandler handler;

    public CreateGroupHandlerTests()
    {
        command = new CreateGroupCommand("teacherId123", "Group");
        handler = new CreateGroupHandler(groupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateGroup()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetGroupByName(It.IsAny<string>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue); // Check that the result has Unit.Value
        groupRepositoryMock.Verify(repo => repo.GetGroupByName(command.GroupName), Times.Once);
        groupRepositoryMock.Verify(repo => repo.CreateGroup(It.IsAny<Group>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithExistingGroup_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetGroupByName(It.IsAny<string>()))
            .ReturnsAsync(new Group("teacherId123", "Group"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Group already exists", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetGroupByName(command.GroupName), Times.Once);
        groupRepositoryMock.Verify(repo => repo.CreateGroup(It.IsAny<Group>()), Times.Never);
    }
}
