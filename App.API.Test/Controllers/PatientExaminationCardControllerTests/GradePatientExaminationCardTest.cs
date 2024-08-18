using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.GradePatientExaminationCard;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class GradePatientExaminationCardTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public GradePatientExaminationCardTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task GradePatientExaminationCard_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);
        var totalScore = 127.4m;

        _mediator.Setup(x => x.Send(It.IsAny<GradePatientExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _patientExaminationCardController.GradePatientExaminationCard(patientExaminationCard.Id, totalScore);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task GradePatientExaminationCard_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);
        var totalScore = 127.4m;
        _mediator.Setup(x => x.Send(It.IsAny<GradePatientExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Patient examination card not found"));

        // Act
        var result = await _patientExaminationCardController.GradePatientExaminationCard(Guid.NewGuid(), totalScore);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}
