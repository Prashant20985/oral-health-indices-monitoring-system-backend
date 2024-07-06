using App.Application.Core;
using App.Domain.DTOs.StudentGroupDtos.Response;
using MediatR;

namespace App.Application.StudentOperations.Query.StudentGroupDetails;

/// <summary>
/// Fetches the student group details with exams.
/// </summary>
public record FetchStudentGroupDetailsWithExamsQuery(string StudentId, Guid GroupId)
    : IRequest<OperationResult<StudentGroupWithExamsListResponseDto>>;
