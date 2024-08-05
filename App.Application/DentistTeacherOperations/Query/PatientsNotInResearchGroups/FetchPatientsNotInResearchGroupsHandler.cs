using App.Application.Core;
using App.Domain.DTOs.ResearchGroupDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.DentistTeacherOperations.Query.PatientsNotInResearchGroups;

/// <summary>
/// Handler for fetching patients not in any research group.
/// </summary>
internal sealed class FetchPatientsNotInResearchGroupsHandler
    : IRequestHandler<FetchPatientsNotInResearchGroupsQuery, OperationResult<PaginatedResearchGroupPatientResponseDto>>
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
    public async Task<OperationResult<PaginatedResearchGroupPatientResponseDto>> Handle(FetchPatientsNotInResearchGroupsQuery request, CancellationToken cancellationToken)
    {
        // Get the initial query of all patients not in any research group.
        var query = _researchGroupRepository.GetAllPatientsNotInAnyResearchGroup();

        // Apply additional filters based on the optional parameters.
        if (!string.IsNullOrEmpty(request.PatientName))
            query = query.Where(p => p.FirstName.Contains(request.PatientName));

        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(p => p.Email.Contains(request.Email));

        // Get the total number of patients.
        var totalNumberOfPatients = await query.CountAsync(cancellationToken);

        // Execute the query and retrieve the list of patients.
        var patients = await query
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // Create a paginated response of the patients.
        var paginatedResponse = new PaginatedResearchGroupPatientResponseDto
        {
            TotalNumberOfPatients = totalNumberOfPatients,
            Patients = patients
        };

        // Return the result encapsulated in an OperationResult.
        return OperationResult<PaginatedResearchGroupPatientResponseDto>.Success(paginatedResponse);
    }
}

