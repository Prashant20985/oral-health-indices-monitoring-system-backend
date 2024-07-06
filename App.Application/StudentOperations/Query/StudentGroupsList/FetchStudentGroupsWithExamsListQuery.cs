using App.Application.Core;
using App.Domain.DTOs.StudentGroupDtos.Response;
using MediatR;

namespace App.Application.StudentOperations.Query.StudentGroupsList;

/// <summary>
/// Fetches all groups with exams for the student.
/// </summary>
public record FetchStudentGroupsWithExamsListQuery(string StudentId)
    : IRequest<OperationResult<List<StudentGroupWithExamsListResponseDto>>>;
