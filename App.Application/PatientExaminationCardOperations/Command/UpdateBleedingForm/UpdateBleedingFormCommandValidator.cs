using App.Application.PatientExaminationCardOperations.CommandValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateBleedingForm;

/// <summary>
/// Validator for UpdateBleedingFormCommand
/// </summary>
public class UpdateBleedingFormCommandValidator : AbstractValidator<UpdateBleedingFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBleedingFormCommandValidator"/> class.
    /// </summary>
    public UpdateBleedingFormCommandValidator()
    {
        // Validate the CardId property
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId must not be empty.");

        // Validate the AssessmentModel property
        RuleFor(x => x.AssessmentModel)
            .NotNull().WithMessage("Assessment Model must not be null.")
            .SetValidator(new APIBleedingAssessmentModelValidator())
            .OverridePropertyName("Bleeding");
    }
}
