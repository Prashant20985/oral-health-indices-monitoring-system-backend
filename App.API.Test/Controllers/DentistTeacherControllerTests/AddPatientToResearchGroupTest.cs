using App.Application.Core;
using App.Application.DentistTeacherOperations.Command.AddPatientToResearchGroup;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.DentistTeacherControllerTests;

public class AddPatientToResearchGroupTest
{
    private readonly TestableDentistTeacherController _dentistTeacherController;
    private readonly Mock<IMediator> _mediatorMock;

    public AddPatientToResearchGroupTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _dentistTeacherController = new TestableDentistTeacherController();
        _dentistTeacherController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task AddPatientToResearchGroup_Returns_OkResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var researchGroup = new ResearchGroup("Group 1", "Description 1", "doctor");
        var patientId = Guid.NewGuid();

        _mediatorMock.Setup(x => x.Send(It.IsAny<AddPatientToResearchGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.AddPatientToResearchGroup(researchGroup.Id, patientId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task AddPatientToResearchGroup_Returns_BadRequestResult()
    {
        // Arrange
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Dentist_Teacher_Researcher")
        }));

        var researchGroup = new ResearchGroup("Group 1", "Description 1", "doctor");
        var patientId = Guid.NewGuid();

        _mediatorMock.Setup(x => x.Send(It.IsAny<AddPatientToResearchGroupCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        _dentistTeacherController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        // Act
        var result = await _dentistTeacherController.AddPatientToResearchGroup(researchGroup.Id, patientId);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
