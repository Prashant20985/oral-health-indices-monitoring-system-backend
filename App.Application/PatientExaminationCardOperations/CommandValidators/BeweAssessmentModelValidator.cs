using App.Domain.Models.Common.Bewe;
using App.Domain.Models.Common.Tooth;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommandValidators;

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
            .SetValidator(new Sectant1Validator())
            .OverridePropertyName("Sectant 1");

        // Validate Sectant2
        RuleFor(x => x.Sectant2)
            .NotNull()
            .SetValidator(new Sectant2Validator())
            .OverridePropertyName("Sectant 2");

        // Validate Sectant3
        RuleFor(x => x.Sectant3)
            .NotNull()
            .SetValidator(new Sectant3Validator())
            .OverridePropertyName("Sectant 3");

        // Validate Sectant4
        RuleFor(x => x.Sectant4)
            .NotNull()
            .SetValidator(new Sectant4Validator())
            .OverridePropertyName("Sectant 4");

        // Validate Sectant5
        RuleFor(x => x.Sectant5)
            .NotNull()
            .SetValidator(new Sectant5Validator())
            .OverridePropertyName("Sectant 5");

        // Validate Sectant6
        RuleFor(x => x.Sectant6)
            .NotNull()
            .SetValidator(new Sectant6Validator())
            .OverridePropertyName("Sectant 6");
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
        RuleFor(x => x.Tooth_14).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_14");

        RuleFor(x => x.Tooth_15).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_15");

        RuleFor(x => x.Tooth_16).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_16");

        RuleFor(x => x.Tooth_17).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_17");
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
        RuleFor(x => x.Tooth_11)
            .SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_11");

        RuleFor(x => x.Tooth_12)
            .SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_12");

        RuleFor(x => x.Tooth_13)
            .SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_13");

        RuleFor(x => x.Tooth_21)
            .SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_21");

        RuleFor(x => x.Tooth_22)
            .SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_22");

        RuleFor(x => x.Tooth_23)
            .SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_23");
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
        RuleFor(x => x.Tooth_24).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_24");

        RuleFor(x => x.Tooth_25).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_25");

        RuleFor(x => x.Tooth_26).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_26");

        RuleFor(x => x.Tooth_27).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_27");

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
        RuleFor(x => x.Tooth_34).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_34");

        RuleFor(x => x.Tooth_35).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_35");

        RuleFor(x => x.Tooth_36).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_36");

        RuleFor(x => x.Tooth_37).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_37");
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
        RuleFor(x => x.Tooth_31).SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_31");

        RuleFor(x => x.Tooth_32).SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_32");

        RuleFor(x => x.Tooth_33).SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_33");

        RuleFor(x => x.Tooth_41).SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_41");

        RuleFor(x => x.Tooth_42).SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_42");

        RuleFor(x => x.Tooth_43).SetValidator(new BeweFourSurfaceToothValidator())
            .OverridePropertyName("Tooth_43");
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
        RuleFor(x => x.Tooth_44).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_44");

        RuleFor(x => x.Tooth_45).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_45");

        RuleFor(x => x.Tooth_46).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_46");

        RuleFor(x => x.Tooth_47).SetValidator(new BeweFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_47");
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

        RuleFor(x => x.M).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Mesial
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Distal
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Buccal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Lingual
        RuleFor(x => x.O).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Occusal
    }

    /// <summary>
    /// Validate if the value is 'x', '0', '1', '2' or '3'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is 'x', '0', '1', '2' or '3', otherwise false</returns>
    private bool BeValidValue(string value) =>
    value == "0" || value == "1" || value == "2" || value == "3" || value == "" || value == "x";
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
        RuleFor(x => x.M).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Mesial
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Distal
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Buccal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("Must be between '0' to '3' or 'x'."); // Validate Lingual
    }

    /// <summary>
    /// Validate if the value is 'x', '0', '1', '2' or '3'
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is 'x', '0', '1', '2' or '3', otherwise false</returns>
    private bool BeValidValue(string value) =>
        value == "0" || value == "1" || value == "2" || value == "3" || value == "" || value == "x";
}

