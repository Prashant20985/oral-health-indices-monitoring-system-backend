using App.Application.DentistTeacherOperations.Command.DeleteResearchGroup;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.DeleteResearchGroup;

public class DeleteResearchGroupHandlerTests : TestHelper
{
    private readonly DeleteResearchGroupCommand command;
    private readonly DeleteResearchGroupHandler handler;

    public DeleteResearchGroupHandlerTests()
    {
        command = new DeleteResearchGroupCommand(Guid.NewGuid());
        handler = new DeleteResearchGroupHandler(researchGroupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldDeleteResearchGroup()
    {
        // Arrange
        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupById(It.IsAny<Guid>()))
            .ReturnsAsync(value: new ResearchGroup("researchGroupId123", "Group", "doctorId"));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        researchGroupRepositoryMock.Verify(repo => repo.GetResearchGroupById(command.ResearchGroupId), Times.Once);
        researchGroupRepositoryMock.Verify(repo => repo.DeleteResearchGroup(It.IsAny<ResearchGroup>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingResearchGroup_ShouldReturnFailureResult()
    {
        // Arrange
        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Research group not found.", result.ErrorMessage);
        researchGroupRepositoryMock.Verify(repo => repo.GetResearchGroupById(command.ResearchGroupId), Times.Once);
        researchGroupRepositoryMock.Verify(repo => repo.DeleteResearchGroup(It.IsAny<ResearchGroup>()), Times.Never);
    }
}
