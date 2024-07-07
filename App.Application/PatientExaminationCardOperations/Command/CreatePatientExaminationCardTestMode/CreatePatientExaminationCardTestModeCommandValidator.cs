using App.Application.PatientExaminationCardOperations.CommanValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardTestMode;

/// <summary>
/// Validator for the CreatePatientExaminationCardTestModeCommand
/// </summary>
public class CreatePatientExaminationCardTestModeCommandValidator
    : AbstractValidator<CreatePatientExaminationCardTestModeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePatientExaminationCardTestModeCommandValidator"/> class.
    /// </summary>
    public CreatePatientExaminationCardTestModeCommandValidator()
    {
        // Validate the PatientId
        RuleFor(x => x.PatientId)
            .NotNull()
            .NotEmpty();

        // Validate the StudentId
        RuleFor(x => x.StudentId)
            .NotNull()
            .NotEmpty();

        // Validate the InputParams
        // Validate the DoctorId
        RuleFor(x => x.InputParams.DoctorId)
            .NotNull()
            .NotEmpty();

        // Validate the RiskFactorAssessmentModel
        RuleFor(x => x.InputParams.RiskFactorAssessmentModel)
            .NotNull()
            .SetValidator(new RiskFactorAssessmentModelValidator());

        // Validate the CreateDMFT_DMFSRequest
        RuleFor(x => x.InputParams.CreateDMFT_DMFSRequest.DMFT_DMFSAssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator());

        RuleFor(x => x.InputParams.CreateDMFT_DMFSRequest.DMFTResult)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.InputParams.CreateDMFT_DMFSRequest.DMFSResult)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.InputParams.CreateDMFT_DMFSRequest.StudentComment)
            .NotNull()
            .MaximumLength(500);

        // Validate the CreateBeweRequest
        RuleFor(x => x.InputParams.CreateBeweRequest.BeweAssessmentModel)
            .NotNull()
            .SetValidator(new BeweAssessmentModelValidator());

        RuleFor(x => x.InputParams.CreateBeweRequest.BeweResult)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.InputParams.CreateBeweRequest.StudentComment)
            .NotNull()
            .MaximumLength(500);

        // Validate the CreateAPIRequest
        RuleFor(x => x.InputParams.CreateAPIRequest.APIAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());

        RuleFor(x => x.InputParams.CreateAPIRequest.APIResult)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.InputParams.CreateAPIRequest.Maxilla)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.InputParams.CreateAPIRequest.Mandible)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.InputParams.CreateAPIRequest.StudentComment)
            .NotNull()
            .MaximumLength(500);

        // Validate the CreateBleedingRequest
        RuleFor(x => x.InputParams.CreateBleedingRequest.BleedingAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());

        RuleFor(x => x.InputParams.CreateBleedingRequest.BleedingResult)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.InputParams.CreateBleedingRequest.Maxilla)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.InputParams.CreateBleedingRequest.Mandible)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.InputParams.CreateBleedingRequest.StudentComment)
            .NotNull()
            .MaximumLength(500);
    }
}
