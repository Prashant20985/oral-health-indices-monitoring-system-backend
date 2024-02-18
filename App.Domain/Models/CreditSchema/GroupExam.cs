using App.Domain.Models.Users;

namespace App.Domain.Models.CreditSchema;

/// <summary>
/// Represents a group associated with an exam.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="GroupExam"/> class.
/// </remarks>
/// <param name="groupId">The unique identifier of the group.</param>
/// <param name="examId">The unique identifier of the exam.</param>
public class GroupExam(Guid groupId, Guid examId)
{

    /// <summary>
    /// Gets the unique identifier of the group.
    /// </summary>
    public Guid GroupId { get; private set; } = groupId;

    /// <summary>
    /// Gets or sets the group associated with this group exam.
    /// </summary>
    public virtual Group Group { get; set; }

    /// <summary>
    /// Gets the unique identifier of the exam.
    /// </summary>
    public Guid ExamId { get; private set; } = examId;

    /// <summary>
    /// Gets or sets the exam associated with this group exam.
    /// </summary>
    public virtual Exam Exam { get; set; }
}

