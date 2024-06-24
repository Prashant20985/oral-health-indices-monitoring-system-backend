using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommanValidators;

/// <summary>
/// Validator for BEWE Assessment Model
/// </summary>
public class BeweAssessmentModelValidator : AbstractValidator<BeweAssessmentModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BeweAssessmentModelValidator"/> class.
    /// </summary>
    public BeweAssessmentModelValidator()
    {
        // Validate Sectant1
        RuleFor(x => x.Sectant1)
            .NotNull()
            .SetValidator(new Sectant1Validator());

        // Validate Sectant2
        RuleFor(x => x.Sectant2)
            .NotNull()
            .SetValidator(new Sectant2Validator());

        // Validate Sectant3
        RuleFor(x => x.Sectant3)
            .NotNull()
            .SetValidator(new Sectant3Validator());

        // Validate Sectant4
        RuleFor(x => x.Sectant4)
            .NotNull()
            .SetValidator(new Sectant4Validator());

        // Validate Sectant5
        RuleFor(x => x.Sectant5)
            .NotNull()
            .SetValidator(new Sectant5Validator());

        // Validate Sectant6
        RuleFor(x => x.Sectant6)
            .NotNull()
            .SetValidator(new Sectant6Validator());
    }
}

/// <summary>
/// Validator for Sectant1
/// </summary>
public class Sectant1Validator : AbstractValidator<Sectant1>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sectant1Validator"/> class.
    /// </summary>
    public Sectant1Validator()
    {
        // Validate Tooth_14, Tooth_15, Tooth_16, Tooth_17
        RuleForEach(x => new[]
        {
            x.Tooth_14,
            x.Tooth_15,
            x.Tooth_16,
            x.Tooth_17
        }).SetValidator(new BeweFiveSurfaceToothValidator());
    }
}

/// <summary>
/// Validator for Sectant2
/// </summary>
public class Sectant2Validator : AbstractValidator<Sectant2>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sectant2Validator"/> class.
    /// </summary>
    public Sectant2Validator()
    {
        // Validate Tooth_11, Tooth_12, Tooth_13, Tooth_21, Tooth_22, Tooth_23
        RuleForEach(x => new[]
        {
            x.Tooth_11,
            x.Tooth_12,
            x.Tooth_13,
            x.Tooth_21,
            x.Tooth_22,
            x.Tooth_23
        }).SetValidator(new BeweFourSurfaceToothValidator());
    }
}

/// <summary>
/// Validator for Sectant3
/// </summary>
public class Sectant3Validator : AbstractValidator<Sectant3>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sectant3Validator"/> class.
    /// </summary>
    public Sectant3Validator()
    {
        // Validate Tooth_24, Tooth_25, Tooth_26, Tooth_27
        RuleForEach(x => new[]
        {
            x.Tooth_24,
            x.Tooth_25,
            x.Tooth_26,
            x.Tooth_27
        }).SetValidator(new BeweFiveSurfaceToothValidator());
    }
}

/// <summary>
/// Validator for Sectant4
/// </summary>
public class Sectant4Validator : AbstractValidator<Sectant4>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sectant4Validator"/> class.
    /// </summary>
    public Sectant4Validator()
    {
        // Validate Tooth_34, Tooth_35, Tooth_36, Tooth_37
        RuleForEach(x => new[]
        {
            x.Tooth_34,
            x.Tooth_35,
            x.Tooth_36,
            x.Tooth_37
        }).SetValidator(new BeweFiveSurfaceToothValidator());
    }
}

/// <summary>
/// Validator for Sectant5
/// </summary>
public class Sectant5Validator : AbstractValidator<Sectant5>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sectant5Validator"/> class.
    /// </summary>
    public Sectant5Validator()
    {
        // Validate Tooth_31, Tooth_32, Tooth_33, Tooth_41, Tooth_42, Tooth_43
        RuleForEach(x => new[]
        {
            x.Tooth_31,
            x.Tooth_32,
            x.Tooth_33,
            x.Tooth_41,
            x.Tooth_42,
            x.Tooth_43
        }).SetValidator(new BeweFourSurfaceToothValidator());
    }
}

/// <summary>
/// Validator for Sectant6
/// </summary>
public class Sectant6Validator : AbstractValidator<Sectant6>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sectant6Validator"/> class.
    /// </summary>
    public Sectant6Validator()
    {
        // Validate Tooth_44, Tooth_45, Tooth_46, Tooth_47
        RuleForEach(x => new[]
        {
            x.Tooth_44,
            x.Tooth_45,
            x.Tooth_46,
            x.Tooth_47
        }).SetValidator(new BeweFiveSurfaceToothValidator());
    }
}

/// <summary>
/// Validator for Five Surface Tooth BEWE
/// </summary>
public class BeweFiveSurfaceToothValidator : AbstractValidator<FiveSurfaceToothBEWE>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BeweFiveSurfaceToothValidator"/> class.
    /// </summary>
    public BeweFiveSurfaceToothValidator()
    {

        RuleFor(x => x.M).Must(BeValidValue).WithMessage("Invalid value"); // Validate Mesial
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("Invalid value"); // Validate Distal
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("Invalid value"); // Validate Buccal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("Invalid value"); // Validate Lingual
        RuleFor(x => x.O).Must(BeValidValue).WithMessage("Invalid value"); // Validate Occusal
    }

    /// <summary>
    /// Validate if the value is 'x', '1', '2' or '3'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is 'x', '1', '2' or '3', otherwise false</returns>
    private bool BeValidValue(string value) =>
        value == "x" || value == "1" || value == "2" || value == "3";
}

/// <summary>
/// Validator for Four Surface Tooth BEWE
/// </summary>
public class BeweFourSurfaceToothValidator : AbstractValidator<FourSurfaceTooth>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BeweFourSurfaceToothValidator"/> class.
    /// </summary>
    public BeweFourSurfaceToothValidator()
    {
        RuleFor(x => x.M).Must(BeValidValue).WithMessage("Invalid value"); // Validate Mesial
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("Invalid value"); // Validate Distal
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("Invalid value"); // Validate Buccal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("Invalid value"); // Validate Lingual
    }

    /// <summary>
    /// Validate if the value is 'x', '1', '2' or '3'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is 'x', '1', '2' or '3', otherwise false</returns>
    private bool BeValidValue(string value) =>
        value == "x" || value == "1" || value == "2" || value == "3";
}

