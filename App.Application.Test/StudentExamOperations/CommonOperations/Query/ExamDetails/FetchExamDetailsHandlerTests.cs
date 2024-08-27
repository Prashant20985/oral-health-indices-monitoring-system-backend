using App.Application.StudentExamOperations.CommonOperations.Query.ExamDetails;
using App.Domain.DTOs.ExamDtos.Response;
using Moq;

namespace App.Application.Test.StudentExamOperations.CommonOperations.Query.ExamDetails;

public class FetchExamDetailsHandlerTests : TestHelper
{
    private readonly FetchExamDetailsHandler handler;
    private readonly FetchExamDetailsQuery query;

    public FetchExamDetailsHandlerTests()
    {
        handler = new FetchExamDetailsHandler(studentExamRepositoryMock.Object);
        query = new FetchExamDetailsQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnExamDetails()
    {
        // Arrange
        var examDetails = new ExamDto
        {
            Id = Guid.NewGuid(),
            DateOfExamination = DateTime.Now
        };

        studentExamRepositoryMock.Setup(x => x.GetExamDtoById(It.IsAny<Guid>()))
            .ReturnsAsync(examDetails);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(examDetails.Id, result.ResultValue.Id);
        Assert.Equal(examDetails.DateOfExamination, result.ResultValue.DateOfExamination);
    }

    [Fact]
    public async Task Handle_WhenExamDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetExamDtoById(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("Exam not found", result.ErrorMessage);
    }
}
