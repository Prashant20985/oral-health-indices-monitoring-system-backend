using App.Application.StudentExamOperations.CommonOperations.Query.ExamsList;
using App.Domain.DTOs.ExamDtos.Response;
using Moq;

namespace App.Application.Test.StudentExamOperations.CommonOperations.Query.ExamsList;

public class FetchExamsListByGroupIdHandlerTests : TestHelper
{
    private readonly FetchExamsListByGroupIdHandler handler;
    private readonly FetchExamsListByGroupIdQuery query;

    public FetchExamsListByGroupIdHandlerTests()
    {
        handler = new FetchExamsListByGroupIdHandler(studentExamRepositoryMock.Object);
        query = new FetchExamsListByGroupIdQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WhenExamsExist_ShouldReturnExamsList()
    {
        // Arrange
        var examsList = new List<ExamDto>
        {
            new ExamDto
            {
                Id = Guid.NewGuid(),
                DateOfExamination = DateTime.Now
            },
            new ExamDto
            {
                Id = Guid.NewGuid(),
                DateOfExamination = DateTime.Now
            }
        };

        studentExamRepositoryMock.Setup(x => x.GetExamDtosByGroupId(It.IsAny<Guid>()))
            .ReturnsAsync(examsList);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(examsList.Count, result.ResultValue.Count);
        Assert.Equal(examsList[0].Id, result.ResultValue[0].Id);
        Assert.Equal(examsList[0].DateOfExamination, result.ResultValue[0].DateOfExamination);
        Assert.Equal(examsList[1].Id, result.ResultValue[1].Id);
        Assert.Equal(examsList[1].DateOfExamination, result.ResultValue[1].DateOfExamination);
    }

    [Fact]
    public async Task Handle_WithInvalidQuery_ShouldReturnFailure()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetExamDtosByGroupId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ResultValue);
    }
}
