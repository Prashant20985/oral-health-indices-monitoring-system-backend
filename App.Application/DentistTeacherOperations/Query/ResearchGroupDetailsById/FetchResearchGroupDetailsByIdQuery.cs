using App.Application.Core;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using MediatR;


namespace App.Application.DentistTeacherOperations.Query.ResearchGroupDetailsById;

/// <summary>
/// Represents a query to fetch research group details by ID.
/// </summary>
public record FetchResearchGroupDetailsByIdQuery(Guid ResearchGroupId) :
    IRequest<OperationResult<ResearchGroupResponseDto>>;
