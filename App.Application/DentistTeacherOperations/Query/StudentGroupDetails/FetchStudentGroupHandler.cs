using App.Application.Core;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentGroupDetails;

/// <summary>
/// Fetches a student group by its identifier.
/// </summary>
/// <param name="groupRepository">The repository for managing groups and related operations.</param>
internal sealed class FetchStudentGroupHandler(IGroupRepository groupRepository)
        : IRequestHandler<FetchStudentGroupQuery, OperationResult<StudentGroupResponseDto>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;

    /// <summary>
    /// Handles the FetchStudentGroupQuery request.
    /// </summary>
    /// <param name="request">The request to fetch a student group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OperationResult<StudentGroupResponseDto>> Handle(FetchStudentGroupQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the group details along with the student list based on the provided group ID.
        var group = await _groupRepository.GetGroupDetailsWithStudentList(request.GroupId);

        // Check if the group was found; if not, return a failure result.
        if (group is null)
            return OperationResult<StudentGroupResponseDto>.Failure("Group not found.");

        // Return a success result with the group details.
        return OperationResult<StudentGroupResponseDto>.Success(group);
    }
}
