using App.Domain.Models.Common.APIBleeding;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommandValidators;

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
            .SetValidator(new APIBleedingQuadrant1Validator())
            .OverridePropertyName("Quadrant1");

        // Validate Quadrant2
        RuleFor(x => x.Quadrant2)
            .NotNull().WithMessage("Quadrant2 must not be null.")
            .SetValidator(new APIBleedingQuadrant2Validator())
            .OverridePropertyName("Quadrant2");

        // Validate Quadrant3
        RuleFor(x => x.Quadrant3)
            .NotNull().WithMessage("Quadrant3 must not be null.")
            .SetValidator(new APIBleedingQuadrant3Validator())
            .OverridePropertyName("Quadrant3");

        // Validate Quadrant4
        RuleFor(x => x.Quadrant4)
            .NotNull().WithMessage("Quadrant4 must not be null.")
            .SetValidator(new APIBleedingQuadrant4Validator())
            .OverridePropertyName("Quadrant4");
    }
}

/// <summary>
/// Validator for API Bleeding Quadrant 1
/// </summary>
public class APIBleedingQuadrant1Validator : AbstractValidator<Quadrant>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="APIBleedingQuadrant1Validator"/> class.
    /// </summary>
    public APIBleedingQuadrant1Validator()
    {
        // Validate Value1
        RuleFor(x => x.Value1)
             .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q11")
             .WithMessage("q11 must be '+' or '-'.");

        // Validate Value2
        RuleFor(x => x.Value2)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q12")
            .WithMessage("q12 must be '+' or '-'.");

        // Validate Value3
        RuleFor(x => x.Value3)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q13")
            .WithMessage("q13 must be '+' or '-'.");

        // Validate Value4
        RuleFor(x => x.Value4)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q14")
            .WithMessage("q14 must be '+' or '-'.");

        // Validate Value5
        RuleFor(x => x.Value5)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q15")
            .WithMessage("q15 must be '+' or '-'.");

        // Validate Value6
        RuleFor(x => x.Value6)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q16")
            .WithMessage("q16 must be '+' or '-'.");

        // Validate Value7
        RuleFor(x => x.Value7)
            .Must(BePlusOrMinusOrEmpty)
            .OverridePropertyName("q17")
            .WithMessage("q17 must be '+' or '-'.");
    }

    /// <summary>
    /// Validate if the value is '+' or '-'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is '+' or '-', otherwise false</returns>
    private bool BePlusOrMinusOrEmpty(string value) =>
        value == "+" || value == "-" || value == "x" || string.IsNullOrEmpty(value);
}

/// <summary>
/// Validator for API Bleeding Quadrant 2
/// </summary>
public class APIBleedingQuadrant2Validator : AbstractValidator<Quadrant>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="APIBleedingQuadrant2Validator"/> class.
    /// </summary>
    public APIBleedingQuadrant2Validator()
    {
        // Validate Value1
        RuleFor(x => x.Value1)
             .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q21")
             .WithMessage("q21 must be '+' or '-'.");

        // Validate Value2
        RuleFor(x => x.Value2)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q22")
            .WithMessage("q22 must be '+' or '-'.");

        // Validate Value3
        RuleFor(x => x.Value3)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q23")
            .WithMessage("q23 must be '+' or '-'.");

        // Validate Value4
        RuleFor(x => x.Value4)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q24")
            .WithMessage("q24 must be '+' or '-'.");

        // Validate Value5
        RuleFor(x => x.Value5)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q25")
            .WithMessage("q25 must be '+' or '-'.");

        // Validate Value6
        RuleFor(x => x.Value6)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q26")
            .WithMessage("q26 must be '+' or '-'.");

        // Validate Value7
        RuleFor(x => x.Value7)
            .Must(BePlusOrMinusOrEmpty)
            .OverridePropertyName("q27")
            .WithMessage("q27 must be '+' or '-'.");
    }

    /// <summary>
    /// Validate if the value is '+' or '-'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is '+' or '-', otherwise false</returns>
    private bool BePlusOrMinusOrEmpty(string value) =>
        value == "+" || value == "-" || value == "x" || string.IsNullOrEmpty(value);
}

/// <summary>
/// Validator for API Bleeding Quadrant 3
/// </summary>
public class APIBleedingQuadrant3Validator : AbstractValidator<Quadrant>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="APIBleedingQuadrant3Validator"/> class.
    /// </summary>
    public APIBleedingQuadrant3Validator()
    {
        // Validate Value1
        RuleFor(x => x.Value1)
             .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q31")
             .WithMessage("q31 must be '+' or '-'.");

        // Validate Value2
        RuleFor(x => x.Value2)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q32")
            .WithMessage("q32 must be '+' or '-'.");

        // Validate Value3
        RuleFor(x => x.Value3)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q33")
            .WithMessage("q33 must be '+' or '-'.");

        // Validate Value4
        RuleFor(x => x.Value4)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q34")
            .WithMessage("q34 must be '+' or '-'.");

        // Validate Value5
        RuleFor(x => x.Value5)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q35")
            .WithMessage("q35 must be '+' or '-'.");

        // Validate Value6
        RuleFor(x => x.Value6)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q36")
            .WithMessage("q36 must be '+' or '-'.");

        // Validate Value7
        RuleFor(x => x.Value7)
            .Must(BePlusOrMinusOrEmpty)
            .OverridePropertyName("q37")
            .WithMessage("q37 must be '+' or '-'.");
    }

    /// <summary>
    /// Validate if the value is '+' or '-'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is '+' or '-', otherwise false</returns>
    private bool BePlusOrMinusOrEmpty(string value) =>
        value == "+" || value == "-" || value == "x" || string.IsNullOrEmpty(value);
}

/// <summary>
/// Validator for API Bleeding Quadrant 4
/// </summary>
public class APIBleedingQuadrant4Validator : AbstractValidator<Quadrant>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="APIBleedingQuadrant4Validator"/> class.
    /// </summary>
    public APIBleedingQuadrant4Validator()
    {
        // Validate Value1
        RuleFor(x => x.Value1)
             .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q41")
             .WithMessage("q41 must be '+' or '-'.");

        // Validate Value2
        RuleFor(x => x.Value2)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q42")
            .WithMessage("q42 must be '+' or '-'.");

        // Validate Value3
        RuleFor(x => x.Value3)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q43")
            .WithMessage("q43 must be '+' or '-'.");

        // Validate Value4
        RuleFor(x => x.Value4)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q44")
            .WithMessage("q44 must be '+' or '-'.");

        // Validate Value5
        RuleFor(x => x.Value5)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q45")
            .WithMessage("q45 must be '+' or '-'.");

        // Validate Value6
        RuleFor(x => x.Value6)
            .Must(BePlusOrMinusOrEmpty)
             .OverridePropertyName("q46")
            .WithMessage("q46 must be '+' or '-'.");

        // Validate Value7
        RuleFor(x => x.Value7)
            .Must(BePlusOrMinusOrEmpty)
            .OverridePropertyName("q47")
            .WithMessage("q47 must be '+' or '-'.");
    }

    /// <summary>
    /// Validate if the value is '+' or '-'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is '+' or '-', otherwise false</returns>
    private bool BePlusOrMinusOrEmpty(string value) =>
        value == "+" || value == "-" || value == "x" || string.IsNullOrEmpty(value);
}