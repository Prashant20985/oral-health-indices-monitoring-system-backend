using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;
using App.Domain.Models.Common.RiskFactorAssessment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class UpdateRiskFactorAssessmentTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public UpdateRiskFactorAssessmentTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UpdateRiskFactorAssessment_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var APIForm = new Domain.Models.OralHealthExamination.API();
        var beweForm = new Domain.Models.OralHealthExamination.Bewe();
        var dMFT_dMFSForm = new Domain.Models.OralHealthExamination.DMFT_DMFS();
        var bleedingForm = new Domain.Models.OralHealthExamination.Bleeding();
        var riskFactorAssessmentForm = new Domain.Models.OralHealthExamination.RiskFactorAssessment();
        var patientExaminationResult = new Domain.Models.OralHealthExamination.PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
        {
            API = APIForm,
            Bewe = beweForm,
            DMFT_DMFS = dMFT_dMFSForm,
            Bleeding = bleedingForm
        };

        var patientExaminationCard = new Domain.Models.OralHealthExamination.PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult,
            RiskFactorAssessment = riskFactorAssessmentForm
        };

        patientExaminationCard.SetRiskFactorAssesmentId(riskFactorAssessmentForm.Id);

        var assessmentModel = new RiskFactorAssessmentModel();

        _mediator.Setup(x => x.Send(It.IsAny<UpdateRiskFactorAssessmentFormCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _patientExaminationCardController.UpdateRiskFactorAssessment(patientExaminationCard.Id, assessmentModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, (result as OkObjectResult)?.StatusCode);
    }

    [Fact]
    public async Task UpdateRiskFactorAssessment_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var APIForm = new Domain.Models.OralHealthExamination.API();
        var beweForm = new Domain.Models.OralHealthExamination.Bewe();
        var dMFT_dMFSForm = new Domain.Models.OralHealthExamination.DMFT_DMFS();
        var bleedingForm = new Domain.Models.OralHealthExamination.Bleeding();
        var riskFactorAssessmentForm = new Domain.Models.OralHealthExamination.RiskFactorAssessment();
        var patientExaminationResult = new Domain.Models.OralHealthExamination.PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
        {
            API = APIForm,
            Bewe = beweForm,
            DMFT_DMFS = dMFT_dMFSForm,
            Bleeding = bleedingForm
        };

        var patientExaminationCard = new Domain.Models.OralHealthExamination.PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult,
            RiskFactorAssessment = riskFactorAssessmentForm
        };

        patientExaminationCard.SetRiskFactorAssesmentId(riskFactorAssessmentForm.Id);

        var assessmentModel = new RiskFactorAssessmentModel();

        _mediator.Setup(x => x.Send(It.IsAny<UpdateRiskFactorAssessmentFormCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Risk Factor Assessment Form Not Found"));

        // Act
        var result = await _patientExaminationCardController.UpdateRiskFactorAssessment(Guid.NewGuid(), assessmentModel);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, (result as BadRequestObjectResult)?.StatusCode);
    }
}
