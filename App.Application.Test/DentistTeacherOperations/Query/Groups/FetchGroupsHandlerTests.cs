using App.Application.DentistTeacherOperations.Query.Groups;
using App.Domain.DTOs;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.Groups;

public class FetchGroupsHandlerTests : TestHelper
{
    private readonly FetchGroupsHandler handler;
    private readonly FetchGroupsQuery query;

    public FetchGroupsHandlerTests()
    {
        handler = new FetchGroupsHandler(groupRepositoryMock.Object);
        query = new FetchGroupsQuery("teacher123");
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfGroupDtos()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetAllGroupsWithStudentsList(It.IsAny<string>()))
            .ReturnsAsync(new List<GroupDto>
            {
                new GroupDto { Id = Guid.NewGuid(), GroupName = "Group1" },
                new GroupDto { Id = Guid.NewGuid(), GroupName = "Group2" }
            });

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(2, result.ResultValue.Count);
        Assert.Equal("Group1", result.ResultValue[0].GroupName);
        Assert.Equal("Group2", result.ResultValue[1].GroupName);
        groupRepositoryMock.Verify(repo => repo.GetAllGroupsWithStudentsList(query.TeacherId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNoGroupsFound_ShouldReturnFailureResult()
    {
        // Arrange
        groupRepositoryMock.Setup(repo => repo.GetAllGroupsWithStudentsList(It.IsAny<string>()))
            .ReturnsAsync(new List<GroupDto>());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal($"No groups found for teacher with Id {query.TeacherId}", result.ErrorMessage);
        groupRepositoryMock.Verify(repo => repo.GetAllGroupsWithStudentsList(query.TeacherId), Times.Once);
    }
}
