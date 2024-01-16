using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

public class ResearchGroup
{
    public ResearchGroup(string groupName, string description, string doctorId)
    {
        Id = Guid.NewGuid();
        GroupName = groupName;
        Description = description;
        DoctorId = doctorId;
    }

    public Guid Id { get; set; }
    public string GroupName { get; set; }

    public string DoctorId { get; set; }
    public ApplicationUser Doctor { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public void UpdateGroup(string newGroupName, string newGroupDescription)
    {
        GroupName = newGroupName;
        Description = newGroupDescription;
    }
}
