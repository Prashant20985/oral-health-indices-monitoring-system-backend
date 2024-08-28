using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardDetails;
using App.Domain.DTOs.PatientDtos.Response;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Query.FetchPatientExaminationCardDetails;

public class FetchPatientExaminationCardDetailsHandlerTests : TestHelper
{
    private readonly FetchPatientExaminationCardDetailsHandler handler;
    private readonly FetchPatientExaminationCardDetailsQuery query;

    public FetchPatientExaminationCardDetailsHandlerTests()
    {
        handler = new FetchPatientExaminationCardDetailsHandler(patientExaminationCardRepositoryMock.Object);
        query = new FetchPatientExaminationCardDetailsQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WhenPatientExaminationCardExists_ShouldReturnPatientExaminationCardDetails()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCardDto
        {
            Id = Guid.NewGuid()
        };

        patientExaminationCardRepositoryMock.Setup(x => x.GetPatientExaminationCardDto(It.IsAny<Guid>()))
            .ReturnsAsync(patientExaminationCard);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(patientExaminationCard.Id, result.ResultValue.Id);
    }

    [Fact]
    public async Task Handle_WhenPatientExaminationCardDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetPatientExaminationCardDto(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Null(result.ResultValue);
        Assert.Equal("Examination Card Not Found", result.ErrorMessage);
    }
}