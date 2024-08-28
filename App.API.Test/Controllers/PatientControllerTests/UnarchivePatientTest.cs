using App.Application.Core;
using App.Application.PatientOperations.Command.UnarchivePatient;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientControllerTests;

public class UnarchivePatientTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientController _patientcontroller;

    public UnarchivePatientTest()
    {
        _mediator = new Mock<IMediator>();
        _patientcontroller = new TestablePatientController();
        _patientcontroller.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task UnarchivePatient_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();

        _mediator.Setup(x => x.Send(It.IsAny<UnarchivePatientCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _patientcontroller.UnarchivePatient(patientId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}
