using App.Application.Core;
using App.Application.PatientOperations.Query.ArchivedPatientsByDoctorId;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.PatientControllerTests;

public class GetAllArchivedPatientsByDoctorIdTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientController _patientcontroller;

    public GetAllArchivedPatientsByDoctorIdTest()
    {
        _mediator = new Mock<IMediator>();
        _patientcontroller = new TestablePatientController();
        _patientcontroller.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetAllArchivedPatientsByDoctorId_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var expectedPatients = new List<PatientDto>
        {
        new PatientDto {
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
        new PatientDto {
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

        _mediator.Setup(x => x.Send(It.IsAny<FetchAllArchivedPatientsByDoctorIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<PatientDto>>
                       .Success(expectedPatients));

        _patientcontroller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, "DoctorUserId"),
                    new Claim(ClaimTypes.Name, "DoctorUserName"),
                new Claim(ClaimTypes.Role, "Dentist_Teacher_Researcher")
            }))
            }
        };

        // Act
        var result = await _patientcontroller.FetchAllArchivedPatientsByDoctorId("John", "john.doe@example.com");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var patients = Assert.IsType<List<PatientDto>>(okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(expectedPatients.Count, patients.Count);
        Assert.Equal(expectedPatients.First().Id, patients.First().Id);
        Assert.Equal(expectedPatients.Last().Id, patients.Last().Id);
    }

    [Fact]
    public async Task GetAllArchivedPatientsByDoctorId_WhenFetchFails_ShouldReturnBadRequest()
    {
        // Arrange
        _mediator.Setup(x => x.Send(It.IsAny<FetchAllArchivedPatientsByDoctorIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<PatientDto>>
                                  .Failure("Failed to fetch archived patients"));

        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
        new Claim(ClaimTypes.NameIdentifier, "userId"),
        new Claim(ClaimTypes.Name, "username")
        }));

        _patientcontroller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _patientcontroller.FetchAllArchivedPatientsByDoctorId("John", "john.doe@example.com");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Failed to fetch archived patients", badRequestResult.Value);
    }
}
