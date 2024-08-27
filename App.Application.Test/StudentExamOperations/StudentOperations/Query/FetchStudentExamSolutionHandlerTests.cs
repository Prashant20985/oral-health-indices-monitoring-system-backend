using App.Application.StudentExamOperations.StudentOperations.Query;
using App.Domain.DTOs.ExamDtos.Response;
using Moq;

namespace App.Application.Test.StudentExamOperations.StudentOperations.Query;

public class FetchStudentExamSolutionHandlerTests : TestHelper
{
    private readonly FetchStudentExamSolutionHandler handler;
    private readonly FetchStudentExamSolutionQuery query;

    public FetchStudentExamSolutionHandlerTests()
    {
        handler = new FetchStudentExamSolutionHandler(studentExamRepositoryMock.Object);
        query = new FetchStudentExamSolutionQuery(Guid.NewGuid(), "studentId");
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnExamDetails()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCardDto
        {
            Id = Guid.NewGuid()
        };

        studentExamRepositoryMock.Setup(x =>
                x.GetPracticePatientExaminationCardByExamIdAndStudentId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(practicePatientExaminationCard);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(practicePatientExaminationCard.Id, result.ResultValue.Id);
    }

    [Fact]
    public async Task Handle_WhenExamDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        studentExamRepositoryMock.Setup(x =>
                x.GetPracticePatientExaminationCardByExamIdAndStudentId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("Examination card not found", result.ErrorMessage);
    }
}