namespace App.Domain.DTOs.ExamDtos.Request;

public class PublishExamDto
{
    public DateTime DateOfExamination { get; set; }
    public string ExamTitle { get; set; }
    public string Description { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public TimeSpan DurationInterval { get; set; }
    public int MaxMark { get; set; }
    public Guid GroupId { get; set; }
}
