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
        RuleFor(x => x.InputParams.DoctorId)
            .NotNull()
            .NotEmpty();

        // Validate RiskFactorAssessmentModel
        RuleFor(x => x.InputParams.RiskFactorAssessmentModel)
            .NotNull()
            .SetValidator(new RiskFactorAssessmentModelValidator());

        // Validate DMFT_DMFSAssessmentModel
        RuleFor(x => x.InputParams.DMFT_DMFSAssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator());

        // Validate BeweAssessmentModel
        RuleFor(x => x.InputParams.BeweAssessmentModel)
            .NotNull()
            .SetValidator(new BeweAssessmentModelValidator());

        // Validate APIAssessmentModel
        RuleFor(x => x.InputParams.APIAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());

        // Validate BleedingAssessmentModel
        RuleFor(x => x.InputParams.BleedingAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());
    }
}
