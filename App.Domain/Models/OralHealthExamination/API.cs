using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Models.OralHealthExamination;

public class API
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int APIResult { get; private set; }
    public int Maxilla { get; set; }
    public int Mandible { get; set; }
    public string Comment { get; private set; }
    public APIBleedingAssessmentModel AssessmentModel { get; set; }
    public PatientExaminationResult PatientExaminationResult { get; set; }

    public void AddComment(string comment) => Comment = comment;
}
