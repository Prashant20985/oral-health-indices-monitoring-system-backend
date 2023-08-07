using App.Application.Core;
using MediatR;

namespace App.Application.AdminOperations.Command.ChangeActivationStatus;

/// <summary>
/// Represents a command to change the activation status of a user.
/// </summary>
public record ChangeActivationStatusCommand(string UserName) : IRequest<OperationResult<Unit>>;

