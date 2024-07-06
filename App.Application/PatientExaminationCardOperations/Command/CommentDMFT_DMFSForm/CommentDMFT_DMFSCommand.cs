using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.PatientExaminationCardOperations.Command.CommentDMFT_DMFSForm;

/// <summary>
/// Command for adding a comment to the DMFT_DMFS form
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentDMFT_DMFSCommand(Guid CardId, string Comment, bool IsStudent)
    : IRequest<OperationResult<Unit>>;
