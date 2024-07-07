using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

/// <summary>
/// Command for creating a patient examination card in test mode
/// </summary>
[OralEhrContextUnitOfWork]
public record CreatePatientExaminationCardTestModeCommand(
    Guid PatientId,
    string StudentId,
    CreatePatientExaminationCardTestModeInputParams InputParams)
        : IRequest<OperationResult<PatientExaminationCardDto>>;

