using App.Application.AccountOperations.DTOs.Response;
using App.Application.Core;
using MediatR;

namespace App.Application.AccountOperations.Command.RefreshToken;

/// <summary>
/// Represents a command to refresh the authentication token for a user.
/// </summary>
/// <param name="UserName">The username of the user for whom the authentication token needs to be refreshed.</param>
public record RefreshTokenCommand(string UserName) : IRequest<OperationResult<UserLoginResponseDto>>;
