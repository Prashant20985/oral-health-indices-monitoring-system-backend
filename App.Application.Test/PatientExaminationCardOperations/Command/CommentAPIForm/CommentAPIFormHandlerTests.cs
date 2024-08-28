using App.Application.PatientExaminationCardOperations.Command.CommentAPIForm;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CommentAPIForm;

public class CommentAPIFormHandlerTests : TestHelper
{
    private readonly CommentAPIFormCommnand command;
    private readonly CommentAPIFormHandler handler;

    public CommentAPIFormHandlerTests()
    {
        handler = new CommentAPIFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new CommentAPIFormCommnand(Guid.NewGuid(), "Doctor comment", false);
    }

    [Fact]
    public async Task Handle_WhenAPIFormDoesNotExist_ShouldReturnFailureRessult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetAPIByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("API Form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenAPIFormExists_ShouldReturnSucessResult()
    {
        // Arrange
        var apiForm = new API();

        patientExaminationCardRepositoryMock.Setup(x => x.GetAPIByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(apiForm);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.Comment, apiForm.DoctorComment);
    }
}