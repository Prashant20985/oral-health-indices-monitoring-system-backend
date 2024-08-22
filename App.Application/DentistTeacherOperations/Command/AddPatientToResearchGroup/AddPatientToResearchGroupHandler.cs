using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.DentistTeacherOperations.Command.AddPatientToResearchGroup;

/// <summary>
/// Handles the command to add a patient to a research group.
/// </summary>
internal sealed class AddPatientToResearchGroupHandler
    : IRequestHandler<AddPatientToResearchGroupCommand, OperationResult<Unit>>
{
    private readonly IResearchGroupRepository _researchGroupRepository;
    private readonly IPatientRepository _patientRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddPatientToResearchGroupHandler"/> class.
    /// </summary>
    /// <param name="patientRepository">The repository for patient-related operations.</param>
    /// <param name="researchGroupRepository">The repository for research group-related operations.</param>
    public AddPatientToResearchGroupHandler(IPatientRepository patientRepository,
        IResearchGroupRepository researchGroupRepository)
    {
        _patientRepository = patientRepository;
        _researchGroupRepository = researchGroupRepository;
    }

    /// <summary>
    /// Handles the command to add a patient to a research group.
    /// </summary>
    /// <param name="request">The command to add a patient to a research group.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating the success or failure of the operation.</returns>
    public async Task<OperationResult<Unit>> Handle(AddPatientToResearchGroupCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the patient by ID
        var patient = await _patientRepository.GetPatientById(request.PatientId);

        // Check if the patient exists
        if (patient is null)
            return OperationResult<Unit>.Failure("Patient not found");

        // Check if the patient is already in a research group
        if (patient.ResearchGroupId is not null)
            return OperationResult<Unit>.Failure("Patient is already in a research group");

        // Retrieve the research group by ID
        var researchGroup = await _researchGroupRepository.GetResearchGroupById(request.ResearchGroupId);

        // Check if the research group exists
        if (researchGroup is null)
            return OperationResult<Unit>.Failure("Research group not found");

        // Add the patient to the research group
        patient.AddResearchGroup(request.ResearchGroupId);

        // Return a successful operation result
        return OperationResult<Unit>.Success(Unit.Value);
    }
}

