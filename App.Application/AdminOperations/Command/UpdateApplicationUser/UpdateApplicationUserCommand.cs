using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateApplicationUser;

/// <summary>
/// Represents a command to update an application user.
/// </summary>
public record UpdateApplicationUserCommand(string UserName,
    UpdateApplicationUserDto UpdateApplicationUser) : IRequest<OperationResult<Unit>>;
