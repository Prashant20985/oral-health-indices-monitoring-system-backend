using App.Application.PatientExaminationCardOperations.Command.GradePatientExaminationCard;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.GradePatientExaminationCard;

public class GradePatientExaminationCardHandlerTests : TestHelper
{
    private readonly GradePatientExaminationCardHandler handler;
    private readonly GradePatientExaminationCardCommand command;

    public GradePatientExaminationCardHandlerTests()
    {
        handler = new GradePatientExaminationCardHandler(patientExaminationCardRepositoryMock.Object);
        command = new GradePatientExaminationCardCommand(Guid.NewGuid(), 5);
    }

    [Fact]
    public async Task Handle_WhenPatientExaminationCardDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetPatientExaminationCard(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Patient examination card not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenPatientExaminationCardExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());

        patientExaminationCardRepositoryMock.Setup(x => x.GetPatientExaminationCard(It.IsAny<Guid>()))
            .ReturnsAsync(patientExaminationCard);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal(command.TotalScore, patientExaminationCard.TotalScore);
    }
}
