using App.Application.Core;
using App.Domain.DTOs.StudentGroupDtos.Response;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentGroupDetails;

/// <summary>
/// Fetches a student group by its identifier.
/// </summary>
public record FetchStudentGroupQuery(Guid GroupId)
    : IRequest<OperationResult<StudentGroupResponseDto>>;
