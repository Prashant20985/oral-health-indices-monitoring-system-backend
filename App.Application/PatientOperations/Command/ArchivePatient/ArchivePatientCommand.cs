using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientOperations.Command.ArchivePatient;

/// <summary>
/// Represents a command to archive a patient in the Oral EHR context.
/// </summary>
[OralEhrContextUnitOfWork]
public record ArchivePatientCommand(Guid PatientId, string ArchiveComment)
    : IRequest<OperationResult<Unit>>;

