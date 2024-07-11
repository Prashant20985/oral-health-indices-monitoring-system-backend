
using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByDoctor;

/// <summary>
/// Command for creating patient examination card By Doctor
/// </summary>
[OralEhrContextUnitOfWork]
public record CreatePatientExaminationCardByDoctorCommand(
    Guid PatientId,
    string DoctorId,
    CreatePatientExaminationCardByDoctorInputParams InputParams)
        : IRequest<OperationResult<PatientExaminationCardDto>>;
