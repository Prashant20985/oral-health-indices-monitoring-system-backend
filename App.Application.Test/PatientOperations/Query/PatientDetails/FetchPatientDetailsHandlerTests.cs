using App.Application.PatientOperations.Query.PatientDetails;
using App.Domain.DTOs.Common.Response;
using Moq;

namespace App.Application.Test.PatientOperations.Query.PatientDetails;

public class FetchPatientDetailsHandlerTests : TestHelper
{
    private readonly FetchPatientDetailsHandler handler;
    private readonly FetchPatientDetailsQuery query;

    public FetchPatientDetailsHandlerTests()
    {
        handler = new FetchPatientDetailsHandler(patientRepositoryMock.Object);
        query = new FetchPatientDetailsQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldFetchPatientDetails()
    {
        // Arrange
        var patient = new PatientResponseDto
        {
            Id = Guid.NewGuid(),
            FirstName = "test",
            LastName = "test",
            Email = "test@test.com"
        };

        patientRepositoryMock.Setup(repo => repo.GetPatientDetails(It.IsAny<Guid>()))
            .ReturnsAsync(patient);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        var fetchedPatient = Assert.IsType<PatientResponseDto>(result.ResultValue);
        Assert.Equal(patient.Id, fetchedPatient.Id);
        Assert.Equal(patient.FirstName, fetchedPatient.FirstName);
        Assert.Equal(patient.LastName, fetchedPatient.LastName);
        Assert.Equal(patient.Email, fetchedPatient.Email);
    }

    [Fact]
    public async Task Handle_WithInvalidQuery_ShouldReturnFailure()
    {
        // Arrange
        patientRepositoryMock.Setup(repo => repo.GetPatientDetails(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient Not Found", result.ErrorMessage);
    }
}