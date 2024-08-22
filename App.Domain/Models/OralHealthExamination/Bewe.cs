using App.Domain.Models.Common.Bewe;

namespace App.Domain.Models.OralHealthExamination;

public class Bewe
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal BeweResult { get; private set; }

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
    ///  Gets or sets the Doctor comment.
    /// </summary>
    public string DoctorComment { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Student comment.
    /// </summary>
    public string StudentComment { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Assessment model.
    /// </summary>
    public BeweAssessmentModel AssessmentModel { get; private set; }
    
    /// <summary>
    ///  Gets or sets the Patient examination result.
    /// </summary>
    public PatientExaminationResult PatientExaminationResult { get; set; }
    
    /// <summary>
    /// Method to add doctor comment.
    /// </summary>
    /// <param name="comment"></param>
    public void AddDoctorComment(string comment) => DoctorComment = comment;

    /// <summary>
    /// method to add student comment.
    /// </summary>
    /// <param name="comment"></param>
    public void AddStudentComment(string comment) => StudentComment = comment;

    /// <summary>
    ///  Method to set the assessment model.
    /// </summary>
    /// <param name="beweResult"></param>
    public void SetBeweResult(decimal beweResult) => BeweResult = beweResult;

    /// <summary>
    ///  Method to set the assessment model.
    /// </summary>
    /// <param name="assessmentModel"></param>
    public void SetAssessmentModel(BeweAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;

    /// <summary>
    ///  Method to calculate the BEWE result.
    /// </summary>
    public void CalculateBeweResult()
    {
        var sectant1MaxValue = AssessmentModel.Sectant1.FindMaxValue();
        var sectant2MaxValue = AssessmentModel.Sectant2.FindMaxValue();
        var sectant3MaxValue = AssessmentModel.Sectant3.FindMaxValue();
        var sectant4MaxValue = AssessmentModel.Sectant4.FindMaxValue();
        var sectant5MaxValue = AssessmentModel.Sectant5.FindMaxValue();
        var sectant6MaxValue = AssessmentModel.Sectant6.FindMaxValue();

        Sectant1 = sectant1MaxValue;
        Sectant2 = sectant2MaxValue;
        Sectant3 = sectant3MaxValue;
        Sectant4 = sectant4MaxValue;
        Sectant5 = sectant5MaxValue;
        Sectant6 = sectant6MaxValue;

        BeweResult = sectant1MaxValue
            + sectant2MaxValue
            + sectant3MaxValue
            + sectant4MaxValue
            + sectant5MaxValue
            + sectant6MaxValue;
    }
}
