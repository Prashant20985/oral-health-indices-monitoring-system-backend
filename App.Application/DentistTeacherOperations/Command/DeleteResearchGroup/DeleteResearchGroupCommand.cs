using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;
namespace App.Application.DentistTeacherOperations.Command.DeleteResearchGroup;

/// <summary>
/// Represents a command to delete a research group.
/// </summary>
[OralEhrContextUnitOfWork]
public record DeleteResearchGroupCommand(Guid ResearchGroupId)
    : IRequest<OperationResult<Unit>>;
