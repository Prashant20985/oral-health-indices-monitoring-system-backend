using App.Application.PatientExaminationCardOperations.Command.CommentBleedingForm;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CommentBleedingForm;

public class CommentBleedingFormHandlerTests : TestHelper
{
    private readonly CommentBleedingFormHandler handler;
    private readonly CommentBleedingFormCommand command;

    public CommentBleedingFormHandlerTests()
    {
        handler = new CommentBleedingFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new CommentBleedingFormCommand(Guid.NewGuid(), "Doctor comment", false);
    }

    [Fact]
    public async Task Handle_WhenBleedingFormDoesNotExist_ShouldReturnFailureRessult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Bleeding form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenBleedingFormExists_ShouldReturnSucessResult()
    {
        // Arrange
        var bleedingForm = new Bleeding();
        patientExaminationCardRepositoryMock.Setup(x => x.GetBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(bleedingForm);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.Comment, bleedingForm.DoctorComment);
    }
}
