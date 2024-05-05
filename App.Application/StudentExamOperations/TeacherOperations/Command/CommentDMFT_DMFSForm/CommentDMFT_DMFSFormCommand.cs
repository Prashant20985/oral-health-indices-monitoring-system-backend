using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentDMFT_DMFSForm;

/// <summary>
/// Command for adding a comment to the DMFT/DMFS form of a practice examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentDMFT_DMFSFormCommand(Guid PracticeExaminationCardId,
    string DoctorComment) : IRequest<OperationResult<Unit>>;
