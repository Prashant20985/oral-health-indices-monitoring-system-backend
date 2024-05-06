using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.StudentGroupDetails;

/// <summary>
/// Fetches a student group by its identifier.
/// </summary>
public record FetchStudentGroupQuery(Guid GroupId)
    : IRequest<OperationResult<GroupDto>>;
