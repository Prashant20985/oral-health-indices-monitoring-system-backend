using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentPatientExaminationCard;

/// <summary>
/// Command to comment on a patient examination card
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentPatientExaminationCardCommand(Guid Cardid, string Comment, bool IsStudent)
    : IRequest<OperationResult<Unit>>;

