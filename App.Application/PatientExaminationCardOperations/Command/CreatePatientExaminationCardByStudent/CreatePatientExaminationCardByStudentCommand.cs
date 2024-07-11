using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByStudent;

/// <summary>
/// Command for creating patient examination card by student
/// </summary>
[OralEhrContextUnitOfWork]
public record CreatePatientExaminationCardByStudentCommand(
    Guid PatientId,
    string StudentId,
    CreatePatientExaminationCardByStudentInputParams InputParams)
        : IRequest<OperationResult<PatientExaminationCardDto>>;
