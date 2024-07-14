using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardsAssignedToDoctor;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class GetPatientExaminationCardsAssignedToDoctorTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public GetPatientExaminationCardsAssignedToDoctorTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
           {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Dentist_Teacher_Researcher")
           }, "mock"));

        _patientExaminationCardController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [Fact]
    public async Task GetPatientExaminationCardsAssignedToDoctor_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var doctorId = Guid.NewGuid().ToString();
        var studentId = Guid.NewGuid().ToString();
        var year = 2023;
        var month = 7;

        var patient = new PatientResponseDto
        {
            Id= Guid.NewGuid()
        };

        var patient2 = new PatientResponseDto
        {
            Id = Guid.NewGuid()
        };

        var patientExaminationCardDtos = new List<PatientDetailsWithExaminationCards>
        {
            new PatientDetailsWithExaminationCards { Patient = patient },
            new PatientDetailsWithExaminationCards { Patient = patient2 }
        };

        var expectedResponse = new List<PatientDetailsWithExaminationCards>
        {
            new PatientDetailsWithExaminationCards {},
            new PatientDetailsWithExaminationCards {}
        };


        _mediator.Setup(x => x.Send(It.IsAny<FetchPatientExaminationCardsAssignedToDoctorQuery>(), default))
            .ReturnsAsync(OperationResult<List<PatientDetailsWithExaminationCards>>.Success(patientExaminationCardDtos));

        // Act
        var result = await _patientExaminationCardController.GetPatientExaminationCardsAssignedToDoctor(studentId,year,month);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<PatientDetailsWithExaminationCards>>(okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}
