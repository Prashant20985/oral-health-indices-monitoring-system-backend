using App.Domain.Models.Common.APIBleeding;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommanValidators;

/// <summary>
/// Validator for API Bleeding Assessment Model
/// </summary>
public class APIBleedingAssessmentModelValidator : AbstractValidator<APIBleedingAssessmentModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="APIBleedingAssessmentModelValidator"/> class.
    /// </summary>
    public APIBleedingAssessmentModelValidator()
    {
        // Validate Quadrant1
        RuleFor(x => x.Quadrant1)
            .NotNull().WithMessage("Quadrant1 must not be null.")
            .SetValidator(new APIBleedingQuadrantValidator());

        // Validate Quadrant2
        RuleFor(x => x.Quadrant2)
            .NotNull().WithMessage("Quadrant2 must not be null.")
            .SetValidator(new APIBleedingQuadrantValidator());

        // Validate Quadrant3
        RuleFor(x => x.Quadrant3)
            .NotNull().WithMessage("Quadrant3 must not be null.")
            .SetValidator(new APIBleedingQuadrantValidator());

        // Validate Quadrant4
        RuleFor(x => x.Quadrant4)
            .NotNull().WithMessage("Quadrant4 must not be null.")
            .SetValidator(new APIBleedingQuadrantValidator());
    }
}

/// <summary>
/// Validator for API Bleeding Quadrant
/// </summary>
public class APIBleedingQuadrantValidator : AbstractValidator<Quadrant>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="APIBleedingQuadrantValidator"/> class.
    /// </summary>
    public APIBleedingQuadrantValidator()
    {
        // Validate Value1
        RuleFor(x => x.Value1)
             .Must(BePlusOrMinusOrEmpty)
             .WithMessage("Value1 must be '+' or '-'.");

        // Validate Value2
        RuleFor(x => x.Value2)
            .Must(BePlusOrMinusOrEmpty)
            .WithMessage("Value2 must be '+' or '-'.");

        // Validate Value3
        RuleFor(x => x.Value3)
            .Must(BePlusOrMinusOrEmpty)
            .WithMessage("Value3 must be '+' or '-'.");

        // Validate Value4
        RuleFor(x => x.Value4)
            .Must(BePlusOrMinusOrEmpty)
            .WithMessage("Value4 must be '+' or '-'.");

        // Validate Value5
        RuleFor(x => x.Value5)
            .Must(BePlusOrMinusOrEmpty)
            .WithMessage("Value5 must be '+' or '-'.");

        // Validate Value6
        RuleFor(x => x.Value6)
            .Must(BePlusOrMinusOrEmpty)
            .WithMessage("Value6 must be '+' or '-'.");

        // Validate Value7
        RuleFor(x => x.Value7)
            .Must(BePlusOrMinusOrEmpty)
            .WithMessage("Value7 must be '+' or '-'.");
    }

    /// <summary>
    /// Validate if the value is '+' or '-'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is '+' or '-', otherwise false</returns>
    private bool BePlusOrMinusOrEmpty(string value) =>
        value == "+" || value == "-" || string.IsNullOrEmpty(value);
}
