using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

public class PatientExaminationCard(Guid patientId)
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime DateOfExamination { get; private set; } = DateTime.UtcNow;

    public bool IsRegularMode { get; private set; }

    public string DoctorComment { get; private set; }

    public string StudentComment { get; private set; }

    public decimal? TotalScore { get; private set; }

    public string NeedForDentalInterventions { get; private set; }

    public string ProposedTreatment { get; private set; }

    public string Description { get; private set; }

    public string PatientRecommendations { get; private set; }

    public Guid PatientId { get; private set; } = patientId;
    public virtual Patient Patient { get; set; }

    public Guid RiskFactorAssesmentId { get; private set; }
    public virtual RiskFactorAssessment RiskFactorAssessment { get; set; }

    public string DoctorId { get; private set; }
    public virtual ApplicationUser Doctor { get; set; }

    public string? StudentId { get; private set; }
    public virtual ApplicationUser Student { get; set; }

    public Guid PatientExaminationResultId { get; private set; }
    public virtual PatientExaminationResult PatientExaminationResult { get; set; }

    public void SetTestMode() => IsRegularMode = false;

    public void SetRegularMode() => IsRegularMode = true;

    public void SetDoctorId(string doctorId) => DoctorId = doctorId;

    public void SetStudentId(string studentId) => StudentId = studentId;

    public void SetRiskFactorAssesmentId(Guid riskFactorAssesmentId) => RiskFactorAssesmentId = riskFactorAssesmentId;

    public void SetPatientExaminationResultId(Guid patientExaminationResultId) => PatientExaminationResultId = patientExaminationResultId;

    public void SetTotalScore(decimal totalScore) => TotalScore = totalScore;

    public void AddDoctorComment(string comment) => DoctorComment = comment;
    public void AddStudentComment(string comment) => StudentComment = comment;
}
