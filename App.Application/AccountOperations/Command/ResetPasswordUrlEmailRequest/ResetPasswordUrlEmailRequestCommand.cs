using App.Application.Core;
using MediatR;

namespace App.Application.AccountOperations.Command.ResetPasswordUrlEmailRequest;

/// <summary>
/// Represents a command to request a password reset URL through email.
/// </summary>
/// <param name="Email">The email to which reset passowrd url is sent.</param>
public record ResetPasswordUrlEmailRequestCommand(string Email) : IRequest<OperationResult<Unit>>;

