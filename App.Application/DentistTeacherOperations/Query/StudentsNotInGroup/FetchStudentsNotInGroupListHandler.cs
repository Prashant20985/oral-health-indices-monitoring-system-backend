using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;

/// <summary>
///  Handler for fetching a list of students not assigned to a specific group.
/// </summary>
internal sealed class FetchStudentsNotInGroupListHandler
    : IRequestHandler<FetchStudentsNotInGroupListQuery,
        OperationResult<List<StudentResponseDto>>>
{
    private readonly IGroupRepository _groupRepository;

    /// <summary>
    /// Initializes a new instance of the FetchStudentsNotInGroupHandler class.
    /// </summary>
    /// <param name="groupRepository"></param>
    public FetchStudentsNotInGroupListHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    /// <inheritdoc />
    public async Task<OperationResult<List<StudentResponseDto>>> Handle
        (FetchStudentsNotInGroupListQuery request, CancellationToken cancellationToken)
    {
        // check if students list not in group is empty
        var studentsNotInGroupQuery = await _groupRepository.GetAllStudentsNotInGroup(request.GroupId);

        // Return a success result with no specific data.
        return OperationResult<List<StudentResponseDto>>.Success(studentsNotInGroupQuery);
    }
}

