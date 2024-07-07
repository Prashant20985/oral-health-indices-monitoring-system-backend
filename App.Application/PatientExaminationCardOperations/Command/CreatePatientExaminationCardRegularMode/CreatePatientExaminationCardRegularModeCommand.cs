
using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

/// <summary>
/// Command for creating patient examination card in regular mode for patient by student or doctor
/// </summary>
[OralEhrContextUnitOfWork]
public record CreatePatientExaminationCardRegularModeCommand(
    Guid PatientId,
    string UserId,
    bool IsStudent,
    CreatePatientExaminationCardRegularModeInputParams InputParams)
        : IRequest<OperationResult<PatientExaminationCardDto>>;
