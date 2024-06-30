using App.Application.Core;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query.ExamEligbility;

/// <summary>
/// Check if the student is eligible to take the exam, returns ture if student hasn't taken the exam before, false otherwise.
/// </summary>
public record ExamEligibiltyQuery(Guid ExamId, string StudentId)
    : IRequest<OperationResult<bool>>;

