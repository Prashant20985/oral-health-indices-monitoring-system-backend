using App.Domain.DTOs.Common.Request;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommanValidators;

public class SummaryValidator
    : AbstractValidator<SummaryRequestDto>
{
    /// <summary>
    /// Validator for SummaryRequestDto
    /// </summary>
    public SummaryValidator()
    {
        RuleFor(x => x.PatientRecommendations)
            .NotEmpty()
            .MaximumLength(500);

        string[] needForDentalIntervensionValues = ["0", "1", "2", "3", "4"];
        RuleFor(x => x.NeedForDentalInterventions)
            .NotEmpty()
            .Must(x => needForDentalIntervensionValues.Contains(x)).WithMessage("Invalid Value.")
            .MaximumLength(1);

        RuleFor(x => x.ProposedTreatment)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}
