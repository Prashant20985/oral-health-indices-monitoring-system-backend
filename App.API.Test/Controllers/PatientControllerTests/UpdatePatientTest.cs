using App.Application.Core;
using App.Application.PatientOperations.Command.UpdatePatient;
using App.Domain.DTOs.PatientDtos.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientControllerTests;

public class UpdatePatientTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TestablePatientController _patientController;

    public UpdatePatientTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _patientController = new TestablePatientController();
        _patientController.ExposeSetMediator(_mediatorMock.Object);
    }

    [Fact]
    public async Task UpdatePatient_Returns_OkResult()
    {
        //Arrange
        var patientId = Guid.NewGuid();
        var updatePatient = new UpdatePatientDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePatientCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _patientController.UpdatePatient(patientId, updatePatient);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePatient_Returns_BadRequestResult()
    {
        //Arrange
        var patientId = Guid.NewGuid();
        var updatePatient = new UpdatePatientDto();

        _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePatientCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Failure("test"));

        // Act
        var result = await _patientController.UpdatePatient(patientId, updatePatient);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}