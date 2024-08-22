using App.Application.PatientExaminationCardOperations.CommandValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;

/// <summary>
/// Validator for Update Patient Examination Card Summary Command.
/// </summary>
public class UpdatePatientExaminationCardSummaryCommandValidator
    : AbstractValidator<UpdatePatientExaminationCardSummaryCommand>
{
    /// <summary>
    /// Validates the UpdatePatientExaminationCardSummaryCommand.
    /// </summary>
    public UpdatePatientExaminationCardSummaryCommandValidator()
    {
        // Validate CardId
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId is required.");

        // Validate Summary
        RuleFor(x => x.Summary)
            .NotNull().WithMessage("Summary is required.")
            .SetValidator(new SummaryValidator())
            .OverridePropertyName("Summary");
    }
}
