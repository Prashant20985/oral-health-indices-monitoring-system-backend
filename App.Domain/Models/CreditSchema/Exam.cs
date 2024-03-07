using App.Domain.Models.Enums;
using App.Domain.Models.Users;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents an examination.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Exam"/> class.
/// </remarks>
/// <param name="dateOfExamination">The date of the examination.</param>
/// <param name="examTitle">The title of the examination.</param>
/// <param name="description">The description of the examination.</param>
/// <param name="startTime">The start time of the examination.</param>
/// <param name="endTime">The end time of the examination.</param>
/// <param name="duration">The duration of the examination in minutes.</param>
/// <param name="maxMark">The maximum mark of the examination.</param>
public class Exam(DateTime dateOfExamination,
    string examTitle,
    string description,
    TimeOnly startTime,
    TimeOnly endTime,
    int duration,
    int maxMark)
{

    /// <summary>
    /// Gets or sets the unique identifier of the examination.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the date of the examination.
    /// </summary>
    public DateTime DateOfExamination { get; set; } = dateOfExamination;

    /// <summary>
    /// Gets or sets the title of the examination.
    /// </summary>
    public string ExamTitle { get; set; } = examTitle;

    /// <summary>
    /// Gets or sets the description of the examination.
    /// </summary>
    public string Description { get; set; } = description;

    /// <summary>
    /// Gets or sets the date and time when the examination is published.
    /// </summary>
    public DateTime PublishDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the start time of the examination.
    /// </summary>
    public TimeOnly StartTime { get; set; } = startTime;

    /// <summary>
    /// Gets or sets the end time of the examination.
    /// </summary>
    public TimeOnly EndTime { get; set; } = endTime;

    /// <summary>
    /// Gets or sets the duration of the examination in minutes.
    /// </summary>
    public int Duration { get; set; } = duration;

    /// <summary>
    /// Gets or sets the maximum mark of the examination.
    /// </summary>
    public int MaxMark { get; set; } = maxMark;

    /// <summary>
    /// Gets or sets the status of the examination.
    /// </summary>
    public ExamStatus ExamStatus { get; set; } = ExamStatus.Published;

    /// <summary>
    /// Gets or sets the unique identifier of the group associated with the examination.
    /// </summary>
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }

    /// <summary>
    /// Gets or sets the collection of practice patient examination cards associated with this examination.
    /// </summary>
    public ICollection<PracticePatientExaminationCard> PracticePatientExaminationCards { get; set; } = new List<PracticePatientExaminationCard>();
}
