using App.Application.PatientOperations.Query.ActivePatients;
using App.Domain.DTOs.Common.Response;
using MockQueryable.Moq;
using Moq;

namespace App.Application.Test.PatientOperations.Query.ActivePatients;

public class FetchAllActivePatientsHandlerTests : TestHelper
{
    private readonly FetchAllActivePatientsHandler handler;
    private readonly FetchAllActivePatientsQuery query;

    public FetchAllActivePatientsHandlerTests()
    {
        handler = new FetchAllActivePatientsHandler(patientRepositoryMock.Object);
        query = new FetchAllActivePatientsQuery("test", "test@test.com");
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldFetchActivePatients()
    {
        // Arrange
        var activePatients = new List<PatientDto>
            {
                new PatientDto { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Email = "test@test.com" },
                new PatientDto { Id = Guid.NewGuid(), FirstName = "test", LastName = "test", Email = "test@test.com" }
            }.AsQueryable().BuildMock();

        patientRepositoryMock.Setup(repo => repo.GetAllActivePatients())
            .Returns(activePatients);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        var fetchedPatients = Assert.IsType<List<PatientDto>>(result.ResultValue);
        Assert.Equal(activePatients.Count(), fetchedPatients.Count);
        patientRepositoryMock.Verify(repo => repo.GetAllActivePatients(), Times.Once);
    }
}