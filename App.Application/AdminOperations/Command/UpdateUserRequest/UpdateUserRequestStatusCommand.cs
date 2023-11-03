using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateUserRequest;

/// <summary>
/// Represents a command to update an request staus and admin comment of user user request.
/// </summary>
[UserContextUnitOfWork]
public record UpdateUserRequestStatusCommand(Guid UserRequestId, string RequestStatus, string AdminComment) : IRequest<OperationResult<Unit>>;
