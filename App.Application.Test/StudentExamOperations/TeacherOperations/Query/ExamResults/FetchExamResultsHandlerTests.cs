using App.Application.StudentExamOperations.TeacherOperations.Query.ExamResults;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Models.CreditSchema;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Query.ExamResults;

public class FetchExamResultsHandlerTests : TestHelper
{
    private readonly FetchExamResultsHandler _handler;
    private readonly FetchExamResultsQuery _query;

    public FetchExamResultsHandlerTests()
    {
        _handler = new FetchExamResultsHandler(studentExamRepositoryMock.Object);
        _query = new FetchExamResultsQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnExamDetails()
    {
        // Arrange
        var exam = new Exam(DateTime.Now, "test", "test", TimeOnly.MinValue, TimeOnly.MinValue, TimeSpan.MaxValue, 110, Guid.NewGuid());

        var examResults = new List<StudentExamResultResponseDto>
        {
            new StudentExamResultResponseDto
            {
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                Email = "test@test.com",
                StudentMark = 100
            },
            new StudentExamResultResponseDto
            {
                UserName = "test2",
                FirstName = "test2",
                LastName = "test2",
                Email = "test2@test.com",
                StudentMark = 90
            }
        };

        studentExamRepositoryMock.Setup(x => x.GetExamById(It.IsAny<Guid>()))
            .ReturnsAsync(exam);

        studentExamRepositoryMock.Setup(x => x.GetStudentExamResults(It.IsAny<Guid>()))
            .ReturnsAsync(examResults);

        // Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(examResults.Count, result.ResultValue.Count);
    }

    [Fact]
    public async Task Handle_ExamNotFound_ReturnsFailureResult()
    {
        // Arrange
        var examId = Guid.NewGuid();

        studentExamRepositoryMock
            .Setup(repo => repo.GetExamById(examId))
            .ReturnsAsync(value:null);


        // Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Exam not found.", result.ErrorMessage);
    }

}
