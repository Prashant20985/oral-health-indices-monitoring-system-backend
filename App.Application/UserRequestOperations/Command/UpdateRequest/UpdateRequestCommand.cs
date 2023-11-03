using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.UserRequestOperations.Command.UpdateRequest;

/// <summary>
/// A command record to update a user request's title and description.
/// </summary>
[UserContextUnitOfWork]
public record UpdateRequestCommand(Guid RequestId, string Title, string Description) 
    : IRequest<OperationResult<Unit>>;
