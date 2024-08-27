using App.Application.DentistTeacherOperations.Query.ResearchGroups;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using MockQueryable.Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.ResearchGroups;

public class FetchResearchGroupsHandlerTests : TestHelper
{
    private readonly FetchResearchGroupsHandler handler;
    private readonly FetchResearchGroupsQuery query;

    public FetchResearchGroupsHandlerTests()
    {
        handler = new FetchResearchGroupsHandler(researchGroupRepositoryMock.Object);
        query = new FetchResearchGroupsQuery("Research group");
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnResearchGroupDto()
    {
        // Arrange
        var researchGroups = new List<ResearchGroupResponseDto>
        {
            new()
            {
                GroupName = "Research group",
                Description = "Research Group Description"
            }
        }.AsQueryable().BuildMockDbSet();

        researchGroupRepositoryMock.Setup(repo => repo.GetAllResearchGroups())
            .Returns(researchGroups.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal("Research group", result.ResultValue.First().GroupName);
        Assert.Equal("Research Group Description", result.ResultValue.First().Description);
    }

    [Fact]
    public async Task Handle_WithInvalidQuery_ShouldReturnEmpty()
    {
        // Arrange
        var researchGroups = new List<ResearchGroupResponseDto>().AsQueryable().BuildMockDbSet();

        researchGroupRepositoryMock.Setup(repo => repo.GetAllResearchGroups())
            .Returns(researchGroups.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result.ResultValue);
    }
}