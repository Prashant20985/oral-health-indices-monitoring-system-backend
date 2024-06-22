using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

public class PatientExaminationCard(Guid patientId)
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime DateOfExamination { get; private set; } = DateTime.UtcNow;

    public bool IsRegularMode { get; set; }

    public string DoctorComment { get; set; }

    public decimal? TotalScore { get; set; }

    public Guid PatientId { get; set; } = patientId;
    public virtual Patient Patient { get; set; }

    public Guid RiskFactorAssesmentId { get; set; }
    public virtual RiskFactorAssessment RiskFactorAssessment { get; set; }

    public string DoctorId { get; set; }
    public virtual ApplicationUser Doctor { get; set; }

    public string? StudentId { get; set; }
    public virtual ApplicationUser Student { get; set; }

    public Guid PatientExaminationResultId { get; set; }
    public virtual PatientExaminationResult PatientExaminationResult { get; set; }

    public void SetTestMode() => IsRegularMode = false;

    public void SetRegularMode() => IsRegularMode = true;

    public void SetDoctorComment(string comment) => DoctorComment = comment;
}
