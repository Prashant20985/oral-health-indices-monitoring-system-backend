using App.Application.PatientOperations.Query.ArchivedPatients;
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
        query = new FetchAllArchivedPatientsQuery("test", "test@test.com");
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldFetchArchivedPatients()
    {
        // Arrange
        var archivedPatients = new List<PatientDto>
        {
                new PatientDto { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Email = "test@test.com"},
                new PatientDto { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Email = "test@test.com"}
        }.AsQueryable().BuildMock();

        patientRepositoryMock.Setup(repo => repo.GetAllArchivedPatients())
            .Returns(archivedPatients);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        var fetchedPatients = Assert.IsType<List<PatientDto>>(result.ResultValue);
        Assert.Equal(archivedPatients.Count(), fetchedPatients.Count);
        patientRepositoryMock.Verify(repo => repo.GetAllArchivedPatients(), Times.Once);
    }

}
