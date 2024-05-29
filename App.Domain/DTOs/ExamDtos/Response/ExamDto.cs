namespace App.Domain.DTOs.ExamDtos.Response;

public class ExamDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the examination.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the date of the examination.
    /// </summary>
    public DateTime DateOfExamination { get; set; }

    /// <summary>
    /// Gets or sets the title of the examination.
    /// </summary>
    public string ExamTitle { get; set; }

    /// <summary>
    /// Gets or sets the description of the examination.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the examination is published.
    /// </summary>
    public DateTime PublishDate { get; set; }

    /// <summary>
    /// Gets or sets the start time of the examination.
    /// </summary>
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// Gets or sets the end time of the examination.
    /// </summary>
    public TimeOnly EndTime { get; set; }

    /// <summary>
    /// Gets or sets the duration of the examination in minutes.
    /// </summary>
    public TimeSpan DurationInterval { get; set; }

    /// <summary>
    /// Gets or sets the maximum mark of the examination.
    /// </summary>
    public int MaxMark { get; set; }

    /// <summary>
    /// Gets or sets the status of the examination.
    /// </summary>
    public string ExamStatus { get; set; }
}
