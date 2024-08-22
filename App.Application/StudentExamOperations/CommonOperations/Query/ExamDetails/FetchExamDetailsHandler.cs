using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.CommonOperations.Query.ExamDetails;

/// <summary>
/// Handler for fetching exam details.
/// </summary>
internal sealed class FetchExamDetailsHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<FetchExamDetailsQuery, OperationResult<ExamDto>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the fetch exam details request.
    /// </summary>
    /// <param name="request">The fetch exam details query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An OperationResult of type ExamDto.</returns>
    public async Task<OperationResult<ExamDto>> Handle(FetchExamDetailsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the exam details
        var examDetails = await _studentExamRepository.GetExamDtoById(request.ExamId);

        // Check if the exam exists
        if (examDetails == null)
            return OperationResult<ExamDto>.Failure("Exam not found");

        // Return the exam details
        return OperationResult<ExamDto>.Success(examDetails);
    }
}
