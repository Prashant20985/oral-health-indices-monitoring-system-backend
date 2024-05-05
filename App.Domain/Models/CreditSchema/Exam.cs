using App.Domain.DTOs.ExamDtos.Request;
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
    int maxMark,
    Guid groupId)
{

    /// <summary>
    /// Gets or sets the unique identifier of the examination.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the date of the examination.
    /// </summary>
    public DateTime DateOfExamination { get; private set; } = dateOfExamination;

    /// <summary>
    /// Gets or sets the title of the examination.
    /// </summary>
    public string ExamTitle { get; private set; } = examTitle;

    /// <summary>
    /// Gets or sets the description of the examination.
    /// </summary>
    public string Description { get; private set; } = description;

    /// <summary>
    /// Gets or sets the date and time when the examination is published.
    /// </summary>
    public DateTime PublishDate { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the start time of the examination.
    /// </summary>
    public TimeOnly StartTime { get; private set; } = startTime;

    /// <summary>
    /// Gets or sets the end time of the examination.
    /// </summary>
    public TimeOnly EndTime { get; private set; } = endTime;

    /// <summary>
    /// Gets or sets the duration of the examination in minutes.
    /// </summary>
    public int Duration { get; private set; } = duration;

    /// <summary>
    /// Gets or sets the maximum mark of the examination.
    /// </summary>
    public int MaxMark { get; private set; } = maxMark;

    /// <summary>
    /// Gets or sets the status of the examination.
    /// </summary>
    public ExamStatus ExamStatus { get; private set; } = ExamStatus.Published;

    /// <summary>
    /// Gets or sets the unique identifier of the group associated with the examination.
    /// </summary>
    public Guid GroupId { get; private set; } = groupId;
    public virtual Group Group { get; set; }

    /// <summary>
    /// Gets or sets the collection of practice patient examination cards associated with this examination.
    /// </summary>
    public ICollection<PracticePatientExaminationCard> PracticePatientExaminationCards { get; set; } = new List<PracticePatientExaminationCard>();

    public void MarksAsGraded() => ExamStatus = ExamStatus.Graded;

    public void UpdateExam(UpdateExamDto updateExam)
    {
        DateOfExamination = updateExam.DateOfExamination;
        ExamTitle = updateExam.ExamTitle;
        Description = updateExam.Description;
        StartTime = updateExam.StartTime;
        EndTime = updateExam.EndTime;
        Duration = updateExam.Duration;
        MaxMark = updateExam.MaxMark;
    }
}
