using App.Application.Core;
using App.Domain.DTOs;
using MediatR;


namespace App.Application.DentistTeacherOperations.Query.ResearchGroupDetailsById;

/// <summary>
/// Represents a query to fetch research group details by ID.
/// </summary>
public record FetchResearchGroupDetailsByIdQuery(Guid ResearchGroupId) :
    IRequest<OperationResult<ResearchGroupDto>>;
