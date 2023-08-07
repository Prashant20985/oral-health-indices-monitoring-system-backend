using App.Application.Core;
using MediatR;

namespace App.Application.AdminOperations.Command.DeleteApplicationUser;

/// <summary>
/// Represents a command to delete an application user.
/// </summary>
public record DeleteApplicationUserCommand(
    string UserName,
    string DeleteComment) : IRequest<OperationResult<Unit>>;
