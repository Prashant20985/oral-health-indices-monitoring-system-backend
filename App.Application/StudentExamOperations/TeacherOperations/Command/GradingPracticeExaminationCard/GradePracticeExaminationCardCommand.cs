using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.GradingPracticeExaminationCard;

/// <summary>
/// Command to grade a practice examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record GradePracticeExaminationCardCommand(Guid PracticeExaminationCardId,
    int StudentMark) : IRequest<OperationResult<Unit>>;