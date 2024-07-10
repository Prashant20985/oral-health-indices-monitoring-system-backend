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
    /// Gets or sets the doctor's name associated with the examination card.
    /// </summary>
    public string DoctorName { get; init; }

    /// <summary>
    /// Gets or sets the student's name associated with the examination card.
    /// </summary>
    public string StudentName { get; init; }

    /// <summary>
    /// Gets or sets the patient's name associated with the examination card.
    /// </summary>
    public string PatientName { get; init; }

    /// <summary>
    /// Gets or sets the doctor's comment related to the examination card.
    /// </summary>
    public string DoctorComment { get; init; }

    /// <summary>
    /// Gets or sets the student's comment related to the examination card.
    /// </summary>
    public string StudentComment { get; init; }

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
    /// Gets or sets the need for dental interventions associated with the examination card.
    /// </summary>
    public string NeedForDentalIntervetions { get; init; }

    /// <summary>
    /// Gets or sets the proposed treatment associated with the examination card.
    /// </summary>
    public string ProposedTreatment { get; init; }

    /// <summary>
    /// Gets or sets the description associated with the examination card.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// Gets or sets the patient recommendations associated with the examination card.
    /// </summary>
    public string PatientRecommendations { get; init; }

    /// <summary>
    /// Gets or sets the risk factor assessment details associated with the examination card.
    /// </summary>
    public RiskFactorAssessmentDto RiskFactorAssessment { get; init; }

    /// <summary>
    /// Gets or sets the result details associated with the examination card.
    /// </summary>
    public PatientExaminationResultDto PatientExaminationResult { get; init; }
}
