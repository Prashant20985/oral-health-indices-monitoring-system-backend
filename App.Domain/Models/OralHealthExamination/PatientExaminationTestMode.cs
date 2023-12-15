using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

public class PatientExaminationTestMode
{
    public PatientExaminationTestMode(string doctorId, string studentId)
    {
        Id = Guid.NewGuid();
        DoctorId = doctorId;
        StudentId = studentId;
    }

    public Guid Id { get; set; }
    public string StudentId { get; set; }
    public virtual ApplicationUser Student { get; set; }

    public string DoctorId { get; set; }
    public virtual ApplicationUser Doctor { get; set; }

    public DateTime DateOfExamination { get; set; } = DateTime.UtcNow;
    public decimal StudentMarks { get; set; } = 0.0m;

    public PatientExaminationCard PatientExaminationCard { get; set; }
}
