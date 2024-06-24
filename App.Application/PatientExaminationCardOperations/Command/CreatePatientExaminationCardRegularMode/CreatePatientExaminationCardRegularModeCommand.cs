
using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

/// <summary>
/// Command for creating patient examination card in regular mode
/// </summary>
[OralEhrContextUnitOfWork]
public record CreatePatientExaminationCardRegularModeCommand(
    Guid PatientId,
    CreatePatientExaminationCardRegularModeInputParams InputParams)
        : IRequest<OperationResult<PatientExaminationCardDto>>;
