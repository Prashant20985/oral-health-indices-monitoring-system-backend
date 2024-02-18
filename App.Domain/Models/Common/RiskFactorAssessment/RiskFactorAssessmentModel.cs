namespace App.Domain.Models.Common.RiskFactorAssessment;

/// <summary>
/// Represents a model for risk factor assessment.
/// </summary>
public class RiskFactorAssessmentModel
{
    /// <summary>
    /// Gets or sets the list of risk factor assessment questions.
    /// </summary>
    public List<RiskFactorAssessmentQuestionModel> Questions { get; set; }
}

