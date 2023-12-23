using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.RemovePatientFromResearchGroup;

/// <summary>
/// Represents a command to remove a patient from a research group.
/// </summary>
[OralEhrContextUnitOfWork]
public record RemovePatientFromResearchGroupCommand(Guid PatientId)
    : IRequest<OperationResult<Unit>>;
