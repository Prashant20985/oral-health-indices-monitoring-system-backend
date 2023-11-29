namespace App.Domain.Models.Users;

/// <summary>
/// Represents a group of students led by a teacher.
/// </summary>
public class Group
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Group"/> class.
    /// </summary>
    /// <param name="teacherId">The identifier of the teacher associated with the group.</param>
    /// <param name="groupName">The name of the group.</param>
    public Group(string teacherId, string groupName)
    {
        Id = Guid.NewGuid();
        TeacherId = teacherId;
        GroupName = groupName;
    }

    /// <summary>
    /// Gets the unique identifier of the group.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    public string GroupName { get; private set; }

    /// <summary>
    /// Gets the identifier of the teacher associated with the group.
    /// </summary>
    public string TeacherId { get; private set; }

    /// <summary>
    /// Gets or sets the teacher user associated with the group.
    /// </summary>
    public ApplicationUser Teacher { get; set; }

    /// <summary>
    /// Updates the name of the group.
    /// </summary>
    /// <param name="groupName">The new name of the group.</param>
    public void UpdateGroupName(string groupName) => GroupName = groupName;

    /// <summary>
    /// Gets or sets the collection of StudentGroup objects associated with the group.
    /// </summary>
    public ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
}
