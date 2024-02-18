using App.Domain.Models.Common.Bewe;

namespace App.Domain.Models.OralHealthExamination;

public class Bewe
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal BeweResult { get; set; }
    public string Comment { get; private set; }
    public BeweAssessmentModel AssessmentModel { get; set; }
    public PatientExaminationResult PatientExaminationResult { get; set; }

    public void AddComment(string comment) => Comment = comment;
}
