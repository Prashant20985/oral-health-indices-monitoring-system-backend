using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Request;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientOperations.Command.UpdatePatient;

/// <summary>
/// Represents a command to update patient information in the Oral EHR context.
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdatePatientCommand(Guid PatientId, UpdatePatientDto UpdatePatient)
    : IRequest<OperationResult<Unit>>;
