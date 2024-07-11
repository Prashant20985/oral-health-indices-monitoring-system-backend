using App.Application.PatientExaminationCardOperations.CommanValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CreatePatientExaminationCardByStudent;

internal class CreatePatientExaminationCardByStudentCommandValidator
    : AbstractValidator<CreatePatientExaminationCardByStudentCommand>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePatientExaminationCardByDoctorCommandValidator"/> class.
    /// </summary>
    public CreatePatientExaminationCardByStudentCommandValidator()
    {
        // Validate PatientId
        RuleFor(x => x.PatientId)
            .NotNull()
            .NotEmpty();

        // Validate DoctorId
        RuleFor(x => x.StudentId)
            .NotNull()
            .NotEmpty();

        // Validate AssignedDoctorId
        RuleFor(x => x.InputParams.AssignedDoctorId)
            .NotNull()
            .NotEmpty();

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
