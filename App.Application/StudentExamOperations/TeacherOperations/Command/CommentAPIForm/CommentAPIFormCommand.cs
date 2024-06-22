using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIForm;

/// <summary>
/// Command for adding a comment to the API form of a practice examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentAPIFormCommand(Guid PracticeExaminationCardId,
    string DoctorComment) : IRequest<OperationResult<Unit>>;