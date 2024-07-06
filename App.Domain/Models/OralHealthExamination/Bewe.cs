using App.Domain.Models.Common.Bewe;

namespace App.Domain.Models.OralHealthExamination;

public class Bewe
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal BeweResult { get; private set; }
    public string DoctorComment { get; private set; }
    public string StudentComment { get; private set; }
    public BeweAssessmentModel AssessmentModel { get; private set; }
    public PatientExaminationResult PatientExaminationResult { get; set; }

    public void AddDoctorComment(string comment) => DoctorComment = comment;

    public void AddStudentComment(string comment) => StudentComment = comment;

    public void SetBeweResult(decimal beweResult) => BeweResult = beweResult;

    public void SetAssessmentModel(BeweAssessmentModel assessmentModel) => AssessmentModel = assessmentModel;

    public void CalculateBeweResult()
    {
        var sectant1MaxValue = AssessmentModel.Sectant1.FindMaxValue();
        var sectant2MaxValue = AssessmentModel.Sectant2.FindMaxValue();
        var sectant3MaxValue = AssessmentModel.Sectant3.FindMaxValue();
        var sectant4MaxValue = AssessmentModel.Sectant4.FindMaxValue();
        var sectant5MaxValue = AssessmentModel.Sectant5.FindMaxValue();
        var sectant6MaxValue = AssessmentModel.Sectant6.FindMaxValue();

        BeweResult = sectant1MaxValue
            + sectant2MaxValue
            + sectant3MaxValue
            + sectant4MaxValue
            + sectant5MaxValue
            + sectant6MaxValue;
    }
}
