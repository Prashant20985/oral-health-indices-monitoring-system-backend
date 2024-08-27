using App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CommentBeweForm;

public class CommentBeweFormHandlerTests : TestHelper
{
    private readonly CommentBeweFormHandler handler;
    private readonly CommentBeweFormCommand command;

    public CommentBeweFormHandlerTests()
    {
        handler = new CommentBeweFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new CommentBeweFormCommand(Guid.NewGuid(), "Doctor comment", false);
    }

    [Fact]
    public async Task Handle_WhenBeweFormDoesNotExist_ShouldReturnFailureRessult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetBeweByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Bewe form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenBeweFormExists_ShouldReturnSucessResult()
    {
        // Arrange
        var beweForm = new Bewe();
        patientExaminationCardRepositoryMock.Setup(x => x.GetBeweByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(beweForm);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.Comment, beweForm.DoctorComment);
    }
}
