using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;
using App.Domain.Models.Common.Bewe;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class UpdateBeweFormTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public UpdateBeweFormTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UpdateBeweForm_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var APIForm = new Domain.Models.OralHealthExamination.API();
        var beweForm = new Domain.Models.OralHealthExamination.Bewe();
        var dMFT_dMFSForm = new Domain.Models.OralHealthExamination.DMFT_DMFS();
        var bleedingForm = new Domain.Models.OralHealthExamination.Bleeding();
        var patientExaminationResult = new Domain.Models.OralHealthExamination.PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
        {
            API = APIForm,
            Bewe = beweForm,
            DMFT_DMFS = dMFT_dMFSForm,
            Bleeding = bleedingForm
        };

        var patientExaminationCard = new Domain.Models.OralHealthExamination.PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult
        };

        var assessmentModel = new BeweAssessmentModel();

        var expectedResponse = 123.45m;

        _mediator.Setup(x => x.Send(It.IsAny<UpdateBeweFormCommand>(), default))
            .ReturnsAsync(OperationResult<decimal>.Success(expectedResponse));

        // Act
        var result = await _patientExaminationCardController.UpdateBeweForm(patientExaminationCard.Id, assessmentModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(expectedResponse, okResult.Value);
    }

    [Fact]
    public async Task UpdateBeweForm_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var APIForm = new Domain.Models.OralHealthExamination.API();
        var beweForm = new Domain.Models.OralHealthExamination.Bewe();
        var dMFT_dMFSForm = new Domain.Models.OralHealthExamination.DMFT_DMFS();
        var bleedingForm = new Domain.Models.OralHealthExamination.Bleeding();
        var patientExaminationResult = new Domain.Models.OralHealthExamination.PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
        {
            API = APIForm,
            Bewe = beweForm,
            DMFT_DMFS = dMFT_dMFSForm,
            Bleeding = bleedingForm
        };

        var patientExaminationCard = new Domain.Models.OralHealthExamination.PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult
        };

        var assessmentModel = new BeweAssessmentModel();

        _mediator.Setup(x => x.Send(It.IsAny<UpdateBeweFormCommand>(), default))
            .ReturnsAsync(OperationResult<decimal>.Failure("Bewe Form Not Found"));

        // Act
        var result = await _patientExaminationCardController.UpdateBeweForm(patientExaminationCard.Id, assessmentModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("Bewe Form Not Found", badRequestResult.Value);
    }
}
