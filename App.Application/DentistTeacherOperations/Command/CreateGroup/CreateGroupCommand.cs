using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.CreateGroup;

/// <summary>
/// Represents a command to create a new group.
/// </summary>
[UserContextUnitOfWork]
public record CreateGroupCommand(string GroupName, string TeacherId) : IRequest<OperationResult<Unit>>;
