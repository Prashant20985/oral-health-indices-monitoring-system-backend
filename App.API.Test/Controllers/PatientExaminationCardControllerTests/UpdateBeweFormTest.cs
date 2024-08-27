using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Common.Bewe;
using App.Domain.Models.OralHealthExamination;
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
        var beweForm = new Bewe();
        var dMFT_dMFSForm = new DMFT_DMFS();
        var bleedingForm = new Bleeding();
        var patientExaminationResult =
            new PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
            {
                API = APIForm,
                Bewe = beweForm,
                DMFT_DMFS = dMFT_dMFSForm,
                Bleeding = bleedingForm
            };

        var patientExaminationCard = new PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult
        };

        var assessmentModel = new BeweAssessmentModel();

        var expectedResponse = new BeweResultResponseDto
        {
            BeweResult = 1.0m,
            Sectant1 = 1.0m,
            Sectant2 = 1.0m,
            Sectant3 = 1.0m,
            Sectant4 = 1.0m,
            Sectant5 = 1.0m,
            Sectant6 = 1.0m
        };

        _mediator.Setup(x => x.Send(It.IsAny<UpdateBeweFormCommand>(), default))
            .ReturnsAsync(OperationResult<BeweResultResponseDto>.Success(expectedResponse));

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
        var beweForm = new Bewe();
        var dMFT_dMFSForm = new DMFT_DMFS();
        var bleedingForm = new Bleeding();
        var patientExaminationResult =
            new PatientExaminationResult(beweForm.Id, dMFT_dMFSForm.Id, APIForm.Id, bleedingForm.Id)
            {
                API = APIForm,
                Bewe = beweForm,
                DMFT_DMFS = dMFT_dMFSForm,
                Bleeding = bleedingForm
            };

        var patientExaminationCard = new PatientExaminationCard(patientId)
        {
            PatientExaminationResult = patientExaminationResult
        };

        var assessmentModel = new BeweAssessmentModel();

        _mediator.Setup(x => x.Send(It.IsAny<UpdateBeweFormCommand>(), default))
            .ReturnsAsync(OperationResult<BeweResultResponseDto>.Failure("Bewe Form Not Found"));

        // Act
        var result = await _patientExaminationCardController.UpdateBeweForm(patientExaminationCard.Id, assessmentModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("Bewe Form Not Found", badRequestResult.Value);
    }
}