﻿using App.Application.Core;
using App.Domain.DTOs.StudentGroupDtos.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.StudentOperations.Query.StudentGroupsList;

/// <summary>
/// Fetches all groups with exams for the student.
/// </summary>
/// <param name="groupRepository">The group repository.</param>
internal sealed class FetchStudentGroupWithExamsHandler(IGroupRepository groupRepository)
        : IRequestHandler<FetchStudentGroupsWithExamsListQuery, OperationResult<List<StudentGroupWithExamsListResponseDto>>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;

    /// <summary>
    /// Handles the fetch student groups with exams list query.
    /// </summary>
    /// <param name="request">The fetch student groups with exams list query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The operation result with the list of groups with exams.</returns>
    public async Task<OperationResult<List<StudentGroupWithExamsListResponseDto>>> Handle(FetchStudentGroupsWithExamsListQuery request, CancellationToken cancellationToken)
    {
        // Get all groups with exams for the student
        var studentGroupsWithExams = await _groupRepository
            .GetAllGroupsByStudentIdWithExamsList(request.StudentId);

        // Return the list of groups with exams
        return OperationResult<List<StudentGroupWithExamsListResponseDto>>.Success(studentGroupsWithExams);
    }
}
