using App.Application.PatientExaminationCardOperations.CommanValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateBeweForm;

/// <summary>
/// Validator for <see cref="UpdateBeweFormCommand"/>
/// </summary>
public class UpdateBeweFormCommandValidator : AbstractValidator<UpdateBeweFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBeweFormCommandValidator"/> class.
    /// </summary>
    public UpdateBeweFormCommandValidator()
    {
        // Validates CardId property
        RuleFor(x => x.CardId)
            .NotEmpty();

        // Validates AssessmentModel property
        RuleFor(x => x.AssessmentModel)
            .NotNull()
            .SetValidator(new BeweAssessmentModelValidator());
    }
}
