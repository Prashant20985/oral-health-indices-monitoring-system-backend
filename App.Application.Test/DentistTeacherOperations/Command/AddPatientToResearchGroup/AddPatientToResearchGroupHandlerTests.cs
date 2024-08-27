using App.Application.DentistTeacherOperations.Command.AddPatientToResearchGroup;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using Moq;

namespace App.Application.Test.DentistTeacherOperations.Command.AddPatientToResearchGroup;

public class AddPatientToResearchGroupHandlerTests : TestHelper
{
    private readonly AddPatientToResearchGroupHandler handler;

    public AddPatientToResearchGroupHandlerTests()
    {
        handler = new AddPatientToResearchGroupHandler(patientRepositoryMock.Object,
            researchGroupRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldAddPatientToResearchGroup()
    {
        // Arrange
        var patient1 = new Patient("test", "test", "test@test.com", Gender.Male, "test", "test", 19, "test", "test",
            "test", "test", 1, "test");
        var researchGroup1 = new ResearchGroup("test", "test", "doctorId");


        patientRepositoryMock.Setup(repo => repo.GetPatientById(patient1.Id)).ReturnsAsync(patient1);
        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupById(researchGroup1.Id))
            .ReturnsAsync(researchGroup1);

        var command = new AddPatientToResearchGroupCommand(researchGroup1.Id, patient1.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(patient1.Id), Times.Once);
        researchGroupRepositoryMock.Verify(repo => repo.GetResearchGroupById(researchGroup1.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidPatientId_ShouldReturnFailureResult()
    {
        // Arrange
        var researchGroup1 = new ResearchGroup("test", "test", "doctorId");

        patientRepositoryMock.Setup(repo => repo.GetPatientById(It.IsAny<Guid>())).ReturnsAsync(value: null);
        researchGroupRepositoryMock.Setup(repo => repo.GetResearchGroupById(researchGroup1.Id))
            .ReturnsAsync(researchGroup1);

        var command = new AddPatientToResearchGroupCommand(researchGroup1.Id, Guid.NewGuid());

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient not found", result.ErrorMessage);
        patientRepositoryMock.Verify(repo => repo.GetPatientById(It.IsAny<Guid>()), Times.Once);
        researchGroupRepositoryMock.Verify(repo => repo.GetResearchGroupById(researchGroup1.Id), Times.Never);
    }
}