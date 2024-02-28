using App.Application.Core;
using App.Application.PatientOperations.Command.ArchivePatient;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace App.API.Test.Controllers.PatientControllerTests;

public class ArchivePatientTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly TestablePatientController _patientcontroller;

    public ArchivePatientTest()
    {
        _mediator = new Mock<IMediator>();
        _patientcontroller = new TestablePatientController();
        _patientcontroller.ExposeSetMediator(_mediator.Object);
    }

    [Fact]
    public async Task ArchivePatient_WithValidData_ShouldReturnOk()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var archiveComment = "Archiving patient for testing purposes.";

        _mediator.Setup(x => x.Send(It.IsAny<ArchivePatientCommand>(), default))
            .ReturnsAsync(OperationResult<Unit>.Success(Unit.Value));

        // Act
        var result = await _patientcontroller.ArchivePatient(patientId, archiveComment);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}
