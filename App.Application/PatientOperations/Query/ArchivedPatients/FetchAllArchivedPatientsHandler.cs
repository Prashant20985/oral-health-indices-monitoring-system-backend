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
internal sealed class FetchAllArchivedPatientsHandler(IPatientRepository patientRepository) : IRequestHandler<FetchAllArchivedPatientsQuery, OperationResult<List<PatientDto>>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the FetchAllArchivedPatientsQuery by executing the necessary logic to retrieve
    /// archived patients based on the provided query parameters.
    /// </summary>
    /// <param name="request">The FetchAllArchivedPatientsQuery containing optional filters.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result containing a list of archived patient DTOs.</returns>
    public async Task<OperationResult<List<PatientDto>>> Handle(FetchAllArchivedPatientsQuery request, CancellationToken cancellationToken)
    {
        var query = _patientRepository.GetAllArchivedPatients();

        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(p => p.FirstName.Contains(request.Name) || p.LastName.Contains(request.Name));

        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(p => p.Email.Contains(request.Email));

        var patients = await query.AsNoTracking().ToListAsync(cancellationToken);

        return OperationResult<List<PatientDto>>.Success(patients);
    }
}
