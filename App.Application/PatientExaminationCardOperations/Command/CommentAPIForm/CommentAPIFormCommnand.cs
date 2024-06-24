using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentAPIForm;

/// <summary>
/// Command for adding comment to the patient examination card
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentAPIFormCommnand(Guid CardId, string DoctorComment)
    : IRequest<OperationResult<Unit>>;
