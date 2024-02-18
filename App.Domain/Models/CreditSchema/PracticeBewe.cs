using App.Domain.Models.Common.Bewe;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents a practice BEWE (Basic Erosive Wear Examination) assessment.
/// </summary>
public class PracticeBewe
{
    /// <summary>
    /// Gets or sets the unique identifier of the practice BEWE assessment.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the BEWE result of the assessment.
    /// </summary>
    public decimal BeweResult { get; set; }

    /// <summary>
    /// Gets or sets the comment related to the assessment.
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Gets or sets the assessment model for BEWE assessment.
    /// </summary>
    public BeweAssessmentModel AssessmentModel { get; set; }

    /// <summary>
    /// Gets or sets the patient examination result associated with this BEWE assessment.
    /// </summary>
    public virtual PracticePatientExaminationResult PatientExaminationResult { get; set; }

    /// <summary>
    /// Adds a comment to the assessment.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddComment(string comment) => Comment = comment;
}

