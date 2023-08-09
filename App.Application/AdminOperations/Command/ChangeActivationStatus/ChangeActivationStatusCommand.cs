using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.AdminOperations.Command.ChangeActivationStatus;

/// <summary>
/// Represents a command to change the activation status of a user.
/// This command is marked with the <see cref="UserContextUnitOfWork"/> attribute,
/// indicating that it should be handled within the user context unit of work.
/// </summary>
[UserContextUnitOfWork]
public record ChangeActivationStatusCommand(string UserName) : IRequest<OperationResult<Unit>>;
