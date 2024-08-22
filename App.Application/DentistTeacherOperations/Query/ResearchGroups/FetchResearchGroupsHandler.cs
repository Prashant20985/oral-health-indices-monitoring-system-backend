using App.Application.Core;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.DentistTeacherOperations.Query.ResearchGroups;
/// <summary>
/// Handles the retrieval of a list of research groups based on the specified criteria.
/// </summary>
internal sealed class FetchResearchGroupsHandler
    : IRequestHandler<FetchResearchGroupsQuery, OperationResult<List<ResearchGroupResponseDto>>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;
   
    /// <summary>
    /// Initializes a new instance of the <see cref="FetchResearchGroupsHandler"/> class.
    /// </summary>
    /// <param name="researchGroupRepository">The repository for research group-related operations.</param>
    public FetchResearchGroupsHandler(IResearchGroupRepository researchGroupRepository)
    {
        _researchGroupRepository = researchGroupRepository;
    }
    /// <summary>
    /// Handles the query to fetch research groups based on the specified criteria.
    /// </summary>
    /// <param name="request">The query containing the criteria for fetching research groups.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the list of research groups.</returns>
    public async Task<OperationResult<List<ResearchGroupResponseDto>>> Handle(FetchResearchGroupsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all research groups from the repository.
        var query = _researchGroupRepository.GetAllResearchGroups();

        // Filter the research groups by group name if specified in the request.
        if (!string.IsNullOrEmpty(request.GroupName))
            query = query.Where(x => x.GroupName.Contains(request.GroupName));

        // Execute the query and get the list of research groups.
        var researchGroups = await query.ToListAsync(cancellationToken);

        // Return a success result with the list of research groups.
        return OperationResult<List<ResearchGroupResponseDto>>.Success(researchGroups);
    }
}
