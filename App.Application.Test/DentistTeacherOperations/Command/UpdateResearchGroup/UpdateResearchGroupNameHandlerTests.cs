using App.Application.DentistTeacherOperations.Command.UpdateResearchGroup;
using App.Domain.DTOs.ResearchGroupDtos.Request;
using App.Domain.Models.OralHealthExamination;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.UpdateResearchGroup;

public class UpdateResearchGroupNameHandlerTests : TestHelper
{
    private readonly UpdateResearchGroupNameHandler handler;

    public UpdateResearchGroupNameHandlerTests()
    {
        handler = new UpdateResearchGroupNameHandler(researchGroupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Update_WIthValidCommand_ShouldUpdateReseachGroup()
    {
        // Arrange
        var researchGroupId = Guid.NewGuid();
        var UpdateResearchGroup = new CreateUpdateResearchGroupRequestDto
        {
            GroupName = "Updated Group Name",
            Description = "Updated Description"
        };

        var researchGroup = new ResearchGroup("Group Name", "Description", "doctorId");

        var command = new UpdateResearchGroupCommand(researchGroupId, UpdateResearchGroup);

        researchGroupRepositoryMock.Setup(x => x.GetResearchGroupById(researchGroupId))
            .ReturnsAsync(researchGroup);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        researchGroupRepositoryMock.Verify(x => x.GetResearchGroupById(researchGroupId), Times.Once);
    }

    [Fact]
    public async Task Handle_Update_WithInvalidCommand_ShouldReturnFailure()
    {
        // Arrange
        var researchGroupId = Guid.NewGuid();
        var UpdateResearchGroup = new CreateUpdateResearchGroupRequestDto
        {
            GroupName = "Updated Group Name",
            Description = "Updated Description"
        };

        var command = new UpdateResearchGroupCommand(researchGroupId, UpdateResearchGroup);

        researchGroupRepositoryMock.Setup(x => x.GetResearchGroupById(researchGroupId))
            .ReturnsAsync(value: null); ;

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        researchGroupRepositoryMock.Verify(x => x.GetResearchGroupById(researchGroupId), Times.Once);
    }
}
