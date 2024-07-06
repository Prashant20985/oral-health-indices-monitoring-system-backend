using App.Application.Core;
using App.Application.PatientExaminationCardOperations.Command.UpdateBleedingForm;
using App.Domain.DTOs.Common.Response;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.UpdateBleedingForm;

public class UpdateBleedingFormHandlerTests : TestHelper
{
    private readonly UpdateBleedingFormHandler handler;
    private readonly UpdateBleedingFormCommand command;

    public UpdateBleedingFormHandlerTests()
    {
        var bleedingAssesmentModel = new APIBleedingAssessmentModel
        {
            Quadrant1 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            },
            Quadrant2 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            },
            Quadrant3 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            },
            Quadrant4 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "+",
                Value4 = "-",
                Value5 = "+",
                Value6 = "+",
                Value7 = "-",
            }
        };

        handler = new UpdateBleedingFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new UpdateBleedingFormCommand(Guid.NewGuid(), bleedingAssesmentModel);
    }

    [Fact]
    public async Task Handle_WhenBleedingFormDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Bleeding form not found.", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenBleedingFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patienExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var bleeding = new Bleeding();

        var patientExaminationResult = new PatientExaminationResult(bleeding.Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        patienExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        patientExaminationCardRepositoryMock.Setup(x => x.GetBleedingByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(bleeding);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.IsType<OperationResult<BleedingResultResponseDto>>(result);
        Assert.Equal(bleeding.AssessmentModel, command.AssessmentModel);
    }
}
