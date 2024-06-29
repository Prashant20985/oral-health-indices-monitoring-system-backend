using App.Application.PatientExaminationCardOperations.Command.UpdateAPIForm;
using App.Domain.Models.Common.APIBleeding;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.UpdateAPIForm;

public class UpdateAPIFormHandlerTests : TestHelper
{
    private readonly UpdateAPIFormHandler handler;
    private readonly UpdateAPIFormCommand command;

    public UpdateAPIFormHandlerTests()
    {
        var apiAssesmentModel = new APIBleedingAssessmentModel
        {
            Quadrant1 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            },
            Quadrant2 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            },
            Quadrant3 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            },
            Quadrant4 = new Quadrant
            {
                Value1 = "+",
                Value2 = "+",
                Value3 = "-",
                Value4 = "+",
                Value5 = "+",
                Value6 = "-",
                Value7 = "+",
            }
        };

        handler = new UpdateAPIFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new UpdateAPIFormCommand(Guid.NewGuid(), apiAssesmentModel);
    }

    [Fact]
    public async Task Handle_WhenAPIFormDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetAPIByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("API form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenAPIFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patienExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var api = new API();

        var patientExaminationResult = new PatientExaminationResult(Guid.NewGuid(), Guid.NewGuid(), api.Id, Guid.NewGuid());

        patienExaminationCard.SetPatientExaminationResultId(patientExaminationResult.Id);

        patientExaminationCardRepositoryMock.Setup(x => x.GetAPIByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(api);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal(command.AssessmentModel, api.AssessmentModel);
    }
}
