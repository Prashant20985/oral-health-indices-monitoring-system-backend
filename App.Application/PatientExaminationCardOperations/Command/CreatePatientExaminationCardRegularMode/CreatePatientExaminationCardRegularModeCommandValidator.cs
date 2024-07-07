using App.Application.PatientExaminationCardOperations.CommanValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardRegularMode;

/// <summary>
/// Validator for <see cref="CreatePatientExaminationCardRegularModeCommand"/>
/// </summary>
public class CreatePatientExaminationCardRegularModeCommandValidator
    : AbstractValidator<CreatePatientExaminationCardRegularModeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePatientExaminationCardRegularModeCommandValidator"/> class.
    /// </summary>
    public CreatePatientExaminationCardRegularModeCommandValidator()
    {
        // Validate PatientId
        RuleFor(x => x.PatientId)
            .NotNull()
            .NotEmpty();

        // Validate DoctorId
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty();

        // Validate IsStudent
        RuleFor(x => x.IsStudent)
            .NotNull();

        // Validate RiskFactorAssessmentModel
        RuleFor(x => x.InputParams.RiskFactorAssessmentModel)
            .NotNull()
            .SetValidator(new RiskFactorAssessmentModelValidator());

        // Validate CreateDMFT_DMFSRequest Comment
        RuleFor(x => x.InputParams.CreateDMFT_DMFSRequest.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate DMFT_DMFSAssessmentModel
        RuleFor(x => x.InputParams.CreateDMFT_DMFSRequest.DMFT_DMFSAssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator());

        // Validate CreateBeweRequest Comment
        RuleFor(x => x.InputParams.CreateBeweRequest.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate BeweAssessmentModel
        RuleFor(x => x.InputParams.CreateBeweRequest.BeweAssessmentModel)
            .NotNull()
            .SetValidator(new BeweAssessmentModelValidator());

        // Validate CreateAPIRequest Comment
        RuleFor(x => x.InputParams.CreateAPIRequest.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate APIAssessmentModel
        RuleFor(x => x.InputParams.CreateAPIRequest.APIAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());

        // Validate CreateBleedingRequest Comment
        RuleFor(x => x.InputParams.CreateBleedingRequest.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate BleedingAssessmentModel
        RuleFor(x => x.InputParams.CreateBleedingRequest.BleedingAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());
    }
}
