using App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using MockQueryable.Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.PatientsNotInResearchGroups;

public class FetchPatientsNotInResearchGroupsHandlerTests : TestHelper
{
    private readonly FetchPatientsNotInResearchGroupsHandler handler;
    private readonly FetchPatientsNotInResearchGroupsQuery query;

    public FetchPatientsNotInResearchGroupsHandlerTests()
    {
        handler = new FetchPatientsNotInResearchGroupsHandler(researchGroupRepositoryMock.Object);
        query = new FetchPatientsNotInResearchGroupsQuery("Patient", "test@test.com", 0, 20);
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfPatientDtos()
    {
        // Arrange
        var patients = new List<ResearchGroupPatientResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Patient",
                LastName = "Patient1",
                Email = "test@test.com"
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Patient",
                LastName = "Patient1",
                Email = "test@test.com"
            }
        }.AsQueryable().BuildMockDbSet();

        researchGroupRepositoryMock.Setup(repo => repo.GetAllPatientsNotInAnyResearchGroup())
            .Returns(patients.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.ResultValue);
        Assert.Equal(2, result.ResultValue.TotalNumberOfPatients);
        Assert.Equal("Patient", result.ResultValue.Patients[0].FirstName);
        Assert.Equal("Patient1", result.ResultValue.Patients[0].LastName);
    }

    [Fact]
    public async Task Handle_WithNoPatientsFound_ShouldReturnEmptyResult()
    {
        // Arrange
        researchGroupRepositoryMock.Setup(repo => repo.GetAllPatientsNotInAnyResearchGroup())
            .Returns(new List<ResearchGroupPatientResponseDto>().AsQueryable().BuildMockDbSet().Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result.ResultValue.Patients);
    }
}