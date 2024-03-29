﻿using App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;
using App.Domain.DTOs;
using MockQueryable.Moq;

namespace App.Application.Test.DentistTeacherOperations.Query.PatientsNotInResearchGroups;

public class FetchPatientsNotInResearchGroupsHandlerTests : TestHelper
{
    private readonly FetchPatientsNotInResearchGroupsHandler handler;
    private readonly FetchPatientsNotInResearchGroupsQuery query;

    public FetchPatientsNotInResearchGroupsHandlerTests()
    {
        handler = new FetchPatientsNotInResearchGroupsHandler(researchGroupRepositoryMock.Object);
        query = new FetchPatientsNotInResearchGroupsQuery("Patient", "test@test.com");
    }

    [Fact]
    public async Task Handle_WithValidQuery_ShouldReturnListOfPatientDtos()
    {
        // Arrange
        var patients = new List<ResearchGroupPatientDto>
            {
               new ResearchGroupPatientDto
               {
                   Id = Guid.NewGuid(),
                   FirstName = "Patient",
                   LastName = "Patient1",
                   Email = "test@test.com"
               },
               new ResearchGroupPatientDto
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
        Assert.Equal(2, result.ResultValue.Count);
        Assert.Equal("Patient", result.ResultValue[0].FirstName);
        Assert.Equal("Patient1", result.ResultValue[0].LastName);
    }

    [Fact]
    public async Task Handle_WithNoPatientsFound_ShouldReturnEmptyResult()
    {
        // Arrange
        researchGroupRepositoryMock.Setup(repo => repo.GetAllPatientsNotInAnyResearchGroup())
            .Returns(new List<ResearchGroupPatientDto>().AsQueryable().BuildMockDbSet().Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result.ResultValue);
    }
}
