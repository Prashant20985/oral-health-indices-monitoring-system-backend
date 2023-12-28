using App.Application.Core;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientOperations.Command.UpdatePatient;

/// <summary>
/// Handler for the <see cref="UpdatePatientCommand"/> command, responsible for updating patient information.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UpdatePatientHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class UpdatePatientHandler(IPatientRepository patientRepository) : IRequestHandler<UpdatePatientCommand, OperationResult<Unit>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the <see cref="UpdatePatientCommand"/> to update patient information.
    /// </summary>
    /// <param name="request">The update patient command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An operation result indicating success or failure.</returns>
    public async Task<OperationResult<Unit>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetPatientById(request.PatientId);

        if (patient is null)
            return OperationResult<Unit>.Failure("Patient not found.");

        if (patient.IsArchived)
            return OperationResult<Unit>.Failure("Patient is archived.");

        patient.UpdatePatient(
            firstName: request.UpdatePatient.FirstName,
            lastName: request.UpdatePatient.LastName,
            gender: request.UpdatePatient.Gender,
            ethnicGroup: request.UpdatePatient.EthnicGroup,
            location: request.UpdatePatient.Location,
            age: request.UpdatePatient.Age,
            otherGroup: CheckNullOrEmpty(request.UpdatePatient.OtherGroup),
            otherData: CheckNullOrEmpty(request.UpdatePatient.OtherData),
            otherData2: CheckNullOrEmpty(request.UpdatePatient.OtherData2),
            otherData3: CheckNullOrEmpty(request.UpdatePatient.OtherData3),
            yearsInSchool: request.UpdatePatient.YearsInSchool);

        return OperationResult<Unit>.Success(Unit.Value);
    }
    private static string CheckNullOrEmpty(string value) => string.IsNullOrEmpty(value) ? null : value;
}

