using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.PatientOperations.Query.ActivePatients;

/// <summary>
/// Handler for the <see cref="FetchAllActivePatientsQuery"/> query, responsible for fetching all active patients.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FetchAllActivePatientsHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class FetchAllActivePatientsHandler(IPatientRepository patientRepository)
    : IRequestHandler<FetchAllActivePatientsQuery, OperationResult<PaginatedPatientResponseDto>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the <see cref="FetchAllActivePatientsQuery"/> to fetch all active patients based on optional name and email filters.
    /// </summary>
    /// <param name="request">The fetch all active patients query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing a list of active patient DTOs.</returns>
    public async Task<OperationResult<PaginatedPatientResponseDto>> Handle(FetchAllActivePatientsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all active patients
        var query = _patientRepository.GetAllActivePatients();

        // Apply optional filters for name
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.FirstName.Contains(request.Name)
                || x.LastName.Contains(request.Name));

        // Apply optional filters for email
        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(x => x.Email.Contains(request.Email));

        // Count the total number of patients
        var totalPatientsCount = await query.CountAsync(cancellationToken);

        // Paginate the patients
        var patients = await query
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        // Create a paginated response DTO
        var paginatedPatients = new PaginatedPatientResponseDto
        {
            TotalPatientsCount = totalPatientsCount,
            Patients = patients
        };

        // Return the paginated response
        return OperationResult<PaginatedPatientResponseDto>.Success(paginatedPatients);
    }
}

