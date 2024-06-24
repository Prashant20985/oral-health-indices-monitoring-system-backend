using App.Application.PatientExaminationCardOperations.CommanValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.UpdateRiskFactorAssessmentForm;

/// <summary>
/// Validator for <see cref="UpdateRiskFactorAssessmentFormCommand"/>
/// </summary>
public class UpdateRiskFactorAssessmentFormCommandValidator : AbstractValidator<UpdateRiskFactorAssessmentFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRiskFactorAssessmentFormCommandValidator"/> class.
    /// </summary>
    public UpdateRiskFactorAssessmentFormCommandValidator()
    {
        // Validate the CardId property
        RuleFor(x => x.CardId)
            .NotEmpty();

        // Validate the AssessmentModel property
        RuleFor(x => x.AssessmentModel)
            .NotNull()
            .SetValidator(new RiskFactorAssessmentModelValidator());
    }
}
