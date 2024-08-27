using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;
using App.Domain.DTOs.Common.Request;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class UpdatePatientExaminationCardSummaryTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public UpdatePatientExaminationCardSummaryTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UpdatePatientExaminationCardSummary_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var summary = new SummaryRequestDto
        {
            PatientRecommendations = "Patient recommendations",
            NeedForDentalInterventions = "1",
            Description = "Description",
            ProposedTreatment = "Proposed treatment"
        };

        _mediator.Setup(x =>
                x.Send(It.IsAny<UpdatePatientExaminationCardSummaryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result =
            await _patientExaminationCardController.UpdatePatientExaminationCardSummary(patientExaminationCard.Id,
                summary);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdatePatientExaminationCardSummary_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        var summary = new SummaryRequestDto
        {
            PatientRecommendations = "Patient recommendations",
            NeedForDentalInterventions = "1",
            Description = "Description",
            ProposedTreatment = "Proposed treatment"
        };

        _mediator.Setup(x =>
                x.Send(It.IsAny<UpdatePatientExaminationCardSummaryCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OperationResult<Unit>.Failure("Patient examination card not found."));

        // Act
        var result =
            await _patientExaminationCardController.UpdatePatientExaminationCardSummary(Guid.NewGuid(), summary);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
}