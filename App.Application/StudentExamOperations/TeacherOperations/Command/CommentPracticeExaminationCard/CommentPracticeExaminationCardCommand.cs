using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentPracticeExaminationCard;

/// <summary>
/// Command for adding a comment to a practice examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentPracticeExaminationCardCommand(Guid PracticeExaminationCardId,
    string DoctorComment) : IRequest<OperationResult<Unit>>;