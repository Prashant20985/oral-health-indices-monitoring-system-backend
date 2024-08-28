using App.Application.StudentExamOperations.TeacherOperations.Query.ExaminationCardDetails;
using App.Domain.DTOs.ExamDtos.Response;
using Moq;

namespace App.Application.Test.StudentExamOperations.TeacherOperations.Query.ExaminationCardDetails;

public class FetchPracticePatientExaminationCardDetailsHandlerTests : TestHelper
{
    private readonly FetchPracticePatientExaminationCardDetailsHandler handler;
    private readonly FetchPracticePatientExaminationCardDetailsQuery query;

    public FetchPracticePatientExaminationCardDetailsHandlerTests()
    {
        handler = new FetchPracticePatientExaminationCardDetailsHandler(studentExamRepositoryMock.Object);
        query = new FetchPracticePatientExaminationCardDetailsQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WhenExamExists_ShouldReturnExamDetails()
    {
        // Arrange
        var practicePatientExaminationCard = new PracticePatientExaminationCardDto
        {
            Id = Guid.NewGuid()
        };

        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardDtoById(It.IsAny<Guid>()))
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
        studentExamRepositoryMock.Setup(x => x.GetPracticePatientExaminationCardDtoById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("Examination Card not found", result.ErrorMessage);
    }
}