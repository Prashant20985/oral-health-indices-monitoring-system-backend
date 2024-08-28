using App.Application.Core;
using App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class GetPatientsNotInResearchGroupTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public GetPatientsNotInResearchGroupTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetPatientsNotInResearchGroup_Returns_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var expectedPatients = new List<ResearchGroupPatientResponseDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@test.com"
            },
             new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@test.com"
            }
        };

        var patientsNotInResearchGroup = new PaginatedResearchGroupPatientResponseDto
        {
            Patients = expectedPatients,
            TotalNumberOfPatients = expectedPatients.Count
        };

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchPatientsNotInResearchGroupsQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedResearchGroupPatientResponseDto>.Success(patientsNotInResearchGroup));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetPatientsNotInResearchGroup("John", "john.doe@test.com");

        // Assert
        Assert.IsType<ActionResult<PaginatedResearchGroupPatientResponseDto>>(result);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedPatients = Assert.IsAssignableFrom<PaginatedResearchGroupPatientResponseDto>(okResult.Value);
        Assert.Equal(expectedPatients, returnedPatients.Patients);
    }

    [Fact]
    public async Task GetPatientsNotInResearchGroup_WhenFetchFails_Returns_BadRequest()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        _mediatorMock.Setup(x => x.Send(It.IsAny<FetchPatientsNotInResearchGroupsQuery>(), default))
            .ReturnsAsync(OperationResult<PaginatedResearchGroupPatientResponseDto>.Failure("Error"));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.GetPatientsNotInResearchGroup("John", "John.doe@test.com");

        // Assert
        Assert.IsType<ActionResult<PaginatedResearchGroupPatientResponseDto>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("Error", badRequestResult.Value);
    }
}
