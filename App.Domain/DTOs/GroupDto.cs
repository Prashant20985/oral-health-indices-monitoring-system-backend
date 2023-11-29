namespace App.Domain.DTOs;

/// <summary>
/// Data transfer object representing a group.
/// </summary>
public class GroupDto
{
    /// <summary>
    /// Gets or sets the Id of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the groupname of the user.
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// Gets or sets a list of students names in which group is associated
    /// </summary>
    public List<StudentDto> Students { get; set; }
}
