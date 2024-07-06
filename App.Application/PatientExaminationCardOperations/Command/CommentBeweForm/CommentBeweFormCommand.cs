using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;

/// <summary>
/// Command to add a comment to the BEWE form
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentBeweFormCommand(Guid CardId, string Comment, bool IsStudent)
    : IRequest<OperationResult<Unit>>;
