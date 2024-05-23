using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentOperations.Query.StudentGroupDetails;

/// <summary>
/// Fetches the student group details with exams.
/// </summary>
/// <param name="groupRepository">The group repository.</param>
internal sealed class FetchStudentGroupDetailsWithExamsHandler(IGroupRepository groupRepository)
        : IRequestHandler<FetchStudentGroupDetailsWithExamsQuery, OperationResult<GroupWithExamsListDto>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;

    /// <summary>
    /// Handles the fetch student group details with exams query.
    /// </summary>
    /// <param name="request">The fetch student group details with exams query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The operation result with student group details.</returns>
    public async Task<OperationResult<GroupWithExamsListDto>> Handle(FetchStudentGroupDetailsWithExamsQuery request, CancellationToken cancellationToken)
    {
        var studentGroupWithExams = await _groupRepository
            .GetGroupDetailsWithExamsListByGroupIdAndStudentId(request.GroupId, request.StudentId);

        if (studentGroupWithExams is null)
            return OperationResult<GroupWithExamsListDto>.Failure("Group Not Found");

        return OperationResult<GroupWithExamsListDto>.Success(studentGroupWithExams);
    }
}
