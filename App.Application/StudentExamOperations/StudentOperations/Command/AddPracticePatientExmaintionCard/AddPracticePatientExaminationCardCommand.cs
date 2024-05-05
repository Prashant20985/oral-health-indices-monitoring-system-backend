using App.Application.Core;
using App.Persistence.Attributes;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;

/// <summary>
/// Command for adding a practice patient examination card.
/// </summary>
[OralEhrContextUnitOfWork]
public record AddPracticePatientExaminationCardCommand(
    Guid ExamId,
    string StudentId,
    PracticePatientExaminationCardInputModel CardInputModel)
    : IRequest<OperationResult<Unit>>;

