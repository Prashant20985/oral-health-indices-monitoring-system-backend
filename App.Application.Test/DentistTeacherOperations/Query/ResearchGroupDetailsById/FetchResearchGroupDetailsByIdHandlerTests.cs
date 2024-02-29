using App.Application.DentistTeacherOperations.Query.ResearchGroupDetailsById;
using App.Domain.DTOs;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.ResearchGroupDetailsById;

public class FetchResearchGroupDetailsByIdHandlerTests : TestHelper
{
    private readonly FetchResearchGroupDetailsByIdHandler handler;
    private readonly FetchResearchGroupDetailsByIdQuery query;

    public FetchResearchGroupDetailsByIdHandlerTests()
    {
        handler = new FetchResearchGroupDetailsByIdHandler(researchGroupRepositoryMock.Object);
        query = new FetchResearchGroupDetailsByIdQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnResearchGroupDto()
    {
        // Arrange
        var researchGroup = new ResearchGroupDto
        {
            GroupName = "Research Group",
            Description = "Research Group Description"
        };

        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupDetailsById(It.IsAny<Guid>()))
            .ReturnsAsync(researchGroup);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal("Research Group", result.ResultValue.GroupName);
        Assert.Equal("Research Group Description", result.ResultValue.Description);
    }

    [Fact]
    public async Task Handle_WithInvalidQuery_ShouldReturnFailure()
    {
        // Arrange
        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupDetailsById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("Research group not found", result.ErrorMessage);
    }
}
