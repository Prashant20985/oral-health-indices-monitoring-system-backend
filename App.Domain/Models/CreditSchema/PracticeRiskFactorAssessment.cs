using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents the assessment of risk factors in a practice scenario.
/// </summary>
public class PracticeRiskFactorAssessment
{
    /// <summary>
    /// Gets the unique identifier of the risk factor assessment.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the risk factor assessment model associated with this assessment.
    /// </summary>
    public RiskFactorAssessmentModel RiskFactorAssessmentModel { get; private set; }

    /// <summary>
    /// Gets or sets the examination card associated with this assessment.
    /// </summary>
    public virtual PracticePatientExaminationCard PracticePatientExaminationCard { get; set; }

    /// <summary>
    /// Sets the risk factor assessment model associated with this assessment.
    /// </summary>
    /// <param name="riskFactorAssessmentModel"></param>
    public void SetRiskFactorAssessmentModel(RiskFactorAssessmentModel riskFactorAssessmentModel) =>
        RiskFactorAssessmentModel = riskFactorAssessmentModel;
}

