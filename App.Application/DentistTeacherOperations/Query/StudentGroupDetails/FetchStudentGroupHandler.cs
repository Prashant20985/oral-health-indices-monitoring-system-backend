using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentGroupDetails;

/// <summary>
/// Fetches a student group by its identifier.
/// </summary>
/// <param name="groupRepository">The repository for managing groups and related operations.</param>
internal sealed class FetchStudentGroupHandler(IGroupRepository groupRepository)
        : IRequestHandler<FetchStudentGroupQuery, OperationResult<GroupDto>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;

    /// <summary>
    /// Handles the FetchStudentGroupQuery request.
    /// </summary>
    /// <param name="request">The request to fetch a student group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OperationResult<GroupDto>> Handle(FetchStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetGroupDetailsWithStudentList(request.GroupId);

        if (group is null)
            return OperationResult<GroupDto>.Failure("Group not found.");

        return OperationResult<GroupDto>.Success(group);
    }
}
