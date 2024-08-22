using App.Domain.DTOs.Common.Response;
using App.Domain.DTOs.PatientDtos.Response;

namespace App.Domain.DTOs.ExamDtos.Response;
/// <summary>
///  Represents the Practice Patient Examination Card DTO.
/// </summary>
public class PracticePatientExaminationCardDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the student examination result.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the mark obtained by the student in the examination.
    /// </summary>
    public decimal StudentMark { get; set; }

    /// <summary>
    /// Gets or sets the comment provided by the doctor regarding the examination.
    /// </summary>
    public string DoctorComment { get; set; }

    /// <summary>
    /// Gets or sets the doctor name associated with the examination card.
    /// </summary>
    public string DoctorName { get; set; }

    /// <summary>
    /// Gets or sets the student name associated with the examination card.
    /// </summary>
    public string StudentName { get; set; }

    /// <summary>
    /// Gets or sets the Summary details associated with the examination card.
    /// </summary>
    public SummaryResponseDto Summary { get; set; }

    /// <summary>
    /// Gets or sets the patient details associated with the examination card.
    /// </summary>
    public PatientResponseDto PracticePatient { get; set; }

    /// <summary>
    /// Gets or sets the risk factor assessment details associated with the examination card.
    /// </summary>
    public RiskFactorAssessmentDto PracticeRiskFactorAssessment { get; init; }

    /// <summary>
    /// Gets or sets the result details associated with the examination card.
    /// </summary>
    public PracticePatientExaminationResultResponseDto PracticePatientExaminationResult { get; init; }
}
