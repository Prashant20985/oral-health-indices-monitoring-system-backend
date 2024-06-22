using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentBleedingForm;

/// <summary>
/// Command for adding a comment to the Bleeding form of a practice examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentBleedingFormCommand(Guid PracticeExaminationCardId,
    string DoctorComment) : IRequest<OperationResult<Unit>>;
