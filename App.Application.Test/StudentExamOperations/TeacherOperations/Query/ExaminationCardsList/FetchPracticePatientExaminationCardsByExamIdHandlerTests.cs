using App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardsList;
using App.Domain.DTOs.ExamDtos.Response;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Query.ExaminationCardsList;

public class FetchPracticePatientExaminationCardsByExamIdHandlerTests : TestHelper
{
    private readonly FetchPracticePatientExaminationCardsByExamIdHandler _handler;
    private readonly FetchPracticePatientExaminationCardsByExamIdQuery _query;

    public FetchPracticePatientExaminationCardsByExamIdHandlerTests()
    {
        _handler = new FetchPracticePatientExaminationCardsByExamIdHandler(studentExamRepositoryMock.Object);
        _query = new FetchPracticePatientExaminationCardsByExamIdQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnExamDetails()
    {
        // Arrange
        var practicePatientExaminationCards = new List<PracticePatientExaminationCardDto>
        {
            new()
            {
                Id = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid()
            }
        };

        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardsByExamId(It.IsAny<Guid>()))
            .ReturnsAsync(practicePatientExaminationCards);

        // Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(practicePatientExaminationCards.Count, result.ResultValue.Count);
    }

    [Fact]
    public async Task Handle_WhenExamDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardsByExamId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ResultValue);
    }
}