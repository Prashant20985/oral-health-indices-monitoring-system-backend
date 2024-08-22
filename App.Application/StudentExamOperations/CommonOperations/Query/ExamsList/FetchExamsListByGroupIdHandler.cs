using App.Application.Core;
using App.Domain.DTOs.ExamDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentExamOperations.CommonOperations.Query.ExamsList;

/// <summary>
/// Handler for fetching a list of exams by group ID.
/// </summary>
internal sealed class FetchExamsListByGroupIdHandler(IStudentExamRepository studentExamRepository)
    : IRequestHandler<FetchExamsListByGroupIdQuery, OperationResult<List<ExamDto>>>
{
    private readonly IStudentExamRepository _studentExamRepository = studentExamRepository;

    /// <summary>
    /// Handles the fetch exams list by group ID request.
    /// </summary>
    /// <param name="request">The fetch exams list by group ID query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An OperationResult containing a list of ExamDto.</returns>
    public async Task<OperationResult<List<ExamDto>>> Handle(FetchExamsListByGroupIdQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the exams by group ID
        var exams = await _studentExamRepository.GetExamDtosByGroupId(request.GroupId);

        // Return the exams list
        return OperationResult<List<ExamDto>>.Success(exams);
    }
}
