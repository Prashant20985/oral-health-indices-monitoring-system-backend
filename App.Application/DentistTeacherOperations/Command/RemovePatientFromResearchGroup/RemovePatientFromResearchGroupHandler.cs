using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.RemovePatientFromResearchGroup;

/// <summary>
/// Handles the command to remove a patient from a research group.
/// </summary>
internal sealed class RemovePatientFromResearchGroupHandler
    : IRequestHandler<RemovePatientFromResearchGroupCommand, OperationResult<Unit>>
{
    private readonly IPatientRepository _patientRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RemovePatientFromResearchGroupHandler"/> class.
    /// </summary>
    /// <param name="patientRepository">The repository for patient-related operations.</param>
    public RemovePatientFromResearchGroupHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    /// <summary>
    /// Handles the command to remove a patient from a research group.
    /// </summary>
    /// <param name="request">The command to remove a patient from a research group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(RemovePatientFromResearchGroupCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the patient based on the provided identifier.
        var patient = await _patientRepository.GetPatientById(request.PatientId);

        // Check if the patient exists.
        if (patient is null)
            return OperationResult<Unit>.Failure("Patient not found");

        // Check if the patient is already not in any research group.
        if (patient.ResearchGroupId is null)
            return OperationResult<Unit>.Failure("Patient is not in any research group");

        // Remove the patient from the research group.
        patient.RemoveResearchGroup();

        // Return a success result with no specific data.
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
