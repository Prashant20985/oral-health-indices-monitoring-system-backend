using App.Application.PatientExaminationCardOperations.CommandValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateDMFT_DMFSForm;

/// <summary>
/// Validator for <see cref="UpdateDMFT_DMFSFormCommand"/>
/// </summary>
public class UpdateDMFT_DMFSFormCommandValidator : AbstractValidator<UpdateDMFT_DMFSFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDMFT_DMFSFormCommandValidator"/> class.
    /// </summary>
    public UpdateDMFT_DMFSFormCommandValidator()
    {
        // Validate CardId
        RuleFor(x => x.CardId)
            .NotEmpty();

        // Validate ProstheticStatus
        string[] prostheticStatusValues = ["x", "0", "1", "2"];
        RuleFor(x => x.UpdateDMFT_DMFS.ProstheticStatus)
            .NotEmpty()
            .Must(x => prostheticStatusValues.Contains(x)).WithMessage("Invalid Value.")
            .MaximumLength(1).OverridePropertyName("Prosthetic Status");

        // Validate DMFTResult
        RuleFor(x => x.UpdateDMFT_DMFS.DMFTResult)
            .NotNull().OverridePropertyName("DMFT Result");

        // Validate DMFSResult
        RuleFor(x => x.UpdateDMFT_DMFS.DMFSResult)
            .NotNull().OverridePropertyName("DMFS Result");

        // Validate AssessmentModel
        RuleFor(x => x.UpdateDMFT_DMFS.AssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator())
            .OverridePropertyName("DMFT/DMFS");
    }
}
