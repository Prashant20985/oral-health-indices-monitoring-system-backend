﻿using App.Application.Core;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.DentistTeacherOperations.Query.ResearchGroups;

internal sealed class FetchResearchGroupsHandler
    : IRequestHandler<FetchResearchGroupsQuery, OperationResult<List<ResearchGroupResponseDto>>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;

    public FetchResearchGroupsHandler(IResearchGroupRepository researchGroupRepository)
    {
        _researchGroupRepository = researchGroupRepository;
    }

    public async Task<OperationResult<List<ResearchGroupResponseDto>>> Handle(FetchResearchGroupsQuery request, CancellationToken cancellationToken)
    {
        var query = _researchGroupRepository.GetAllResearchGroups();

        if (!string.IsNullOrEmpty(request.GroupName))
            query = query.Where(x => x.GroupName.Contains(request.GroupName));

        var researchGroups = await query.ToListAsync(cancellationToken);

        return OperationResult<List<ResearchGroupResponseDto>>.Success(researchGroups);
    }
}
