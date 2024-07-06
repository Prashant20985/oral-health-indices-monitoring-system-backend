using App.Application.Core;
using App.Domain.DTOs.ResearchGroupDtos.Request;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.CreateResearchGroup;

/// <summary>
/// Represents a command to create a new research group.
/// </summary>
[OralEhrContextUnitOfWork]
public record CreateResearchGroupCommand(
    CreateUpdateResearchGroupRequestDto CreateResearchGroup,
    string DoctorId) : IRequest<OperationResult<Unit>>;
