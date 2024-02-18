namespace App.Domain.Models.Common.RiskFactorAssessment;

/// <summary>
/// Represents a model for risk factor assessment answers.
/// </summary>
public class RiskFactorAssessmentAnswerModel
{
    /// <summary>
    /// Gets or sets the answer for low risk.
    /// </summary>
    public string LowRisk { get; set; }

    /// <summary>
    /// Gets or sets the answer for moderate risk.
    /// </summary>
    public string ModerateRisk { get; set; }

    /// <summary>
    /// Gets or sets the answer for high risk.
    /// </summary>
    public string HighRisk { get; set; }
}

