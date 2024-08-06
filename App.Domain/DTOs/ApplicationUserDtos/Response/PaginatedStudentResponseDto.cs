namespace App.Domain.DTOs.ApplicationUserDtos.Response;

/// <summary>
/// Class representing a paginated response of students not in a group.
/// </summary>
public class PaginatedStudentResponseDto
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
