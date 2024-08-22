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
        // Get the group details with exams list by group id and student id
        var studentGroupWithExams = await _groupRepository
            .GetGroupDetailsWithExamsListByGroupIdAndStudentId(request.GroupId, request.StudentId);

        // Check if the group exists
        if (studentGroupWithExams is null)
            return OperationResult<StudentGroupWithExamsListResponseDto>.Failure("Group Not Found");
        
        // Return the student group details with exams
        return OperationResult<StudentGroupWithExamsListResponseDto>.Success(studentGroupWithExams);
    }
}
