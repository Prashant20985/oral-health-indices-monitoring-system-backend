using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientOperations.Command.DeletePatient;

/// <summary>
/// Represents a command to delete a patient in the Oral EHR context.
/// </summary>
[OralEhrContextUnitOfWork]
public record DeletePatientCommand(Guid PatientId) : IRequest<OperationResult<Unit>>;

