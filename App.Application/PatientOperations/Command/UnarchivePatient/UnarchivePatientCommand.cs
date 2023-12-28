using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientOperations.Command.UnarchivePatient;

/// <summary>
/// Represents a command to unarchive a patient in the Oral EHR context.
/// </summary>
[OralEhrContextUnitOfWork]
public record UnarchivePatientCommand(Guid PatientId) : IRequest<OperationResult<Unit>>;
