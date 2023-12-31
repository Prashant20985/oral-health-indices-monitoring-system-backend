﻿using App.Application.Core;
using App.Domain.DTOs;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;

/// <summary>
/// Handler for fetching patients not in any research group.
/// </summary>
internal sealed class FetchPatientsNotInResearchGroupsHandler
    : IRequestHandler<FetchPatientsNotInResearchGroupsQuery, OperationResult<List<ResearchGroupPatientDto>>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="FetchPatientsNotInResearchGroupsHandler"/> class.
    /// </summary>
    /// <param name="researchGroupRepository">The repository for research group-related operations.</param>
    public FetchPatientsNotInResearchGroupsHandler(IResearchGroupRepository researchGroupRepository)
    {
        _researchGroupRepository = researchGroupRepository ?? throw new ArgumentNullException(nameof(researchGroupRepository));
    }

    /// <summary>
    /// Handles the query to fetch patients not in any research group.
    /// </summary>
    /// <param name="request">The query to fetch patients not in any research group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing the list of patients not in any research group.</returns>
    public async Task<OperationResult<List<ResearchGroupPatientDto>>> Handle(FetchPatientsNotInResearchGroupsQuery request, CancellationToken cancellationToken)
    {
        // Get the initial query of all patients not in any research group.
        var query = _researchGroupRepository.GetAllPatientsNotInAnyResearchGroup();

        // Apply additional filters based on the optional parameters.
        if (!string.IsNullOrEmpty(request.PatientName))
            query = query.Where(p => p.FirstName.Contains(request.PatientName));

        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(p => p.Email.Contains(request.Email));

        // Execute the query and retrieve the list of patients.
        var patients = await query.ToListAsync(cancellationToken);

        // Return the result encapsulated in an OperationResult.
        return OperationResult<List<ResearchGroupPatientDto>>.Success(patients);
    }
}

