using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.PatientOperations.Query.ArchivedPatients;

/// <summary>
/// Handles the FetchAllArchivedPatientsQuery by retrieving a list of archived patients
/// with optional filtering by name and email.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FetchAllArchivedPatientsHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class FetchAllArchivedPatientsHandler(IPatientRepository patientRepository)
    : IRequestHandler<FetchAllArchivedPatientsQuery, OperationResult<PaginatedPatientResponseDto>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the FetchAllArchivedPatientsQuery by executing the necessary logic to retrieve
    /// archived patients based on the provided query parameters.
    /// </summary>
    /// <param name="request">The FetchAllArchivedPatientsQuery containing optional filters.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result containing a list of archived patient DTOs.</returns>
    public async Task<OperationResult<PaginatedPatientResponseDto>> Handle(FetchAllArchivedPatientsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all archived patients
        var query = _patientRepository.GetAllArchivedPatients();

        // Apply optional filters for name
        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(p => p.FirstName.Contains(request.Name) || p.LastName.Contains(request.Name));

        // Apply optional filters for email
        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(p => p.Email.Contains(request.Email));

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
