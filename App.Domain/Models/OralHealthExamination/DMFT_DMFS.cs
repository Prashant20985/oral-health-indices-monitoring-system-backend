using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.Models.OralHealthExamination;

/// <summary>
/// Represents the DMFT/DMFS (Decayed, Missing, Filled Teeth/Surfaces) examination results.
/// </summary>
public class DMFT_DMFS
{
    /// <summary>
    /// Gets the unique identifier for the DMFT/DMFS examination.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the DMFT result.
    /// </summary>
    public decimal DMFTResult { get; private set; }

    /// <summary>
    /// Gets the DMFS result.
    /// </summary>
    public decimal DMFSResult { get; private set; }

    /// <summary>
    /// Gets the doctor's comment.
    /// </summary>
    public string DoctorComment { get; private set; }

    /// <summary>
    /// Gets the student's comment.
    /// </summary>
    public string StudentComment { get; private set; }

    /// <summary>
    /// Gets the prosthetic status.
    /// </summary>
    public string ProstheticStatus { get; private set; }

    /// <summary>
    /// Gets the assessment model for the DMFT/DMFS examination.
    /// </summary>
    public DMFT_DMFSAssessmentModel AssessmentModel { get; private set; }

    /// <summary>
    /// Gets or sets the patient examination result.
    /// </summary>
    public virtual PatientExaminationResult PatientExaminationResult { get; set; }

    /// <summary>
    /// Adds a comment from the doctor.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddDoctorComment(string comment) => DoctorComment = comment;

    /// <summary>
    /// Adds a comment from the student.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddStudentComment(string comment) => StudentComment = comment;

    /// <summary>
    /// Sets the DMFT result.
    /// </summary>
    /// <param name="dmftResult">The DMFT result to set.</param>
    public void SetDMFTResult(decimal dmftResult) => DMFTResult = dmftResult;

    /// <summary>
    /// Sets the DMFS result.
    /// </summary>
    /// <param name="dmfsResult">The DMFS result to set.</param>
    public void SetDMFSResult(decimal dmfsResult) => DMFSResult = dmfsResult;

    /// <summary>
    /// Sets the assessment model for the DMFT/DMFS examination.
    /// </summary>
    /// <param name="assessmentModel">The assessment model to set.</param>
    public void SetDMFT_DMFSAssessmentModel(DMFT_DMFSAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;

    /// <summary>
    /// Sets the prosthetic status.
    /// </summary>
    /// <param name="prostheticStatus">The prosthetic status to set.</param>
    public void SetProstheticStatus(string prostheticStatus) => ProstheticStatus = prostheticStatus;
}