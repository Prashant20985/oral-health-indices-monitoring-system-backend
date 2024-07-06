using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Request;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateApplicationUser;

/// <summary>
/// Represents a command to update an application user.
/// This command is marked with the <see cref="UserContextUnitOfWork"/> attribute,
/// indicating that it should be handled within the user context unit of work.
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateApplicationUserCommand(string UserName,
    UpdateApplicationUserRequestDto UpdateApplicationUser) : IRequest<OperationResult<Unit>>;
