using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.UserRequestOperations.Command.DeleteRequest;
/// <summary>
/// Represents a command to delete a new user request.
/// </summary>
[UserContextUnitOfWork]
public record DeleteRequestCommand(Guid UserRequestId)
    : IRequest<OperationResult<Unit>>;