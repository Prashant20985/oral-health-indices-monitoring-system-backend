using App.Domain.Models.Common.RiskFactorAssessment;

namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Data transfer object representing information about the risk factor assessment in a patient examination.
/// </summary>
public class RiskFactorAssessmentDto
{
    /// <summary>
    /// Gets or sets the model containing details of the risk factor assessment.
    /// </summary>
    public RiskFactorAssessmentModel RiskFactorAssessmentModel { get; init; }
}
