using App.Application.Core;
using App.Application.PatientOperations.Query.ArchivedPatients;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientControllerTests;

public class GetAllArchivedPatientsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientController _patientcontroller;

    public GetAllArchivedPatientsTest()
    {
        _mediator = new Mock<IMediator>();
        _patientcontroller = new TestablePatientController();
        _patientcontroller.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetAllArchivedPatients_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var expectedPatients = new List<PatientResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example",
                Gender = Gender.Male.ToString(),
                EthnicGroup = "Caucasian",
                OtherGroup = "Other",
                YearsInSchool = 1,
                OtherData = "Other data",
                OtherData2 = "Other data 2",
                OtherData3 = "Other data 3",
                Location = "Location",
                Age = 20,
                IsArchived = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example",
                Gender = Gender.Female.ToString(),
                EthnicGroup = "Caucasian",
                OtherGroup = "Other",
                YearsInSchool = 1,
                OtherData = "Other data",
                OtherData2 = "Other data 2",
                OtherData3 = "Other data 3",
                Location = "Location",
                Age = 20,
                IsArchived = true
            }
        };

        var paginatedPatientResponseDto = new PaginatedPatientResponseDto
        {
            Patients = expectedPatients,
            TotalPatientsCount = expectedPatients.Count
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchAllArchivedPatientsQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedPatientResponseDto>
                .Success(paginatedPatientResponseDto));

        // Act
        var result = await _patientcontroller.FetchAllArchivedPatients("John", "john.doe@example");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var patients = Assert.IsAssignableFrom<PaginatedPatientResponseDto>(okResult.Value);
        Assert.Equal(expectedPatients.Count, patients.Patients.Count);
    }

    [Fact]
    public async Task GetAllArchivedPatients_WhenFetchFails_ShouldReturnBadRequest()
    {
        // Arrange
        _mediator.Setup(x => x.Send(It.IsAny<FetchAllArchivedPatientsQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedPatientResponseDto>
                .Failure("Failed to fetch archived patients"));

        // Act
        var result = await _patientcontroller.FetchAllArchivedPatients("John", "john.doe@example");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}