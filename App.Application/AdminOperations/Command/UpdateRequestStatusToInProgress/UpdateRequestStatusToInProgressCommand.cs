using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateRequestStatusToInProgress;
/// <summary>
/// Command to update the status of a user request to "In Progress" for administrators.
/// </summary>
[UserContextUnitOfWork]
public record UpdateRequestStatusToInProgressCommand(Guid UserRequestId)
    : IRequest<OperationResult<Unit>>;