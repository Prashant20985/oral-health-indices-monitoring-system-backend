using App.Domain.Models.Common.APIBleeding;

namespace App.Domain.Models.OralHealthExamination;

public class APIBleeding
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public decimal APIResult { get; private set; }
    public decimal BleedingResult { get; private set; }
    public string Comments { get; private set; }
    public APIBleedingAssessmentModel AssessmentModel { get; set; }
    public PatientExaminationResult PatientExaminationResult { get; set; }

    public void AddComment(string comment) => Comments = comment;
}
