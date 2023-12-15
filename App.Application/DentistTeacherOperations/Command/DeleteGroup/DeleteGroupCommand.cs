using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.DeleteGroup;

/// <summary>
/// Represents command to delete a group
/// </summary>
/// <param name="GroupId"></param>
[OralEhrContextUnitOfWork]
public record DeleteGroupCommand(Guid GroupId) : IRequest<OperationResult<Unit>>;
