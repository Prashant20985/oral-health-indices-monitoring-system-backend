using App.Application.PatientExaminationCardOperations.Command.DeletePatientExaminationCard;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.DeletePatientExaminationCard;

public class DeletePatientExaminationCardHandlerTests : TestHelper
{
    private readonly DeletePatientExaminationCardHandler handler;
    private readonly DeletePatientExaminationCardCommand command;

    public DeletePatientExaminationCardHandlerTests()
    {
        handler = new DeletePatientExaminationCardHandler(patientExaminationCardRepositoryMock.Object);
        command = new DeletePatientExaminationCardCommand(Guid.NewGuid());
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
        Assert.Equal("Examination Card not forund", result.ErrorMessage);
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
    }
}
