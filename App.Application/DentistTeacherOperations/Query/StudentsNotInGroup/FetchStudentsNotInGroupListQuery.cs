using App.Application.Core;
using App.Domain.DTOs.ApplicationUserDtos.Response;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentsNotInGroup;

/// <summary>
/// Represents a request to fetch a paged list of students not in group.
/// </summary>
/// <param name="Params"></param>
public record FetchStudentsNotInGroupListQuery(Guid GroupId,
    string StudentName,
    string Email,
    int Page,
    int PageSize) : IRequest<OperationResult<PaginatedStudentResponseDto>>;
