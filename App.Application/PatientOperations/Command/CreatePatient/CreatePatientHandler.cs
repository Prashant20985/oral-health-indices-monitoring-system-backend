using App.Application.Core;
using App.Domain.Models.Enums;
using App.Domain.Models.OralHealthExamination;
using App.Domain.Repository;
using MediatR;

namespace App.Application.PatientOperations.Command.CreatePatient;

/// <summary>
/// Handler for the <see cref="CreatePatientCommand"/> command, responsible for creating new patients.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CreatePatientHandler"/> class.
/// </remarks>
/// <param name="patientRepository">The repository for patient-related operations.</param>
internal sealed class CreatePatientHandler(IPatientRepository patientRepository) :
    IRequestHandler<CreatePatientCommand, OperationResult<Unit>>
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    /// <summary>
    /// Handles the <see cref="CreatePatientCommand"/> command to create a new patient.
    /// </summary>
    /// <param name="request">The create patient command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the success or failure of the create operation.</returns>
    public async Task<OperationResult<Unit>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        // Check if a patient with the same email already exists
        var checkIfPatientExists = await _patientRepository.GetPatientByEmail(request.CreatePatient.Email);

        if (checkIfPatientExists is not null)
            return OperationResult<Unit>.Failure("Patient with this email already exists.");

        // Create a new patient based on the command
        Patient patient = new(
            firstName: request.CreatePatient.FirstName,
            lastName: request.CreatePatient.LastName,
            email: request.CreatePatient.Email,
            gender: Enum.Parse<Gender>(request.CreatePatient.Gender),
            ethnicGroup: request.CreatePatient.EthnicGroup,
            location: request.CreatePatient.Location,
            age: request.CreatePatient.Age,
            otherGroup: CheckNullOrEmpty(request.CreatePatient.OtherGroup),
            otherData: CheckNullOrEmpty(request.CreatePatient.OtherData),
            otherData2: CheckNullOrEmpty(request.CreatePatient.OtherData2),
            otherData3: CheckNullOrEmpty(request.CreatePatient.OtherData3),
            yearsInSchool: request.CreatePatient.YearsInSchool,
            doctorId: request.DoctorId);

        // Add the new patient to the repository
        await _patientRepository.CreatePatient(patient);

        // Return success
        return OperationResult<Unit>.Success(Unit.Value);
    }

    /// <summary>
    ///  Checks if the value is null or empty and returns null if it is.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string CheckNullOrEmpty(string value) => string.IsNullOrEmpty(value) ? null : value;
}

