using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.RemoveStudentFromGroup;

/// <summary>
/// Represents a command to remove a student from a group.
/// </summary>
[UserContextUnitOfWork]
public record RemoveStudentFromGroupCommand(Guid GroupId, string StudentId)
    : IRequest<OperationResult<Unit>>;
