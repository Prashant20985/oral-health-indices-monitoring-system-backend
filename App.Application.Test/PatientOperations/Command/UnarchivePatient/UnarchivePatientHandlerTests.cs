using App.Application.PatientOperations.Command.UnarchivePatient;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientOperations.Command.UnarchivePatient;

public class UnarchivePatientHandlerTests : TestHelper
{
    private readonly UnarchivePatientCommand command;
    private readonly UnarchivePatientHandler handler;

    public UnarchivePatientHandlerTests()
    {
        command = new UnarchivePatientCommand(Guid.NewGuid());
        handler = new UnarchivePatientHandler(patientRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldUnarchivePatient()
    {
        // Arrange
        var archivedPatient = new Patient("test", "test", "test@123", Gender.Male, "test", "test", 12, "test", "test",
            "test", "test", 2, "doctor123");
        archivedPatient.ArchivePatient("test");

        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(archivedPatient);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Null(result.ErrorMessage);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.False(archivedPatient.IsArchived);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
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
    public async Task Handle_WithUnarchivedPatient_ShouldReturnFailureResult()
    {
        // Arrange
        var nonArchivedPatient = new Patient("test", "test", "test@123", Gender.Male, "test", "test", 12, "test",
            "test", "test", "test", 2, "doctor123");

        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(nonArchivedPatient);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.ErrorMessage);
        Assert.Contains("Patient is not archived.", result.ErrorMessage);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
    }
}