using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace App.Application.DentistTeacherOperations.Query.Groups;

/// <summary>
/// Handles the retrieval of a list of groups associated with a specific teacher.
/// </summary>
internal sealed class FetchGroupsHandler : IRequestHandler<FetchGroupsQuery, OperationResult<List<GroupDto>>>
{
    private readonly IGroupRepository _groupRepository;

    /// <summary>
    /// Initializes a new instance of the FetchGroupsHandler class
    /// </summary>
    /// <param name="groupRepository">The repository for mananging group-realated operations.</param>
    public FetchGroupsHandler(IGroupRepository groupRepository) =>
        _groupRepository = groupRepository;

    public async Task<OperationResult<List<GroupDto>>> Handle(FetchGroupsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve a list of groups associated with the specified teacher.
        var result = await _groupRepository.GetAllGroupsWithStudentsList(request.TeacherId);

        if (result.IsNullOrEmpty())
            return OperationResult<List<GroupDto>>.Failure($"No groups found for teacher with Id {request.TeacherId}");

        // Return a success result with the list of groups.
        return OperationResult<List<GroupDto>>.Success(result);
    }

}
