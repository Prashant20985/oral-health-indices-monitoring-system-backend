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

        // Validate the InputParams
        // Validate the DoctorId
        RuleFor(x => x.InputParams.DoctorId)
            .NotNull()
            .NotEmpty();

        // Validate the StudentId
        RuleFor(x => x.InputParams.StudentId)
            .NotNull()
            .NotEmpty();

        // Validate the BeweResult
        RuleFor(x => x.InputParams.BeweResult)
            .NotNull();

        // Validate the DMFT_Result
        RuleFor(x => x.InputParams.DMFT_Result)
            .NotNull();

        // Validate the DMFS_Result
        RuleFor(x => x.InputParams.DMFS_Result)
            .NotNull();

        // Validate the APIResult
        RuleFor(x => x.InputParams.APIResult)
            .NotNull();

        // Validate the BleedingResult
        RuleFor(x => x.InputParams.BleedingResult)
            .NotNull();

        // Validate the RiskFactorAssessmentModel
        RuleFor(x => x.InputParams.RiskFactorAssessmentModel)
            .NotNull()
            .SetValidator(new RiskFactorAssessmentModelValidator());

        // Validate the DMFT_DMFSAssessmentModel
        RuleFor(x => x.InputParams.DMFT_DMFSAssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator());

        // Validate the BeweAssessmentModel
        RuleFor(x => x.InputParams.BeweAssessmentModel)
            .NotNull()
            .SetValidator(new BeweAssessmentModelValidator());

        // Validate the APIAssessmentModel
        RuleFor(x => x.InputParams.APIAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());

        // Validate the BleedingAssessmentModel
        RuleFor(x => x.InputParams.BleedingAssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator());
    }
}
