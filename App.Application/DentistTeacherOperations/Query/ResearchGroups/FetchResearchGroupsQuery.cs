using App.Application.Core;
using App.Domain.DTOs;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.ResearchGroups;

/// <summary>
/// Represents a query to fetch research groups based on the group name.
/// </summary>
public record FetchResearchGroupsQuery(string GroupName) 
    : IRequest<OperationResult<List<ResearchGroupDto>>>;