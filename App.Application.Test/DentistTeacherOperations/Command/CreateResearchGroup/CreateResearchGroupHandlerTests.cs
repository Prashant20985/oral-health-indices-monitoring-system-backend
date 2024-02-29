using App.Application.DentistTeacherOperations.Command.CreateResearchGroup;
using App.Domain.DTOs;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.CreateResearchGroup;

public class CreateResearchGroupHandlerTests : TestHelper
{
    private readonly CreateResearchGroupHandler handler;

    public CreateResearchGroupHandlerTests()
    {
        handler = new CreateResearchGroupHandler(researchGroupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateResearchGroup()
    {
        // Arrange
        var researchGroup = new CreateUpdateResearchGroupDto
        {
            GroupName = "test",
            Description = "test"
        };

        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupByName(It.IsAny<string>()))
            .ReturnsAsync(value: null);

        var command = new CreateResearchGroupCommand(researchGroup, "doctorId");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        researchGroupRepositoryMock.Verify(repo => repo.GetResearchGroupByName("test"), Times.Once);
        researchGroupRepositoryMock.Verify(repo => repo.CreateResearchGroup(It.IsAny<ResearchGroup>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithExistingResearchGroup_ShouldReturnFailureResult()
    {
        // Arrange
        var researchGroup = new CreateUpdateResearchGroupDto
        {
            GroupName = "test",
            Description = "test"
        };

        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupByName(It.IsAny<string>()))
            .ReturnsAsync(new ResearchGroup("test", "test", "doctorId"));

        var command = new CreateResearchGroupCommand(researchGroup, "doctorId");

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Group already exists.", result.ErrorMessage);
        researchGroupRepositoryMock.Verify(repo => repo.GetResearchGroupByName("test"), Times.Once);
        researchGroupRepositoryMock.Verify(repo => repo.CreateResearchGroup(It.IsAny<ResearchGroup>()), Times.Never);
    }

}
