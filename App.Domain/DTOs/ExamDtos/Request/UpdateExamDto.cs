namespace App.Domain.DTOs.ExamDtos.Request;
/// <summary>
///     Represents the Update Exam DTO.
/// </summary>
public class UpdateExamDto
{
    /// <summary>
    ///   Gets or sets the unique identifier for the exam.
    /// </summary>
    public DateTime DateOfExamination { get; set; }
    
    /// <summary>
    ///  Gets or sets the title of the exam.
    /// </summary>
    public string ExamTitle { get; set; }
    
    /// <summary>
    ///   Gets or sets the description of the exam.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///  Gets or sets the start time of the exam.
    /// </summary>
    public TimeOnly StartTime { get; set; }
    
    /// <summary>
    ///  Gets or sets the end time of the exam.
    /// </summary>
    public TimeOnly EndTime { get; set; }
    
    /// <summary>
    ///  Gets or sets the duration interval of the exam.
    /// </summary>
    public TimeSpan DurationInterval { get; set; }
    
    /// <summary>
    ///  Gets or sets the maximum mark of the exam.
    /// </summary>
    public int MaxMark { get; set; }
}
