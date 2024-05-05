using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIBleedingForm;

/// <summary>
/// Command for adding a comment to the API Bleeding Form of a practice examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record CommentAPIBleedingFormCommand(Guid PracticeExaminationCardId, string DoctorComment)
    : IRequest<OperationResult<Unit>>;

