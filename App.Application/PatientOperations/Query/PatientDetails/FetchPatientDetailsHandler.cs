using App.Application.Core;
using App.Domain.DTOs.Common.Response;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientOperations.Query.PatientDetails;

/// <summary>
/// Handles the FetchPatientDetailsQuery by retrieving the details of a specific patient.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FetchPatientDetailsHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class FetchPatientDetailsHandler(IPatientRepository patientRepository)
        : IRequestHandler<FetchPatientDetailsQuery, OperationResult<PatientResponseDto>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the FetchPatientDetailsQuery by executing the necessary logic to retrieve
    /// patient details based on the provided patient ID.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <param name="request">The FetchPatientDetailsQuery containing the patient ID.</param>
    /// <returns>An operation result containing the patient examination DTO.</returns>
    public async Task<OperationResult<PatientResponseDto>> Handle(FetchPatientDetailsQuery request, CancellationToken cancellationToken)
    {
        var patientDetails = await _patientRepository.GetPatientDetails(request.PatientId);

        if (patientDetails is null)
            return OperationResult<PatientResponseDto>.Failure("Patient Not Found");

        return OperationResult<PatientResponseDto>.Success(patientDetails);
    }
}
