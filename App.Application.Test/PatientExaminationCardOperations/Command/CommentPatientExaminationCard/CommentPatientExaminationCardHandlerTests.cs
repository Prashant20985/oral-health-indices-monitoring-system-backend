using App.Application.PatientExaminationCardOperations.Command.CommentPatientExaminationCard;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;


namespace App.Application.Test.PatientExaminationCardOperations.Command.CommentPatientExaminationCard;

public class CommentPatientExaminationCardHandlerTests : TestHelper
{
    private readonly CommentPatientExaminationCardHandler handler;
    private readonly CommentPatientExaminationCardCommand command;

    public CommentPatientExaminationCardHandlerTests()
    {
        handler = new CommentPatientExaminationCardHandler(patientExaminationCardRepositoryMock.Object);
        command = new CommentPatientExaminationCardCommand(Guid.NewGuid(), "Doctor comment");
    }

    [Fact]
    public async Task Handle_WhenPatientExaminationCardDoesNotExist_ShouldReturnFailureRessult()
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
    public async Task Handle_WhenPatientExaminationCardExists_ShouldReturnSucessResult()
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
        Assert.Contains(command.DoctorComment, patientExaminationCard.DoctorComment);
    }
}
