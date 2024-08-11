using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientOperations.Command.DeletePatient;

/// <summary>
/// Handler for the <see cref="DeletePatientCommand"/> command, responsible for deleting patients.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DeletePatientHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class DeletePatientHandler(IPatientRepository patientRepository) : IRequestHandler<DeletePatientCommand, OperationResult<Unit>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the <see cref="DeletePatientCommand"/> command to delete a patient.
    /// </summary>
    /// <param name="request">The delete patient command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the success or failure of the delete operation.</returns>
    public async Task<OperationResult<Unit>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the patient by Id from the repository
        var patient = await _patientRepository.GetPatientById(request.PatientId);

        // Check if the patient exists
        if (patient is null)
            return OperationResult<Unit>.Failure("Patient not found.");

        // Delete the patient from the repository
        await _patientRepository.DeletePatient(request.PatientId);

        return OperationResult<Unit>.Success(Unit.Value);
    }
}

