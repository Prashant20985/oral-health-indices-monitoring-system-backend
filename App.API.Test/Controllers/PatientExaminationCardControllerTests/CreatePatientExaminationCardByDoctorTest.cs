using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByDoctor;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.Common.RiskFactorAssessment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class CreatePatientExaminationCardByDoctorTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public CreatePatientExaminationCardByDoctorTest()
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
    public async Task CreatePatientExaminationCardByDoctor_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();

        var summary = new SummaryRequestDto
        {
            PatientRecommendations = "Test Patient Recommendations",
            NeedForDentalInterventions = "1",
            ProposedTreatment = "Test Proposed Treatment",
            Description = "Test Description"
        };

        var riskFactorAssessmentModel = new RiskFactorAssessmentModel { };
        var DMFT_DMFS = new CreateDMFT_DMFSRequestDto { };
        var API = new CreateAPIRequestDto { };
        var bleeding = new CreateBleedingRequestDto { };
        var bewe = new CreateBeweRequestDto { };
        var patientExaminationCardCommment = "Test comment";

        var inputParams = new CreatePatientExaminationCardByDoctorInputParams
            (patientExaminationCardCommment,
            summary,
            riskFactorAssessmentModel,
            DMFT_DMFS,
            bewe,
            API,
            bleeding);

        var expectedResponse = new PatientExaminationCardDto
        {
            Id = Guid.NewGuid(),
            DoctorName = "Test Doctor",
            StudentName = "Test Student",
            PatientName = "Test Patient",
            DoctorComment = "Test Doctor Comment",
            StudentComment = "Test Student Comment",
            IsRegularMode = true,
            TotalScore = 100,
            DateOfExamination = DateTime.Now,
            Summary = new SummaryResponseDto
            {
                PatientRecommendations = "Test Patient Recommendations",
                NeedForDentalInterventions = "1",
                ProposedTreatment = "Test Proposed Treatment",
                Description = "Test Description"
            },
            RiskFactorAssessment = new RiskFactorAssessmentDto
            {

            }
        };

        _mediator.Setup(x => x.Send(It.IsAny<CreatePatientExaminationCardByDoctorCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<PatientExaminationCardDto>.Success(expectedResponse));

        // Act
        var result = await _patientExaminationCardController.CreatePatientExaminationCardByDoctor(patientId, inputParams);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        var returnValue = Assert.IsType<PatientExaminationCardDto>(okResult.Value);
        Assert.Equal(expectedResponse, returnValue);
    }
}
