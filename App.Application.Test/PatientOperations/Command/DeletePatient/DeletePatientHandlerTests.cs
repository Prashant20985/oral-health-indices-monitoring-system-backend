using App.Application.PatientOperations.Command.DeletePatient;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientOperations.Command.DeletePatient;

public class DeletePatientHandlerTests : TestHelper
{
    private readonly DeletePatientCommand command;
    private readonly DeletePatientHandler handler;

    public DeletePatientHandlerTests()
    {
        command = new DeletePatientCommand(Guid.NewGuid());
        handler = new DeletePatientHandler(patientRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldDeletePatient()
    {
        // Arrange
        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(new Patient("test", "test", "test@123", Gender.Male, "test", "test", 12, "test", "test",
                "test", "test", 2, "doctor123"));


        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
        patientRepositoryMock.Verify(repo => repo.DeletePatient(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingPatient_ShouldReturnFailureResult()
    {
        // Arrange
        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient not found.", result.ErrorMessage);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
        patientRepositoryMock.Verify(repo => repo.DeletePatient(It.IsAny<Guid>()), Times.Never);
    }
}