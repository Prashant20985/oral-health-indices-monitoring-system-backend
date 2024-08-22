using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Domain.Models.OralHealthExamination;

/// <summary>
/// Represents the risk factor assessment for a patient's oral health examination.
/// </summary>
public class RiskFactorAssessment
{
    /// <summary>
    /// Gets the unique identifier for the risk factor assessment.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the risk factor assessment model.
    /// </summary>
    public RiskFactorAssessmentModel RiskFactorAssessmentModel { get; private set; }

    /// <summary>
    /// Gets or sets the patient examination card associated with the risk factor assessment.
    /// </summary>
    public PatientExaminationCard PatientExaminationCard { get; set; }

    /// <summary>
    /// Sets the risk factor assessment model.
    /// </summary>
    /// <param name="riskFactorAssessmentModel">The risk factor assessment model to set.</param>
    public void SetRiskFactorAssessmentModel(RiskFactorAssessmentModel riskFactorAssessmentModel) => RiskFactorAssessmentModel = riskFactorAssessmentModel;
}