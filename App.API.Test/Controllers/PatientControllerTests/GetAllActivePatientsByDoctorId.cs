using App.Application.Core;
using App.Application.PatientOperations.Query.ActivePatientsByDoctorId;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.PatientControllerTests;

public class GetAllActivePatientsByDoctorId
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientController _patientcontroller;

    public GetAllActivePatientsByDoctorId()
    {
        _mediator = new Mock<IMediator>();
        _patientcontroller = new TestablePatientController();
        _patientcontroller.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetAllActivePatientsByDoctorId_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var expectedPatients = new List<PatientDto>
    {
        new PatientDto
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Gender = Gender.Male.ToString(),
            EthnicGroup = "Caucasian",
            OtherGroup = "Other",
            YearsInSchool = 1,
            OtherData = "Other data",
            OtherData2 = "Other data 2",
            OtherData3 = "Other data 3",
            Location = "Location",
            Age = 20
        },
        new PatientDto
        {
            Id = Guid.NewGuid(),
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            Gender = Gender.Female.ToString(),
            EthnicGroup = "Caucasian",
            OtherGroup = "Other",
            YearsInSchool = 1,
            OtherData = "Other data",
            OtherData2 = "Other data 2",
            OtherData3 = "Other data 3",
            Location = "Location",
            Age = 20
        }
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchAllActivePatientsByDoctorIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<PatientDto>>.Success(expectedPatients));

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
        var result = await _patientcontroller.FetchAllActivePatientsByDoctorId("John", "john.doe@example.com");

        // Assert
        var okResult = Assert.IsType<ActionResult<List<PatientDto>>>(result);
        var okObjectResult = Assert.IsType<OkObjectResult>(okResult.Result);
        var patients = Assert.IsAssignableFrom<List<PatientDto>>(okObjectResult.Value);
        Assert.Equal(expectedPatients.Count, patients.Count);
    }

    [Fact]
    public async Task FetchAllActivePatientsByDoctorId_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
        new Claim(ClaimTypes.NameIdentifier, "userId"),
        new Claim(ClaimTypes.Name, "username")
        }));

        _patientcontroller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        _mediator.Setup(x => x.Send(It.IsAny<FetchAllActivePatientsByDoctorIdQuery>(), default))
            .ReturnsAsync(OperationResult<List<PatientDto>>.Failure("Invalid data provided"));

        // Act
        var result = await _patientcontroller.FetchAllActivePatientsByDoctorId("John", "john.doe@example.com");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Invalid data provided", badRequestResult.Value);
    }
}

