using App.Application.AccountOperations.DTOs.Response;
using App.Application.Core;
using MediatR;

namespace App.Application.AccountOperations.Query.CurrentUser;

/// <summary>
/// Record class representing the CurrentUserQuery
/// </summary>
/// <param name="UserName">Current user's user name</param>
public record CurrentUserQuery(string UserName) : IRequest<OperationResult<UserLoginResponseDto>>;
