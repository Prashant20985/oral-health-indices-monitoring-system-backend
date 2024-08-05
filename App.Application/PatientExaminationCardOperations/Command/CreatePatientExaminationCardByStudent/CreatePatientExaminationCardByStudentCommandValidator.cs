using App.Application.PatientExaminationCardOperations.CommandValidators;
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

        // Validate PatientExaminationCardComment
        RuleFor(x => x.InputParams.PatientExaminationCardComment)
            .MaximumLength(500)
            .OverridePropertyName("Patient Examination Card Comment");

        // Validate Summary
        RuleFor(x => x.InputParams.Summary)
            .NotNull()
            .SetValidator(new SummaryValidator())
            .OverridePropertyName("Summary");

        // Validate RiskFactorAssessmentModel
        RuleFor(x => x.InputParams.RiskFactorAssessmentModel)
            .NotNull()
            .SetValidator(new RiskFactorAssessmentModelValidator())
            .OverridePropertyName("Risk Factor Assessment");

        // Validate CreateDMFT_DMFSRequest Comment
        RuleFor(x => x.InputParams.DMFT_DMFS.Comment)
            .MaximumLength(500)
            .OverridePropertyName("DMFT/DMFS Comment");

        // Validate DMFT_DMFSAssessmentModel
        RuleFor(x => x.InputParams.DMFT_DMFS.AssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator())
            .OverridePropertyName("DMFT/DMFS");

        // Validate CreateDMFT_DMFSRequest ProstheticStatus
        string[] prostheticStatusValues = ["x", "0", "1", "2"];
        RuleFor(x => x.InputParams.DMFT_DMFS.ProstheticStatus)
            .NotEmpty()
            .Must(x => prostheticStatusValues.Contains(x)).WithMessage("Invalid Value.")
            .MaximumLength(1)
            .OverridePropertyName("Prosthetic Status");

        // Validate CreateBeweRequest Comment
        RuleFor(x => x.InputParams.Bewe.Comment)
            .MaximumLength(500)
            .OverridePropertyName("Bewe Comment");

        // Validate BeweAssessmentModel
        RuleFor(x => x.InputParams.Bewe.AssessmentModel)
            .NotNull()
            .SetValidator(new BeweAssessmentModelValidator())
            .OverridePropertyName("BEWE");

        // Validate CreateAPIRequest Comment
        RuleFor(x => x.InputParams.API.Comment)
            .MaximumLength(500)
            .OverridePropertyName("API Comment");

        // Validate APIAssessmentModel
        RuleFor(x => x.InputParams.API.AssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator())
            .OverridePropertyName("API");

        // Validate CreateBleedingRequest Comment
        RuleFor(x => x.InputParams.Bleeding.Comment)
            .MaximumLength(500)
            .OverridePropertyName("Bleeding Comment");

        // Validate BleedingAssessmentModel
        RuleFor(x => x.InputParams.Bleeding.AssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator())
            .OverridePropertyName("Bleeding");
    }
}
