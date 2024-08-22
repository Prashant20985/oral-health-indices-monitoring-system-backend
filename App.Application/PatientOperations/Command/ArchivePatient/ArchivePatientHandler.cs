using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientOperations.Command.ArchivePatient;

/// <summary>
/// Handler for the <see cref="ArchivePatientCommand"/> command, responsible for archiving patients.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ArchivePatientHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class ArchivePatientHandler(IPatientRepository patientRepository)
    : IRequestHandler<ArchivePatientCommand, OperationResult<Unit>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the <see cref="ArchivePatientCommand"/> command to archive a patient.
    /// </summary>
    /// <param name="request">The archive patient command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the success or failure of the archive operation.</returns>
    public async Task<OperationResult<Unit>> Handle(ArchivePatientCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the patient by Id from the repository
        var patient = await _patientRepository.GetPatientById(request.PatientId);

        // Check if the patient exists
        if (patient is null)
            return OperationResult<Unit>.Failure("Patient not found.");

        // Check if the patient is already archived
        if (patient.IsArchived)
            return OperationResult<Unit>.Failure("Patient already archived.");

        // Archive the patient with the provided comment
        patient.ArchivePatient(request.ArchiveComment);

        //Return success
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
