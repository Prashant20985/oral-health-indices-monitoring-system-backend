using App.Domain.DTOs.Common.Request;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommandValidators;

public class SummaryValidator
    : AbstractValidator<SummaryRequestDto>
{
    /// <summary>
    /// Validator for SummaryRequestDto
    /// </summary>
    public SummaryValidator()
    {
        // Validate PatientRecommendations
        RuleFor(x => x.PatientRecommendations)
            .MaximumLength(500);
        
        // Validate NeedForDentalInterventions
        string[] needForDentalIntervensionValues = ["0", "1", "2", "3", "4"];
        RuleFor(x => x.NeedForDentalInterventions)
            .NotNull()
            .Must(x => needForDentalIntervensionValues.Contains(x)).WithMessage("Invalid Value.")
            .MaximumLength(1);

        // Validate ProposedTreatment
        RuleFor(x => x.ProposedTreatment)
            .MaximumLength(500);

        // Validate Description
        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
