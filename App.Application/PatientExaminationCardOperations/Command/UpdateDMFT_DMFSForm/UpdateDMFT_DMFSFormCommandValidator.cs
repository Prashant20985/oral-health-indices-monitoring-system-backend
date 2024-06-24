using App.Application.PatientExaminationCardOperations.CommanValidators;
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

        // Validate AssessmentModel
        RuleFor(x => x.AssessmentModel)
            .NotNull()
            .SetValidator(new DMFT_DMFSAssessmentModelValidator());
    }
}
