using App.Application.PatientOperations.Query.ArchivedPatients;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.PatientOperations.Query.ArchivedPatients;

public class FetchAllArchivedPatientsHandlerTests : TestHelper
{
    private readonly FetchAllArchivedPatientsHandler handler;
    private readonly FetchAllArchivedPatientsQuery query;

    public FetchAllArchivedPatientsHandlerTests()
    {
        handler = new FetchAllArchivedPatientsHandler(patientRepositoryMock.Object);
        query = new FetchAllArchivedPatientsQuery("test", "test@test.com", 0, 20);
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldFetchArchivedPatients()
    {
        // Arrange
        var archivedPatients = new List<PatientResponseDto>
        {
                new PatientResponseDto { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Email = "test@test.com"},
                new PatientResponseDto { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Email = "test@test.com"}
        }.AsQueryable().BuildMock();

        patientRepositoryMock.Setup(repo => repo.GetAllArchivedPatients())
            .Returns(archivedPatients);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        var fetchedPatients = Assert.IsType<PaginatedPatientResponseDto>(result.ResultValue);
        Assert.Equal(archivedPatients.Count(), fetchedPatients.Patients.Count);
        patientRepositoryMock.Verify(repo => repo.GetAllArchivedPatients(), Times.Once);
    }

}
