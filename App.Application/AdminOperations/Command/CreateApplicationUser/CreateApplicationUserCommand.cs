using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Request;
using MediatR;

namespace App.Application.AdminOperations.Command.CreateApplicationUser;

/// <summary>
/// Represents a command to create a new application user.
/// </summary>
public record CreateApplicationUserCommand(CreateApplicationUserRequestDto CreateApplicationUser)
    : IRequest<OperationResult<Unit>>;