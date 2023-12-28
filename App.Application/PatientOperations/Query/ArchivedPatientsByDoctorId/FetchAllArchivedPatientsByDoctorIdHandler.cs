using App.Application.Core;
using App.Domain.DTOs.PatientDtos.Response;
using App.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.PatientOperations.Query.ArchivedPatientsByDoctorId;

/// <summary>
/// Handles the FetchAllArchivedPatientsByDoctorIdQuery by retrieving a list of archived patients
/// associated with a specific doctor, with optional filtering by name and email.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FetchAllArchivedPatientsByDoctorIdHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class FetchAllArchivedPatientsByDoctorIdHandler(IPatientRepository patientRepository)
        : IRequestHandler<FetchAllArchivedPatientsByDoctorIdQuery, OperationResult<List<PatientExaminationDto>>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the FetchAllArchivedPatientsByDoctorIdQuery by executing the necessary logic to retrieve
    /// archived patients associated with a specific doctor based on the provided query parameters.
    /// </summary>
    /// <param name="request">The FetchAllArchivedPatientsByDoctorIdQuery containing optional filters.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>An operation result containing a list of archived patient examination DTOs.</returns>
    public async Task<OperationResult<List<PatientExaminationDto>>> Handle(FetchAllArchivedPatientsByDoctorIdQuery request, CancellationToken cancellationToken)
    {
        var query = _patientRepository.GetAllArchivedPatientsByDoctorId(request.DoctorId);

        if (!string.IsNullOrEmpty(request.Name))
            query = query.Where(p => p.FirstName.Contains(request.Name) || p.LastName.Contains(request.Name));

        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(p => p.Email.Contains(request.Email));

        var patients = await query.AsNoTracking().ToListAsync(cancellationToken);

        return OperationResult<List<PatientExaminationDto>>.Success(patients);
    }
}

