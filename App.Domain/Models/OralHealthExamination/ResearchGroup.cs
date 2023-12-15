using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

public class ResearchGroup
{
    public ResearchGroup(string groupName)
    {
        Id = Guid.NewGuid();
        GroupName = groupName;
    }

    public Guid Id { get; set; }
    public string GroupName { get; set; }

    public string DoctorId { get; set; }
    public ApplicationUser Doctor { get; set; }

    public ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
