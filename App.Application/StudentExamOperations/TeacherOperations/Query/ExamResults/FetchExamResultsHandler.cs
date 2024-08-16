using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.TeacherOperations.Query.ExamResults;

/// <summary>
/// Handler for fetching exam results.
/// </summary>
/// <param name="studentExamRepository">The student exam repository.</param>
internal sealed class FetchExamResultsHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<FetchExamResultsQuery, OperationResult<List<StudentExamResultResponseDto>>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles fetching exam results.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="request">The request.</param>
    /// <returns>The operation result.</returns>
    public async Task<OperationResult<List<StudentExamResultResponseDto>>> Handle(FetchExamResultsQuery request, CancellationToken cancellationToken)
    {
        var exam = await _studentExamRepository.GetExamById(request.ExamId);

        // Check if the exam is null.
        if (exam is null)
            return OperationResult<List<StudentExamResultResponseDto>>.Failure("Exam not found.");

        // Get the exam results.
        var examResults = await _studentExamRepository.GetStudentExamResults(request.ExamId);

        return OperationResult<List<StudentExamResultResponseDto>>.Success(examResults);
    }
}
