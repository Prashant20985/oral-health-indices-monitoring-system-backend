using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

public class PatientExaminationRegularMode
{
    public PatientExaminationRegularMode(string doctorId)
    {
        Id = Guid.NewGuid();
        DoctorId = doctorId;
    }

    public Guid Id { get; set; }

    public string DoctorId { get; set; }
    public ApplicationUser Doctor { get; set; }

    public DateTime DateOfExamination { get; set; } = DateTime.UtcNow;

    public PatientExaminationCard PatientExaminationCard { get; set; }
}
