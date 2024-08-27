using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.DeletePatientExaminationCard;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientExaminationCardControllerTests;

public class DeletePatientExaminationCardTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientExaminationCardController _patientExaminationCardController;

    public DeletePatientExaminationCardTest()
    {
        _mediator = new Mock<IMediator>();
        _patientExaminationCardController = new TestablePatientExaminationCardController();
        _patientExaminationCardController.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task DeletePatientExaminationCard_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        _mediator.Setup(x => x.Send(It.IsAny<DeletePatientExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _patientExaminationCardController.DeletePatientExaminationCard(patientExaminationCard.Id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task DeletePatientExaminationCard_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patientExaminationCard = new PatientExaminationCard(patientId);

        _mediator.Setup(x => x.Send(It.IsAny<DeletePatientExaminationCardCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("Patient examination card not found"));

        // Act
        var result = await _patientExaminationCardController.DeletePatientExaminationCard(Guid.NewGuid());

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }
}