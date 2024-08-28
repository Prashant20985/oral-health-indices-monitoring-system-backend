using App.Application.DentistTeacherOperations.Command.RemovePatientFromResearchGroup;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.RemovePatientFromResearchGroup;

public class RemovePatientFromResearchGroupHandlerTests : TestHelper
{
    private readonly RemovePatientFromResearchGroupHandler handler;

    public RemovePatientFromResearchGroupHandlerTests()
    {
        handler = new RemovePatientFromResearchGroupHandler(patientRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldRemovePatientFromResearchGroup()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test", "test", "test", 1, "test");
        var researchGroup = new ResearchGroup("test", "test", "test");
        patient1.AddResearchGroup(researchGroup.Id);

        patientRepositoryMock.Setup(repo => repo.GetPatientById(patient1.Id))
            .ReturnsAsync(patient1);

        var command = new RemovePatientFromResearchGroupCommand(patient1.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingPatient_ShouldReturnFailureResult()
    {
        // Arrange
        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        var command = new RemovePatientFromResearchGroupCommand(Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient not found", result.ErrorMessage);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(command.PatientId), Times.Once);
    }
}
