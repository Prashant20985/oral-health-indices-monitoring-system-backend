using App.Domain.DTOs.ExamDtos.Response;

namespace App.Domain.DTOs.StudentGroupDtos.Response;

public class StudentGroupWithExamsListResponseDto
{
    /// <summary>
    /// Gets or sets the group id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the group name.
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// Gets or sets the teacher name.
    /// </summary>
    public string Teacher { get; set; }

    /// <summary>
    /// Gets or sets the exams.
    /// </summary>
    public List<ExamDto> Exams { get; set; }
}
