using App.Domain.Models.Users;

namespace App.Domain.Models.OralHealthExamination;

/// <summary>
/// Represents a research group with details about the group name, description, associated doctor, and patients.
/// </summary>
public class ResearchGroup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResearchGroup"/> class.
    /// </summary>
    /// <param name="groupName">The name of the research group.</param>
    /// <param name="description">The description of the research group.</param>
    /// <param name="doctorId">The ID of the doctor associated with the research group.</param>
    public ResearchGroup(string groupName, string description, string doctorId)
    {
        Id = Guid.NewGuid();
        GroupName = groupName;
        Description = description;
        DoctorId = doctorId;
    }

    /// <summary>
    /// Gets or sets the unique identifier for the research group.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the research group.
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// Gets or sets the ID of the doctor associated with the research group.
    /// </summary>
    public string DoctorId { get; set; }

    /// <summary>
    /// Gets or sets the doctor associated with the research group.
    /// </summary>
    public ApplicationUser Doctor { get; set; }

    /// <summary>
    /// Gets or sets the description of the research group.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets the date and time when the research group was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of patients in the research group.
    /// </summary>
    public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    /// <summary>
    /// Updates the research group's name and description.
    /// </summary>
    /// <param name="newGroupName">The new name of the research group.</param>
    /// <param name="newGroupDescription">The new description of the research group.</param>
    public void UpdateGroup(string newGroupName, string newGroupDescription)
    {
        GroupName = newGroupName;
        Description = newGroupDescription;
    }
}