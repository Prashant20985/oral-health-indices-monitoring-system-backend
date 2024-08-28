using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Query.FetchPatientExaminationCardDetails;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class GetPatientExaminationCardDetailsTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public GetPatientExaminationCardDetailsTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GetPatientExaminationCardDetails_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var patientExaminationCardDto = new PatientExaminationCardDto();
        _mediator.Setup(x => x.Send(It.IsAny<FetchPatientExaminationCardDetailsQuery>(), default))
            .ReturnsAsync(OperationResult<PatientExaminationCardDto>.Success(patientExaminationCardDto));

        // Act
        var result =
            await _patientExaminationCardController.GetPatientExaminationCardDetails(patientExaminationCard.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<PatientExaminationCardDto>(okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GetPatientExaminationCardDetails_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        _mediator.Setup(x => x.Send(It.IsAny<FetchPatientExaminationCardDetailsQuery>(), default))
            .ReturnsAsync(OperationResult<PatientExaminationCardDto>.Failure("Patient examination card not found"));

        // Act
        var result = await _patientExaminationCardController.GetPatientExaminationCardDetails(Guid.NewGuid());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}