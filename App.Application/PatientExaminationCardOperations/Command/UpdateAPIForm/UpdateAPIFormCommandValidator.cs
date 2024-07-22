using App.Application.PatientExaminationCardOperations.CommandValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateAPIForm;

/// <summary>
/// validator for UpdateAPIFormCommand
/// </summary>
public class UpdateAPIFormCommandValidator : AbstractValidator<UpdateAPIFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAPIFormCommandValidator"/> class.
    /// </summary>
    public UpdateAPIFormCommandValidator()
    {
        // Validates CardId
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId must not be empty.");

        // Validates AssessmentModel
        RuleFor(x => x.AssessmentModel)
            .NotNull()
            .SetValidator(new APIBleedingAssessmentModelValidator())
            .OverridePropertyName("API");
    }
}
