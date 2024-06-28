using App.Application.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;
using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.OralHealthExamination;
using MediatR;
using Moq;

namespace App.Application.Test.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;

public class UpdateRiskFactorAssessmentFormHandlerTests : TestHelper
{
    private readonly UpdateRiskFactorAssessmentFormHandler handler;
    private readonly UpdateRiskFactorAssessmentFormCommand command;

    public UpdateRiskFactorAssessmentFormHandlerTests()
    {
        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        handler = new UpdateRiskFactorAssessmentFormHandler(patientExaminationCardRepositoryMock.Object);
        command = new UpdateRiskFactorAssessmentFormCommand(Guid.NewGuid(), riskFactorAssessmentModel);
    }

    [Fact]
    public async Task Handle_WhenRiskFactorAssessmentFormDoesNotExist_ShouldReturnFailureResult()
    {
        // Arrange
        patientExaminationCardRepositoryMock.Setup(x => x.GetRiskFactorAssessmentByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(value: null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Equal("Risk factor assessment form not found", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WhenRiskFactorAssessmentFormExists_ShouldReturnSuccessResult()
    {
        // Arrange
        var patientExaminationCard = new PatientExaminationCard(Guid.NewGuid());
        var riskFactorAssessment = new RiskFactorAssessment();

        patientExaminationCard.SetRiskFactorAssesmentId(riskFactorAssessment.Id);

        patientExaminationCardRepositoryMock.Setup(x => x.GetRiskFactorAssessmentByCardId(It.IsAny<Guid>()))
            .ReturnsAsync(riskFactorAssessment);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(Unit.Value, result.ResultValue);
        Assert.Equal(command.AssessmentModel, riskFactorAssessment.RiskFactorAssessmentModel);
    }
}
