using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardInTestMode;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Query.FetchPatientExaminationCardInTestMode;

public class FetchPatientExaminationCardsInTestModeHandlerTests : TestHelper
{
    private readonly FetchPatientExaminationCardsInTestModeHandler handler;
    private readonly FetchPatientExaminationCardsInTestModeQuery query;

    public FetchPatientExaminationCardsInTestModeHandlerTests()
    {
        handler = new FetchPatientExaminationCardsInTestModeHandler(patientExaminationCardRepositoryMock.Object, patientRepositoryMock.Object);
        query = new FetchPatientExaminationCardsInTestModeQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WhenPatientDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        patientRepositoryMock.Setup(x => x.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("Patient Not Found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenPatientExists_ShouldReturnPatientExaminationCards()
    {
        // Arrange
        var patient = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 18, "test", "test", "test", "test", 1, "doctorId");
        var patientExaminationCard = new PatientExaminationCardDto
        {
            Id = Guid.NewGuid()
        };

        patientRepositoryMock.Setup(x => x.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(patient);

        patientExaminationCardRepositoryMock.Setup(x => x.GetPatientExminationCardDtosInTestModeByPatientId(It.IsAny<Guid>()))
            .ReturnsAsync(new List<PatientExaminationCardDto> { patientExaminationCard });

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(patientExaminationCard.Id, result.ResultValue.First().Id);
        Assert.IsType<List<PatientExaminationCardDto>>(result.ResultValue);
    }
}
