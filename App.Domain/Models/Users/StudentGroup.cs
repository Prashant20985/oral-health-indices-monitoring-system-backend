namespace App.Domain.Models.Users;

/// <summary>
/// Represents a relationship between a student and a group.
/// </summary>
public class StudentGroup
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentGroup"/> class.
    /// </summary>
    /// <param name="groupId">The identifier of the group to which the student belongs.</param>
    /// <param name="studentId">The identifier of the student associated with the group.</param>
    public StudentGroup(Guid groupId, string studentId)
    {
        GroupId = groupId;
        StudentId = studentId;
    }

    /// <summary>
    /// Gets or sets the identifier of the group to which the student belongs.
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the group associated with this student-group relationship.
    /// </summary>
    public Group Group { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the student associated with the group.
    /// </summary>
    public string StudentId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the student associated with this student-group relationship.
    /// </summary>
    public ApplicationUser Student { get; set; }
}
