using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentBeweForm;

/// <summary>
/// Command for adding a comment to the BEWE form of a practice examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentBeweFormCommand(Guid PracticeExaminationCardId,
    string DoctorComment) : IRequest<OperationResult<Unit>>;
