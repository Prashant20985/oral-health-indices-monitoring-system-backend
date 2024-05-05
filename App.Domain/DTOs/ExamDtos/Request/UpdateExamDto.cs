namespace App.Domain.DTOs.ExamDtos.Request;

public class UpdateExamDto
{
    public DateTime DateOfExamination { get; set; }
    public string ExamTitle { get; set; }
    public string Description { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int Duration { get; set; }
    public int MaxMark { get; set; }
}
