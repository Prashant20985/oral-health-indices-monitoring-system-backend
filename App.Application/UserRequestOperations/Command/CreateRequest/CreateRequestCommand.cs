using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.UserRequestOperations.Command.CreateRequest;
/// <summary>
/// Represents a command to create a new user request.
/// </summary>
[OralEhrContextUnitOfWork]
public record CreateRequestCommand(string RequestTitle, string Description, string CreatedBy)
    : IRequest<OperationResult<Unit>>;