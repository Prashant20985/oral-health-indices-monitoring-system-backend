namespace App.Domain.Models.OralHealthExamination;

public class PatientExaminationCard
{
    public PatientExaminationCard(Guid patientId)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
    }

    public Guid Id { get; set; }

    public string DoctorComment { get; set; }

    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; }

    public Guid RiskFactorAssesmentId { get; set; }
    public virtual RiskFactorAssessment RiskFactorAssessment { get; set; }

    public Guid? PatientExaminationRegularModeId { get; set; }
    public virtual PatientExaminationRegularMode PatientExaminationRegularMode { get; set; }

    public Guid? PatientExaminationTestModeId { get; set; }
    public virtual PatientExaminationTestMode PatientExaminationTestMode { get; set; }

    public Guid PatientExaminationResultId { get; set; }
    public virtual PatientExaminationResult PatientExaminationResult { get; set; }
}
