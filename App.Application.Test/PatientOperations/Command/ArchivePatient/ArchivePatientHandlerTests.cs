using App.Application.PatientOperations.Command.ArchivePatient;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using MediatR;
using Moq;

namespace App.Application.Test.PatientOperations.Command.ArchivePatient;

public class ArchivePatientHandlerTests : TestHelper
{
    private readonly ArchivePatientHandler handler;
    private readonly ArchivePatientCommand command;

    public ArchivePatientHandlerTests()
    {
        handler = new ArchivePatientHandler(patientRepositoryMock.Object);
        command = new ArchivePatientCommand(Guid.NewGuid(), "test");
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldArchivePatient()
    {
        // Arrange
        var patient = new Patient("test", "test", "test@123", Gender.Male, "test", "test", 12, "test", "test", "test", "test", 2, "doctor123");

        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(patient);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.True(patient.IsArchived);
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
    }

    [Fact]
    public async Task Handle_WithArchivedPatient_ShouldReturnFailureResult()
    {
        // Arrange
        var patient = new Patient("test", "test", "test@123", Gender.Male, "test", "test", 12, "test", "test", "test", "test", 2, "doctor123");
        patient.ArchivePatient("test");

        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(patient);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient already archived.", result.ErrorMessage);
    }
}
