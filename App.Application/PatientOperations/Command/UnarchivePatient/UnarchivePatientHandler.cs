using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientOperations.Command.UnarchivePatient;

/// <summary>
/// Handler for the <see cref="UnarchivePatientCommand"/> command, responsible for unarchiving patients.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UnarchivePatientHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class UnarchivePatientHandler(IPatientRepository patientRepository) : IRequestHandler<UnarchivePatientCommand, OperationResult<Unit>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the <see cref="UnarchivePatientCommand"/> command to unarchive a patient.
    /// </summary>
    /// <param name="request">The unarchive patient command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the success or failure of the unarchive operation.</returns>
    public async Task<OperationResult<Unit>> Handle(UnarchivePatientCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the patient by Id from the repository
        var patient = await _patientRepository.GetPatientById(request.PatientId);

        // Check if the patient exists
        if (patient is null)
            return OperationResult<Unit>.Failure("Patient not found.");

        // Check if the patient is already unarchived
        if (!patient.IsArchived)
            return OperationResult<Unit>.Failure("Patient is not archived.");

        // Unarchive the patient
        patient.UnarchivePatient();

        return OperationResult<Unit>.Success(Unit.Value);
    }
}

