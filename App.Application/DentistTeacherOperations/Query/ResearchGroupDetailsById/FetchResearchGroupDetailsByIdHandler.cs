using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Query.ResearchGroupDetailsById;

/// <summary>
/// Handler for fetching research group details by ID.
/// </summary>
internal sealed class FetchResearchGroupDetailsByIdHandler
    : IRequestHandler<FetchResearchGroupDetailsByIdQuery, OperationResult<ResearchGroupDto>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchResearchGroupDetailsByIdHandler"/> class.
    /// </summary>
    /// <param name="researchGroupRepository">The repository for research group-related operations.</param>
    public FetchResearchGroupDetailsByIdHandler (IResearchGroupRepository researchGroupRepository)
    {
        _researchGroupRepository = researchGroupRepository;
    }

    /// <summary>
    /// Handles the query to fetch research group details by ID.
    /// </summary>
    /// <param name="request">The query to fetch research group details by ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the research group details.</returns>
    public async Task<OperationResult<ResearchGroupDto>> Handle
        (FetchResearchGroupDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        // Get the research group details by ID.
        var researchGroup = await _researchGroupRepository.GetResearchGroupDetailsById(request.ResearchGroupId);

        // Return the result encapsulated in an OperationResult.
        if (researchGroup is null)
            return OperationResult<ResearchGroupDto>.Failure("Research group not found");

        // Return the result encapsulated in an OperationResult.
        return OperationResult<ResearchGroupDto>.Success(researchGroup);
    }
}

