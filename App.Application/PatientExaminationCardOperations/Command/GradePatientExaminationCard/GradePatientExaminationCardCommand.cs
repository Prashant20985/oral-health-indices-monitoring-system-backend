using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.GradePatientExaminationCard;

/// <summary>
/// Command to grade a patient examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record GradePatientExaminationCardCommand(Guid CardId, decimal TotalScore)
    : IRequest<OperationResult<Unit>>;
