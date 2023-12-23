using App.Application.Core;
using App.Domain.DTOs;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.UpdateResearchGroup;

/// <summary>
/// Represents a command to update the name of a research group.
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdateResearchGroupCommand(Guid ResearchGroupId,
    CreateUpdateResearchGroupDto UpdateResearchGroup) : IRequest<OperationResult<Unit>>;
