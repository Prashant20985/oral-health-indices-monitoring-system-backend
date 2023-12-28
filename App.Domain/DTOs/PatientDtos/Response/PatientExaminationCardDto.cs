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
    /// Gets or sets the test mode details associated with the examination card.
    /// </summary>
    public PatientExaminationTestModeDto PatientExaminationTestMode { get; init; }

    /// <summary>
    /// Gets or sets the regular mode details associated with the examination card.
    /// </summary>
    public PatientExaminationRegularModeDto PatientExaminationRegularMode { get; init; }

    /// <summary>
    /// Gets or sets the risk factor assessment details associated with the examination card.
    /// </summary>
    public RiskFactorAssessmentDto RiskFactorAssessment { get; init; }

    /// <summary>
    /// Gets or sets the result details associated with the examination card.
    /// </summary>
    public PatientExaminationResultDto PatientExaminationResult { get; init; }
}
