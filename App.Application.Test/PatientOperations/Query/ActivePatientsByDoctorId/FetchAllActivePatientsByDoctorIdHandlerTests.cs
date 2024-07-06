using App.Application.PatientOperations.Query.ActivePatientsByDoctorId;
using App.Domain.DTOs.Common.Response;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.PatientOperations.Query.ActivePatientsByDoctorId;

public class FetchAllActivePatientsByDoctorIdHandlerTests : TestHelper
{
    private readonly FetchAllActivePatientsByDoctorIdHandler handler;
    private readonly FetchAllActivePatientsByDoctorIdQuery query;

    public FetchAllActivePatientsByDoctorIdHandlerTests()
    {
        handler = new FetchAllActivePatientsByDoctorIdHandler(patientRepositoryMock.Object);
        query = new FetchAllActivePatientsByDoctorIdQuery("doctor123", "test", "test@test.com");
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfPatientExaminationDtos()
    {
        // Arrange
        var patients = new List<PatientResponseDto>
            {
                new PatientResponseDto { Id = Guid.NewGuid(), FirstName = "Patient1", LastName = "test", Email = "test@test.com" },
                new PatientResponseDto { Id = Guid.NewGuid(), FirstName = "Patient2", LastName = "test", Email = "test@test.com" }
            }.AsQueryable().BuildMock();

        patientRepositoryMock.Setup(repo => repo.GetAllActivePatientsByDoctorId(query.DoctorId))
            .Returns(patients);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(2, result.ResultValue.Count);
        Assert.Equal("Patient1", result.ResultValue[0].FirstName);
        Assert.Equal("Patient2", result.ResultValue[1].FirstName);
        patientRepositoryMock.Verify(repo => repo.GetAllActivePatientsByDoctorId(query.DoctorId), Times.Once);
    }

}
