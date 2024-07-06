using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;

public class UpdateDMFT_DMFSFormHandlerTests : TestHelper
{
    private readonly UpdateDMFT_DMFSFormHandler handler;
    private readonly UpdateDMFT_DMFSFormCommand command;

    public UpdateDMFT_DMFSFormHandlerTests()
    {
        var dmft_dmfsAssessmentModel = new DMFT_DMFSAssessmentModel();

        handler = new UpdateDMFT_DMFSFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new UpdateDMFT_DMFSFormCommand(Guid.NewGuid(), dmft_dmfsAssessmentModel);
    }

    [Fact]
    public async Task Handle_WhenDMFT_DMFSFormDoesNotExist_ShouldReturnFailureResult()
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
    public async Task Handle_WhenDMFT_DMFSFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patienExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var dmft_dmfs = new DMFT_DMFS();

        var patientExaminationResult = new PatientExaminationResult(dmft_dmfs.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        patienExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        patientExaminationCardRepositoryMock.Setup(x => x.GetDMFT_DMFSByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(dmft_dmfs);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.IsType<OperationResult<DMFT_DMFSResultResponseDto>>(result);
        Assert.Equal(dmft_dmfs.AssessmentModel, command.AssessmentModel);
    }
}
