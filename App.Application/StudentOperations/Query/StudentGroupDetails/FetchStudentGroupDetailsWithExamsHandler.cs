using App.Application.Core;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentOperations.Query.StudentGroupDetails;

/// <summary>
/// Fetches the student group details with exams.
/// </summary>
/// <param name="groupRepository">The group repository.</param>
internal sealed class FetchStudentGroupDetailsWithExamsHandler(IGroupRepository groupRepository)
        : IRequestHandler<FetchStudentGroupDetailsWithExamsQuery, OperationResult<StudentGroupWithExamsListResponseDto>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;

    /// <summary>
    /// Handles the fetch student group details with exams query.
    /// </summary>
    /// <param name="request">The fetch student group details with exams query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The operation result with student group details.</returns>
    public async Task<OperationResult<StudentGroupWithExamsListResponseDto>> Handle(FetchStudentGroupDetailsWithExamsQuery request, CancellationToken cancellationToken)
    {
        var studentGroupWithExams = await _groupRepository
            .GetGroupDetailsWithExamsListByGroupIdAndStudentId(request.GroupId, request.StudentId);

        if (studentGroupWithExams is null)
            return OperationResult<StudentGroupWithExamsListResponseDto>.Failure("Group Not Found");

        return OperationResult<StudentGroupWithExamsListResponseDto>.Success(studentGroupWithExams);
    }
}
