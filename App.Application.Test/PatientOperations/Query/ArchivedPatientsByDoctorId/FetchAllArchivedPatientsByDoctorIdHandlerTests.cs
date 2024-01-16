using App.Application.PatientOperations.Query.ArchivedPatientsByDoctorId;
using App.Domain.DTOs.PatientDtos.Response;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.PatientOperations.Query.ArchivedPatientsByDoctorId;

public class FetchAllArchivedPatientsByDoctorIdHandlerTests : TestHelper
{
    private readonly FetchAllArchivedPatientsByDoctorIdHandler handler;
    private readonly FetchAllArchivedPatientsByDoctorIdQuery query;

    public FetchAllArchivedPatientsByDoctorIdHandlerTests()
    {
        handler = new FetchAllArchivedPatientsByDoctorIdHandler(patientRepositoryMock.Object);
        query = new FetchAllArchivedPatientsByDoctorIdQuery("doctor123", "test", "test@test.com");
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfPatientExaminationDtos()
    {
        // Arrange
        var patients = new List<PatientExaminationDto>
        {
                new PatientExaminationDto { Id = Guid.NewGuid(),FirstName = "Patient1", LastName = "test", Email = "test@test.com"},
                new PatientExaminationDto { Id = Guid.NewGuid(), FirstName = "Patient2", LastName = "test", Email = "test@test.com"}
        }.AsQueryable().BuildMock();

        patientRepositoryMock.Setup(repo => repo.GetAllArchivedPatientsByDoctorId(query.DoctorId))
            .Returns(patients);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(2, result.ResultValue.Count);
        Assert.Equal("Patient1", result.ResultValue[0].FirstName);
        Assert.Equal("Patient2", result.ResultValue[1].FirstName);
        patientRepositoryMock.Verify(repo => repo.GetAllArchivedPatientsByDoctorId(query.DoctorId), Times.Once);
    }
}
