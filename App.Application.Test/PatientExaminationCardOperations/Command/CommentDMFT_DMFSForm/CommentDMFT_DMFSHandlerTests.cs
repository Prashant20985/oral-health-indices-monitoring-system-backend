using App.Application.PatientExaminationCardOperations.Command.CommentDMFT_DMFSForm;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.CommentDMFT_DMFSForm;

public class CommentDMFT_DMFSHandlerTests : TestHelper
{
    private readonly CommentDMFT_DMFSHandler handler;
    private readonly CommentDMFT_DMFSCommand command;

    public CommentDMFT_DMFSHandlerTests()
    {
        handler = new CommentDMFT_DMFSHandler(patientExaminationCardRepositoryMock.Object);
        command = new CommentDMFT_DMFSCommand(Guid.NewGuid(), "Doctor comment", false);
    }

    [Fact]
    public async Task Handle_WhenDMFT_DMFSFormDoesNotExist_ShouldReturnFailureRessult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetDMFT_DMFSByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("DMFT/DMFS form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenDMFT_DMFSFormExists_ShouldReturnSucessResult()
    {
        // Arrange
        var dmft_dmfsForm = new DMFT_DMFS();
        patientExaminationCardRepositoryMock.Setup(x => x.GetDMFT_DMFSByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(dmft_dmfsForm);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Contains(command.Comment, dmft_dmfsForm.DoctorComment);
    }
}
