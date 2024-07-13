using App.Application.PatientExaminationCardOperations.CommanValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByDoctor;

/// <summary>
/// Validator for <see cref="CreatePatientExaminationCardByDoctorCommand"/>
/// </summary>
public class CreatePatientExaminationCardByDoctorCommandValidator
    : AbstractValidator<CreatePatientExaminationCardByDoctorCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePatientExaminationCardByDoctorCommandValidator"/> class.
    /// </summary>
    public CreatePatientExaminationCardByDoctorCommandValidator()
    {
        // Validate PatientId
        RuleFor(x => x.PatientId)
            .NotNull()
            .NotEmpty();

        // Validate DoctorId
        RuleFor(x => x.DoctorId)
            .NotNull()
            .NotEmpty();

        // Validate Summary
        RuleFor(x => x.InputParams.Summary)
            .NotNull()
            .SetValidator(new SummaryValidator());

        // Validate RiskFactorAssessmentModel
        RuleFor(x => x.InputParams.RiskFactorAssessmentModel)
            .NotNull()
            .SetValidator(new RiskFactorAssessmentModelValidator());

        // Validate CreateDMFT_DMFSRequest Comment
        RuleFor(x => x.InputParams.DMFT_DMFS.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate DMFT_DMFSAssessmentModel
        RuleFor(x => x.InputParams.DMFT_DMFS.DMFT_DMFSAssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator());

        // Validate CreateDMFT_DMFSRequest ProstheticStatus
        string[] prostheticStatusValues = ["x", "0", "1", "2"];
        RuleFor(x => x.InputParams.DMFT_DMFS.ProstheticStatus)
            .NotEmpty()
            .Must(x => prostheticStatusValues.Contains(x)).WithMessage("Invalid Value.")
            .MaximumLength(1);

        // Validate CreateBeweRequest Comment
        RuleFor(x => x.InputParams.Bewe.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate BeweAssessmentModel
        RuleFor(x => x.InputParams.Bewe.BeweAssessmentModel)
            .NotNull()
            .SetValidator(new BeweAssessmentModelValidator());

        // Validate CreateAPIRequest Comment
        RuleFor(x => x.InputParams.API.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate APIAssessmentModel
        RuleFor(x => x.InputParams.API.APIAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());

        // Validate CreateBleedingRequest Comment
        RuleFor(x => x.InputParams.Bleeding.Comment)
            .NotEmpty()
            .MaximumLength(500);

        // Validate BleedingAssessmentModel
        RuleFor(x => x.InputParams.Bleeding.BleedingAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());
    }
}
