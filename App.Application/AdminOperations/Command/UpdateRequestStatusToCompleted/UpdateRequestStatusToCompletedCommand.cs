using App.Application.Core;
using App.Domain.DTOs;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.AdminOperations.Command.UpdateRequestStatusToCompleted;
/// <summary>
/// Command to update the status of a user request to "Completed" for administrators with comment.
/// </summary>
[UserContextUnitOfWork]
public record UpdateRequestStatusToCompletedCommand(
        Guid UserRequestId, 
        string AdminComment) : IRequest<OperationResult<Unit>>;