using App.Application.AccountOperations.DTOs.Request;
using App.Application.Core;
using MediatR;

namespace App.Application.AccountOperations.ChangePassword;

/// <summary>
/// Record class representing the ChangePasswordCommand with the ChangePasswordDto payload
/// </summary>
/// <param name="ChangePassword">The data transfer object containing the necessary information for changing the password.</param>
public record ChangePasswordCommand(ChangePasswordDto ChangePassword) : IRequest<OperationResult<Unit>>;
