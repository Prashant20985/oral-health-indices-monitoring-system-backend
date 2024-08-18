using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.StudentOperations.Query.ExamEligbility;

/// <summary>
/// Check if the student is eligible to take the exam, returns ture if student hasn't taken the exam before, false otherwise.
/// </summary>
/// <param name="studentExamRepository"></param>
internal sealed class ExamEligibilityHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<ExamEligibiltyQuery, OperationResult<bool>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handle the ExamEligibiltyQuery request.
    /// </summary>
    /// <param name="request">Request to check the eligibility of the student to take the exam.</param>
    /// <param name="cancellationToken">The CancellationToken.</param>
    /// <returns>OperationResult of type bool.</returns>
    public async Task<OperationResult<bool>> Handle(ExamEligibiltyQuery request, CancellationToken cancellationToken) =>
        await _studentExamRepository.CheckIfStudentHasAlreadyTakenTheExam(request.ExamId, request.StudentId)
            ? OperationResult<bool>.Success(false)
            : OperationResult<bool>.Success(true);
}
