using App.Application.PatientExaminationCardOperations.CommanValidators;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.UpdatePatientExaminationCardSummary;

/// <summary>
/// Validator for Update Patient Examination Card Summary Command.
/// </summary>
public class UpdatePatientExaminationCardSummaryCommandValidator
    : AbstractValidator<UpdatePatientExaminationCardSummaryCommand>
{
    public UpdatePatientExaminationCardSummaryCommandValidator()
    {
        RuleFor(x => x.CardId)
            .NotEmpty().WithMessage("CardId is required.");

        RuleFor(x => x.Summary)
            .NotNull().WithMessage("Summary is required.")
            .SetValidator(new SummaryValidator());
    }
}
