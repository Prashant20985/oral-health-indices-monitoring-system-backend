using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.AdminOperations.Command.DeleteApplicationUser;

/// <summary>
/// Represents a command to delete an application user.
/// This command is marked with the <see cref="UserContextUnitOfWork"/> attribute,
/// indicating that it should be handled within the user context unit of work.
/// </summary>
[UserContextUnitOfWork]
public record DeleteApplicationUserCommand(
    string UserName,
    string DeleteComment) : IRequest<OperationResult<Unit>>;
