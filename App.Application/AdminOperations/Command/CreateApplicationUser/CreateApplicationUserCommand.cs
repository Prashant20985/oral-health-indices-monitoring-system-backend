using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.AdminOperations.Command.CreateApplicationUser;

/// <summary>
/// Represents a command to create a new application user.
/// </summary>
public record CreateApplicationUserCommand(CreateApplicationUserDto CreateApplicationUser)
    : IRequest<OperationResult<Unit>>;