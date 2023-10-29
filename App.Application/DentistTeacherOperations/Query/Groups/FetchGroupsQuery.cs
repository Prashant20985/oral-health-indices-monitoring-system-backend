using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.Groups;

/// <summary>
/// Represents a query to fetch a list of groups asscoiated with a specific teacher.
/// </summary>
public record FetchGroupsQuery(string TeacherId) : IRequest<OperationResult<List<GroupDto>>>;
