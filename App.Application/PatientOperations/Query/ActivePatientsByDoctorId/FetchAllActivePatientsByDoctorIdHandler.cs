using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.PatientOperations.Query.ActivePatientsByDoctorId;

/// <summary>
/// Handler for the <see cref="FetchAllActivePatientsByDoctorIdQuery"/> query, responsible for fetching all active patients by doctor ID.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FetchAllActivePatientsByDoctorIdHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class FetchAllActivePatientsByDoctorIdHandler(IPatientRepository patientRepository)
    : IRequestHandler<FetchAllActivePatientsByDoctorIdQuery, OperationResult<List<PatientDto>>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the <see cref="FetchAllActivePatientsByDoctorIdQuery"/> to fetch all active patients by doctor ID, with optional name and email filters.
    /// </summary>
    /// <param name="request">The fetch all active patients by doctor ID query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result containing a list of active patient examination DTOs.</returns>
    public async Task<OperationResult<List<PatientDto>>> Handle(FetchAllActivePatientsByDoctorIdQuery request, CancellationToken cancellationToken)
    {
        var query = _patientRepository.GetAllActivePatientsByDoctorId(request.DoctorId);

        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(p => p.FirstName.Contains(request.Name)
            || p.LastName.Contains(request.Name));

        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(p => p.Email.Contains(request.Email));

        var patients = await query.AsNoTracking().ToListAsync(cancellationToken);

        return OperationResult<List<PatientDto>>.Success(patients);
    }
}
