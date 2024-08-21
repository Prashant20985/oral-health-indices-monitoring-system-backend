using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.OralHealthExamination;

namespace App.Domain.Test.Models.OralHealthExamination;

public class RiskFactorAssessmentTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Act
        var riskFactorAssessment = new RiskFactorAssessment();

        // Assert
        Assert.NotEqual(Guid.Empty, riskFactorAssessment.Id);
        Assert.Null(riskFactorAssessment.RiskFactorAssessmentModel);
    }

    [Fact]
    public void SetRiskFactorAssessmentModel_ShouldSetRiskFactorAssessmentModel()
    {
        // Arrange
        var riskFactorAssessment = new RiskFactorAssessment();
        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        // Act
        riskFactorAssessment.SetRiskFactorAssessmentModel(riskFactorAssessmentModel);

        // Assert
        Assert.Equal(riskFactorAssessmentModel, riskFactorAssessment.RiskFactorAssessmentModel);
    }
}
