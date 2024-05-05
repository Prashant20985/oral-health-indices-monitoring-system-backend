using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.MarkExamAsGraded;

/// <summary>
/// Command to mark an exam as graded.
/// </summary>
[OralEhrContextUnitOfWork]
public record MarkAsGradedCommand(Guid ExamId) : IRequest<OperationResult<Unit>>;
