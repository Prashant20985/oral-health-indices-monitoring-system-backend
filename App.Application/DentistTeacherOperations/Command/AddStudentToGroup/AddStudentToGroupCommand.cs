using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.AddStudentToGroup;

/// <summary>
/// Represents a command to add a student to a group.
/// </summary>
[UserContextUnitOfWork]
public record AddStudentToGroupCommand(Guid GroupId, string StudentId)
    : IRequest<OperationResult<Unit>>;

