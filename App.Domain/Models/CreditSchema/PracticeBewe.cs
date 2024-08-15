using App.Domain.Models.Common.Bewe;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents a practice BEWE (Basic Erosive Wear Examination) assessment.
/// </summary>
public class PracticeBewe(decimal beweResult)
{

    /// <summary>
    /// Gets or sets the unique identifier of the practice BEWE assessment.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the BEWE result of the assessment.
    /// </summary>
    public decimal BeweResult { get; private set; } = beweResult;

    /// <summary>
    /// Gets or sets the sectant 1 value of the assessment.
    /// </summary>
    public decimal Sectant1 { get; private set; }

    /// <summary>
    /// Gets or sets the sectant 2 value of the assessment.
    /// </summary>
    public decimal Sectant2 { get; private set; }

    /// <summary>
    /// Gets or sets the sectant 3 value of the assessment.
    /// </summary>
    public decimal Sectant3 { get; private set; }

    /// <summary>
    /// Gets or sets the sectant 4 value of the assessment.
    /// </summary>
    public decimal Sectant4 { get; private set; }

    /// <summary>
    /// Gets or sets the sectant 5 value of the assessment.
    /// </summary>
    public decimal Sectant5 { get; private set; }

    /// <summary>
    /// Gets or sets the sectant 6 value of the assessment.
    /// </summary>
    public decimal Sectant6 { get; private set; }

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

    public void SetAssessmentModel(BeweAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;

    public void SetSectant1(decimal sectant1) => Sectant1 = sectant1;

    public void SetSectant2(decimal sectant2) => Sectant2 = sectant2;

    public void SetSectant3(decimal sectant3) => Sectant3 = sectant3;

    public void SetSectant4(decimal sectant4) => Sectant4 = sectant4;

    public void SetSectant5(decimal sectant5) => Sectant5 = sectant5;

    public void SetSectant6(decimal sectant6) => Sectant6 = sectant6;
}

