using App.Domain.DTOs.Common.Response;

namespace App.Domain.DTOs.PatientDtos.Response;

/// <summary>
/// Data transfer object representing information about a patient examination card.
/// </summary>
public class PatientExaminationCardDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the patient examination card.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the doctor's comment related to the examination card.
    /// </summary>
    public string DoctorComment { get; init; }

    /// <summary>
    /// Gets or sets a value indicating whether the examination card is in regular mode or test mode.
    /// </summary>
    public bool IsRegularMode { get; init; }

    /// <summary>
    /// Gets or sets the total score of the examination card.
    /// </summary>
    public decimal? TotalScore { get; init; }

    /// <summary>
    /// Gets or sets the date of the examination card.
    /// </summary>
    public DateTime DateOfExamination { get; init; }

    /// <summary>
    /// Gets or sets the risk factor assessment details associated with the examination card.
    /// </summary>
    public RiskFactorAssessmentDto RiskFactorAssessment { get; init; }

    /// <summary>
    /// Gets or sets the result details associated with the examination card.
    /// </summary>
    public PatientExaminationResultDto PatientExaminationResult { get; init; }
}
