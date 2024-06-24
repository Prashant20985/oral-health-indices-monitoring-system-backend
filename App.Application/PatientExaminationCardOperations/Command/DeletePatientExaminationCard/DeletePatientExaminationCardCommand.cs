using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.DeletePatientExaminationCard;

/// <summary>
/// Command to delete a patient examination card
/// </summary>
[OralEhrContextUnitOfWork]
public record DeletePatientExaminationCardCommand(Guid CardId)
    : IRequest<OperationResult<Unit>>;
