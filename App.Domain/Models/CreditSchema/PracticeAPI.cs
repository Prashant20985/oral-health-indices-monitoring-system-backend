using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Models.CreditSchema;

public class PracticeAPI(int aPIResult, int maxilla, int mandible)
{
    /// <summary>
    /// Gets the unique identifier of the practice bleeding assessment.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets the bleeding result of the assessment.
    /// </summary>
    public int APIResult { get; private set; } = aPIResult;

    /// <summary>
    /// Gets the API result of the maxilla.
    /// </summary>
    public int Maxilla { get; private set; } = maxilla;

    /// <summary>
    /// Gets the API result of the mandible.
    /// </summary>
    public int Mandible { get; private set; } = mandible;

    /// <summary>
    /// Gets the comments related to the assessment.
    /// </summary>
    public string Comment { get; private set; }

    /// <summary>
    /// Gets the assessment model for bleeding assessment.
    /// </summary>
    public APIBleedingAssessmentModel AssessmentModel { get; private set; }

    /// <summary>
    /// Gets the patient examination result associated with this bleeding assessment.
    /// </summary>
    public virtual PracticePatientExaminationResult PatientExaminationResult { get; set; }

    /// <summary>
    /// Adds a comment to the assessment.
    /// </summary>
    /// <param name="comment">The comment to add.</param>
    public void AddComment(string comment) => Comment = comment;

    /// <summary>
    /// Adds an assessment model to the assessment.
    /// </summary
    /// <param name="assessmentModel">The assessment model to add.</param>
    public void SetAssessmentModel(APIBleedingAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;
}
