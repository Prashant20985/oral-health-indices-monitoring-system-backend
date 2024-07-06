using App.Domain.DTOs.ApplicationUserDtos.Response;

namespace App.Domain.DTOs.StudentGroupDtos.Response;

/// <summary>
/// Data transfer object representing a group.
/// </summary>
public class StudentGroupResponseDto
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
    public List<StudentResponseDto> Students { get; set; }
}
