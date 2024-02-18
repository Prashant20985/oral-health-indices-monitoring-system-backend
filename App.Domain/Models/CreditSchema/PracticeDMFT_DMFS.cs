using App.Domain.Models.Common.DMFT_DMFS;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents a practice DMFT (Decayed, Missing, Filled Teeth) and DMFS (Decayed, Missing, Filled Surfaces) assessment.
/// </summary>
public class PracticeDMFT_DMFS
{
    /// <summary>
    /// Gets the unique identifier of the practice DMFT_DMFS assessment.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the DMFT result of the assessment.
    /// </summary>
    public decimal DMFTResult { get; set; }

    /// <summary>
    /// Gets or sets the DMFS result of the assessment.
    /// </summary>
    public decimal DMFSResult { get; set; }

    /// <summary>
    /// Gets or sets the comment related to the assessment.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Gets or sets the assessment model for DMFT_DMFS assessment.
    /// </summary>
    public DMFT_DMFSAssessmentModel AssessmentModel { get; set; }

    /// <summary>
    /// Gets or sets the patient examination result associated with this DMFT_DMFS assessment.
    /// </summary>
    public virtual PracticePatientExaminationResult PatientExaminationResult { get; set; }

    /// <summary>
    /// Adds a comment to the assessment.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddComment(string comment) => Comment = comment;
}

