using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;
using App.Domain.DTOs.Common.Request;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Common.DMFT_DMFS;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class UpdateDMFTDMFSFormTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public UpdateDMFTDMFSFormTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UpdateDMFTDMFSForm_WithValidData_ShouldReturnOk()
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

        var updateDmft_Dmfs = new UpdateDMFT_DMFSRequestDto
        {
            AssessmentModel = new DMFT_DMFSAssessmentModel(),
            DMFTResult = 1,
            DMFSResult = 1,
            ProstheticStatus = "x"
        };

        var exepctedResponse = new DMFT_DMFSResultResponseDto
        { 
            DMFSResult = 1,
            DMFTResult = 1
        };

        var prostheticStatus = "1";

        _mediator.Setup(x => x.Send(It.IsAny<UpdateDMFT_DMFSFormCommand>(), default))
            .ReturnsAsync(OperationResult<DMFT_DMFSResultResponseDto>.Success(exepctedResponse));

        // Act
        var result = await _patientExaminationCardController.UpdateDMFTDMFSForm(patientExaminationCard.Id,prostheticStatus, updateDmft_Dmfs);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(exepctedResponse, okResult.Value);
    }

    [Fact]
    public async Task UpdateDMFTDMFSForm_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientExaminationCardId = Guid.NewGuid();
        var updateDmft_Dmfs = new UpdateDMFT_DMFSRequestDto
        {
            AssessmentModel = new DMFT_DMFSAssessmentModel(),
            DMFTResult = 1,
            DMFSResult = 1,
            ProstheticStatus = "x"
        };

        var exepctedResponse = new DMFT_DMFSResultResponseDto
        {
            DMFSResult = 1,
            DMFTResult = 1
        };

        var prostheticStatus = "1";

        _mediator.Setup(x => x.Send(It.IsAny<UpdateDMFT_DMFSFormCommand>(), default))
            .ReturnsAsync(OperationResult<DMFT_DMFSResultResponseDto>.Failure("Dmft Dmfs Form not Found"));

        // Act
        var result = await _patientExaminationCardController.UpdateDMFTDMFSForm(patientExaminationCardId, prostheticStatus, updateDmft_Dmfs);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.Equal("Dmft Dmfs Form not Found", badRequestResult.Value);
    }
}
