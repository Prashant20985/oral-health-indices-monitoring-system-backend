using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.AddPatientToResearchGroup;

/// <summary>
/// Represents a command to add a patient to a research group.
/// </summary>
[OralEhrContextUnitOfWork]
public record AddPatientToResearchGroupCommand(Guid ResearchGroupId,
    Guid PatientId) : IRequest<OperationResult<Unit>>;
