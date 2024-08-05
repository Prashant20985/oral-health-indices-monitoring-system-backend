using App.Domain.DTOs.ApplicationUserDtos.Response;

namespace App.Domain.DTOs.StudentGroupDtos.Response;

/// <summary>
/// Class representing a paginated response of students not in a group.
/// </summary>
public class PaginatedStudentnotInGroupResponseDto
{
    /// <summary>
    /// Gets or sets the total number of students.
    /// </summary>
    public int TotalStudents { get; set; }

    /// <summary>
    /// Gets or sets the students.
    /// </summary>
    public List<StudentResponseDto> Students { get; set; }
}
