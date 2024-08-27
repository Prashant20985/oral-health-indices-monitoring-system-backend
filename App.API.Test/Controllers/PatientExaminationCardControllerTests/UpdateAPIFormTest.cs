using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateAPIForm;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class UpdateAPIFormTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public UpdateAPIFormTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UpdateAPIForm_WithValidData_ShouldReturnOk()
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

        var assessmentModel = new APIBleedingAssessmentModel();

        var exepctedResponse = new APIResultResponseDto
        {
            APIResult = 1,
            Maxilla = 1,
            Mandible = 1
        };


        _mediator.Setup(x => x.Send(It.IsAny<UpdateAPIFormCommand>(), default))
            .ReturnsAsync(OperationResult<APIResultResponseDto>.Success(exepctedResponse));

        // Act
        var result = await _patientExaminationCardController.UpdateAPIForm(patientExaminationCard.Id, assessmentModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(exepctedResponse, okResult.Value);
    }

    [Fact]
    public async Task UpdateAPIForm_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientExaminationCardId = Guid.NewGuid();
        var assessmentModel = new APIBleedingAssessmentModel();

        var exepctedResponse = new APIResultResponseDto
        {
            APIResult = 1,
            Maxilla = 1,
            Mandible = 1
        };

        _mediator.Setup(x => x.Send(It.IsAny<UpdateAPIFormCommand>(), default))
            .ReturnsAsync(OperationResult<APIResultResponseDto>.Failure("API Form Not Found"));

        // Act
        var result = await _patientExaminationCardController.UpdateAPIForm(patientExaminationCardId, assessmentModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}