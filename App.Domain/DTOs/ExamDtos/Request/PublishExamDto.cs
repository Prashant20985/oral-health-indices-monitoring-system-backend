namespace App.Domain.DTOs.ExamDtos.Request;
/// <summary>
///  Represents the publish exam DTO.
/// </summary>
public class PublishExamDto
{
    /// <summary>
    ///  Gets or sets the date of examination.
    /// </summary>
    public DateTime DateOfExamination { get; set; }
    
    /// <summary>
    ///  Gets or sets the title of the examination.
    /// </summary>
    public string ExamTitle { get; set; }
    
    /// <summary>
    ///  Gets or sets the description of the examination.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///  Gets or sets the start time of the examination.
    /// </summary>
    public TimeOnly StartTime { get; set; }
    
    /// <summary>
    ///  Gets or sets the end time of the examination.
    /// </summary>
    public TimeOnly EndTime { get; set; }
    
    /// <summary>
    ///  Gets or sets the duration interval of the examination.
    /// </summary>
    public TimeSpan DurationInterval { get; set; }
    
    /// <summary>
    ///  Gets or sets the maximum mark of the examination.
    /// </summary>
    public int MaxMark { get; set; }
    
    /// <summary>
    ///  Gets or sets the minimum mark of the examination.
    /// </summary>
    public Guid GroupId { get; set; }
}
