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
        var query = _patientRepository.GetAllActivePatients();

        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(x => x.FirstName.Contains(request.Name)
                || x.LastName.Contains(request.Name));

        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(x => x.Email.Contains(request.Email));

        var totalPatientsCount = await query.CountAsync(cancellationToken);

        var patients = await query
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var paginatedPatients = new PaginatedPatientResponseDto
        {
            TotalPatientsCount = totalPatientsCount,
            Patients = patients
        };

        return OperationResult<PaginatedPatientResponseDto>.Success(paginatedPatients);
    }
}

