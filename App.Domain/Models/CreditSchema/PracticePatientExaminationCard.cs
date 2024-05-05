using App.Domain.Models.Users;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents the result of an examination for a student in a practice scenario.
/// </summary>
public class PracticePatientExaminationCard(Guid examId, string studentId)
{
    /// <summary>
    /// Gets or sets the unique identifier of the student examination result.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the mark obtained by the student in the examination.
    /// </summary>
    public decimal StudentMark { get; private set; }

    /// <summary>
    /// Gets or sets the comment provided by the doctor regarding the examination.
    /// </summary>
    public string DoctorComment { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the risk factor assessment associated with this examination.
    /// </summary>
    public Guid RiskFactorAssessmentId { get; private set; }

    /// <summary>
    /// Gets or sets the risk factor assessment associated with this examination.
    /// </summary>
    public virtual PracticeRiskFactorAssessment PracticeRiskFactorAssessment { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the patient examination result associated with this examination.
    /// </summary>
    public Guid PatientExaminationResultId { get; private set; }

    /// <summary>
    /// Gets or sets the patient examination result associated with this examination.
    /// </summary>
    public virtual PracticePatientExaminationResult PracticePatientExaminationResult { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the exam associated with this examination.
    /// </summary>
    public Guid ExamId { get; private set; } = examId;

    /// <summary>
    /// Gets or sets the exam associated with this examination.
    /// </summary>
    public virtual Exam Exam { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier of the patient associated with this examination.
    /// </summary>
    public Guid PatientId { get; private set; }

    /// <summary>
    /// Gets or sets the patient associated with this examination.
    /// </summary>
    public virtual PracticePatient PracticePatient { get; private set; }

    /// <summary>
    /// Gets or sets the student identifier associated with this examination.
    /// </summary>
    public string StudentId { get; private set; } = studentId;

    /// <summary>
    /// Gets or sets the student associated with this examination.
    /// </summary>
    public virtual ApplicationUser Student { get; private set; }

    public void SetPatientId(Guid patientId) => PatientId = patientId;
    public void SetStudentMark(decimal studentMark) => StudentMark = studentMark;
    public void SetDoctorComment(string doctorComment) => DoctorComment = doctorComment;
    public void SetPatientExaminationResultId(Guid patientExaminationResultId) => PatientExaminationResultId = patientExaminationResultId;
    public void SetRiskFactorAssessmentId(Guid riskFactorAssessmentId) => RiskFactorAssessmentId = riskFactorAssessmentId;
}

