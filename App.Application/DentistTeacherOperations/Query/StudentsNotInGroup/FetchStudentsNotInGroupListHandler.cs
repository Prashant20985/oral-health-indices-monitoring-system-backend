using App.Application.Core;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;

/// <summary>
///  Handler for fetching a list of students not assigned to a specific group.
/// </summary>
internal sealed class FetchStudentsNotInGroupListHandler
    : IRequestHandler<FetchStudentsNotInGroupListQuery,
        OperationResult<PaginatedStudentnotInGroupResponseDto>>
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
    public async Task<OperationResult<PaginatedStudentnotInGroupResponseDto>> Handle
        (FetchStudentsNotInGroupListQuery request, CancellationToken cancellationToken)
    {
        // check if students list not in group is empty
        var studentsNotInGroupQuery = _groupRepository.GetAllStudentsNotInGroup(request.GroupId);

        // Apply additional filters based on the optional parameters.
        if(!string.IsNullOrEmpty(request.StudentName))
            studentsNotInGroupQuery = studentsNotInGroupQuery.Where(p => (p.FirstName + " " + p.LastName).Contains(request.StudentName));


        if (!string.IsNullOrEmpty(request.Email))
            studentsNotInGroupQuery = studentsNotInGroupQuery.Where(p => p.Email.Contains(request.Email));

        var totalStudents = await studentsNotInGroupQuery.CountAsync(cancellationToken);

        // Execute the query and retrieve the list of students.
        var studentsNotInGroup = await studentsNotInGroupQuery
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // Create a paginated response object.
        var studentsNotInGroupResponse = new PaginatedStudentnotInGroupResponseDto
        {
            TotalStudents = totalStudents,
            Students = studentsNotInGroup
        };

        // Return a success result with no specific data.
        return OperationResult<PaginatedStudentnotInGroupResponseDto>.Success(studentsNotInGroupResponse);
    }
}

