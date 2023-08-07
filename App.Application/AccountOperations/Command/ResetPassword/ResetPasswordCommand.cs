using App.Application.AccountOperations.DTOs.Request;
using App.Application.Core;
using MediatR;

namespace App.Application.AccountOperations.Command.ResetPassword;

/// <summary>
/// Represents a command to reset a user's password.
/// </summary>
/// <param name="ResetPassword">The data transfer object containing neccessary information for reset password operation</param>
public record ResetPasswordCommand(ResetPasswordDto ResetPassword) : IRequest<OperationResult<Unit>>;
