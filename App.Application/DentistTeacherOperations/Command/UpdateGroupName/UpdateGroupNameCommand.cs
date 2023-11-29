using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.UpdateGroupName;

/// <summary>
/// Represents a command to update name of a  group.
/// </summary>
[UserContextUnitOfWork]
public record UpdateGroupNameCommand(Guid GroupId, string GroupName) : IRequest<OperationResult<Unit>>;
