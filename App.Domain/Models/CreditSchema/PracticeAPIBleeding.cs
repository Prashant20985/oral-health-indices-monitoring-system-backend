using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents a practice API bleeding assessment.
/// </summary>
public class PracticeAPIBleeding
{
    /// <summary>
    /// Gets or sets the unique identifier of the practice API bleeding assessment.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the API result of the assessment.
    /// </summary>
    public decimal APIResult { get; set; }

    /// <summary>
    /// Gets or sets the bleeding result of the assessment.
    /// </summary>
    public decimal BleedingResult { get; set; }

    /// <summary>
    /// Gets or sets the comments related to the assessment.
    /// </summary>
    public string Comments { get; set; }

    /// <summary>
    /// Gets or sets the assessment model for API bleeding assessment.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; set; }

    /// <summary>
    /// Gets or sets the patient examination result associated with this API bleeding assessment.
    /// </summary>
    public virtual PracticePatientExaminationResult PatientExaminationResult { get; set; }

    /// <summary>
    /// Adds a comment to the assessment.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddComment(string comment) => Comments = comment;
}

