using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.DeleteExam;

/// <summary>
/// Command to delete an exam
/// </summary>
[OralEhrContextUnitOfWork]
public record DeleteExamCommand(Guid ExamId) : IRequest<OperationResult<Unit>>;
