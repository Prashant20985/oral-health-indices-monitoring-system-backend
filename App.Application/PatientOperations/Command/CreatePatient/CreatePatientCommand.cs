using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Request;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientOperations.Command.CreatePatient;

/// <summary>
/// Represents a command to create a new patient.
/// </summary>
[OralEhrContextUnitOfWork]
public record CreatePatientCommand(CreatePatientDto CreatePatient, string DoctorId)
    : IRequest<OperationResult<Unit>>;

