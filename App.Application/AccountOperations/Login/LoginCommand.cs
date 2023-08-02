using App.Application.AccountOperations.DTOs.Request;
using App.Application.AccountOperations.DTOs.Response;
using App.Application.Core;
using MediatR;

namespace App.Application.AccountOperations.Login;

/// <summary>
/// Record class representing the LoginCommand
/// </summary>
/// <param name="UserCredentials">The data transfer object containing neccessary information for login operation</param>
public record LoginCommand(UserCredentialsDto UserCredentials) : IRequest<OperationResult<UserLoginResponseDto>>;
