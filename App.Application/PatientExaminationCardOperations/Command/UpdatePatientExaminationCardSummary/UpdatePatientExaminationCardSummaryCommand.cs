using App.Application.Core;
using App.Domain.DTOs.Common.Request;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;

/// <summary>
/// Command for updating patient examination card summary.
/// </summary>
[OralEhrContextUnitOfWork]
public record UpdatePatientExaminationCardSummaryCommand(Guid CardId, SummaryRequestDto Summary)
    : IRequest<OperationResult<Unit>>;
