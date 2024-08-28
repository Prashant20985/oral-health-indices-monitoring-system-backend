using App.Domain.Models.Common.RiskFactorAssessment;
using App.Domain.Models.CreditSchema;

namespace App.Domain.Test.Models.CreditSchema;

public class PracticeRiskFactorAssessmentTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Act
        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();

        // Assert
        Assert.NotEqual(Guid.Empty, practiceRiskFactorAssessment.Id);
        Assert.Null(practiceRiskFactorAssessment.RiskFactorAssessmentModel);
        Assert.Null(practiceRiskFactorAssessment.PracticePatientExaminationCard);
    }

    [Fact]
    public void SetRiskFactorAssessmentModel_ShouldSetRiskFactorAssessmentModel()
    {
        // Arrange
        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var riskFactorAssessmentModel = new RiskFactorAssessmentModel();

        // Act
        practiceRiskFactorAssessment.SetRiskFactorAssessmentModel(riskFactorAssessmentModel);

        // Assert
        Assert.Equal(riskFactorAssessmentModel, practiceRiskFactorAssessment.RiskFactorAssessmentModel);
    }

    [Fact]
    public void SetPracticePatientExaminationCard_ShouldSetPracticePatientExaminationCard()
    {
        // Arrange
        var practiceRiskFactorAssessment = new PracticeRiskFactorAssessment();
        var practicePatientExaminationCard = new PracticePatientExaminationCard(Guid.NewGuid(), "student123");

        // Act
        practiceRiskFactorAssessment.PracticePatientExaminationCard = practicePatientExaminationCard;

        // Assert
        Assert.Equal(practicePatientExaminationCard, practiceRiskFactorAssessment.PracticePatientExaminationCard);
    }
}
