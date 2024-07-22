using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.Tooth;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommandValidators;

/// <summary>
/// Validator for DMFT_DMFS Assessment Model
/// </summary>
public class DMFT_DMFSAssessmentModelValidator : AbstractValidator<DMFT_DMFSAssessmentModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DMFT_DMFSAssessmentModelValidator"/> class.
    /// </summary>
    public DMFT_DMFSAssessmentModelValidator()
    {
        // Validate UpperMouth
        RuleFor(x => x.UpperMouth)
            .NotNull()
            .SetValidator(new UpperMouthValidator())
            .OverridePropertyName("UpperMouth");

        // Validate LowerMouth
        RuleFor(x => x.LowerMouth)
            .NotNull()
            .SetValidator(new LowerMouthValidator())
            .OverridePropertyName("LowerMouth");
    }
}

/// <summary>
/// Validator for UpperMouth
/// </summary>
public class UpperMouthValidator : AbstractValidator<UpperMouth>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpperMouthValidator"/> class.
    /// </summary>
    public UpperMouthValidator()
    {
        RuleFor(x => x.Tooth_18).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_18");

        RuleFor(x => x.Tooth_17).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_17");

        RuleFor(x => x.Tooth_16).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_16");

        RuleFor(x => x.Tooth_15).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_15");

        RuleFor(x => x.Tooth_14).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_14");

        RuleFor(x => x.Tooth_24).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_24");

        RuleFor(x => x.Tooth_25).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_25");

        RuleFor(x => x.Tooth_26).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_26");

        RuleFor(x => x.Tooth_27).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_27");

        RuleFor(x => x.Tooth_28).SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_28");

        // Validate Tooth_13, Tooth_12, Tooth_11, Tooth_21, Tooth_22, Tooth_23
        RuleFor(x => x.Tooth_13)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_13");

        RuleFor(x => x.Tooth_12)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_12");

        RuleFor(x => x.Tooth_11)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_11");

        RuleFor(x => x.Tooth_21)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_21");

        RuleFor(x => x.Tooth_22)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_22");

        RuleFor(x => x.Tooth_23)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_23");

        // Validate Tooth_48, Tooth_47, Tooth_46, Tooth_45, Tooth_44, Tooth_34, Tooth_35, Tooth_36, Tooth_37, Tooth_38
        RuleFor(x => x.Tooth_55)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_55");

        RuleFor(x => x.Tooth_54)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_54");

        RuleFor(x => x.Tooth_53)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_53");

        RuleFor(x => x.Tooth_52)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_52");

        RuleFor(x => x.Tooth_51)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_51");

        RuleFor(x => x.Tooth_61)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_61");

        RuleFor(x => x.Tooth_62)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_62");

        RuleFor(x => x.Tooth_63)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_63");

        RuleFor(x => x.Tooth_64)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_64");

        RuleFor(x => x.Tooth_65)
            .Must(BeValidValue)
            .WithMessage("Must between be '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_65");
    }

    /// <summary>
    /// Validate if the values is x or 1 to 10
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is x or 1 to 10, otherwise false</returns>
    private bool BeValidValue(string value) =>
        value == "x" ||
        value == "1" ||
        value == "2" ||
        value == "3" ||
        value == "4" ||
        value == "5" ||
        value == "6" ||
        value == "7" ||
        value == "8" ||
        value == "9" ||
        value == "10" ||
        value == "0" ||
        value == "";
}

/// <summary>
/// Validator for LowerMouth
/// </summary>
public class LowerMouthValidator : AbstractValidator<LowerMouth>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LowerMouthValidator"/> class.
    /// </summary>
    public LowerMouthValidator()
    {
        // Validate Tooth_48, Tooth_47, Tooth_46, Tooth_45, Tooth_44, Tooth_34, Tooth_35, Tooth_36, Tooth_37, Tooth_38
        RuleFor(x => x.Tooth_48)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_48");

        RuleFor(x => x.Tooth_47)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_47");

        RuleFor(x => x.Tooth_46)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_46");

        RuleFor(x => x.Tooth_45)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_45");

        RuleFor(x => x.Tooth_44)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_44");

        RuleFor(x => x.Tooth_34)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_34");

        RuleFor(x => x.Tooth_35)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_35");

        RuleFor(x => x.Tooth_36)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_36");

        RuleFor(x => x.Tooth_37)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_37");

        RuleFor(x => x.Tooth_38)
            .SetValidator(new DMFT_DMFSSixSurfaceToothValidator())
            .OverridePropertyName("Tooth_38");


        // Validate Tooth_43, Tooth_42, Tooth_41, Tooth_31, Tooth_32, Tooth_33
        RuleFor(x => x.Tooth_43)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_43");

        RuleFor(x => x.Tooth_42)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_42");

        RuleFor(x => x.Tooth_41)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_41");

        RuleFor(x => x.Tooth_31)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_31");

        RuleFor(x => x.Tooth_32)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_32");

        RuleFor(x => x.Tooth_33)
            .SetValidator(new DMFT_DMFSFiveSurfaceToothValidator())
            .OverridePropertyName("Tooth_33");


        // Validate Tooth_85, Tooth_84, Tooth_83, Tooth_82, Tooth_81, Tooth_71, Tooth_72, Tooth_73, Tooth_74, Tooth_75
        RuleFor(x => x.Tooth_85)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_85");

        RuleFor(x => x.Tooth_84)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_84");

        RuleFor(x => x.Tooth_83)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_83");

        RuleFor(x => x.Tooth_82)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_82");

        RuleFor(x => x.Tooth_81)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_81");

        RuleFor(x => x.Tooth_71)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_71");

        RuleFor(x => x.Tooth_72)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_72");

        RuleFor(x => x.Tooth_73)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_73");

        RuleFor(x => x.Tooth_74)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_74");

        RuleFor(x => x.Tooth_75)
            .Must(BeValidValue)
            .WithMessage("Must be between '1' to '10' or 'x'.")
            .OverridePropertyName("Tooth_75");
    }

    /// <summary>
    /// Validate if the values is x or 1 to 10
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is x or 1 to 10, otherwise false</returns>
    private bool BeValidValue(string value) =>
        value == "x" ||
        value == "1" ||
        value == "2" ||
        value == "3" ||
        value == "4" ||
        value == "5" ||
        value == "6" ||
        value == "7" ||
        value == "8" ||
        value == "9" ||
        value == "10" ||
        value == "0" ||
        value == "";
}

/// <summary>
/// Validator for DMFT_DMFS Five Surface Tooth
/// </summary>
public class DMFT_DMFSFiveSurfaceToothValidator : AbstractValidator<FiveSurfaceToothDMFT_DMFS>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DMFT_DMFSFiveSurfaceToothValidator"/> class.
    /// </summary>
    public DMFT_DMFSFiveSurfaceToothValidator()
    {
        RuleFor(x => x.R).Must(BeValidValue).WithMessage("R must be between '0' to '10' or 'x'."); // Validate Root
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("B must be between '0' to '10' or 'x'."); // Validate Buccal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("L must be between '0' to '10' or 'x'."); // Validate Lingual
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("D must be between '0' to '10' or 'x'."); // Validate Distal
        RuleFor(x => x.M).Must(BeValidValue).WithMessage("M must be between '0' to '10' or 'x'."); // Validate Mesial
    }

    /// <summary>
    /// Validate if the values is x or 1 to 10
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is x or 1 to 10, otherwise false</returns>
    private bool BeValidValue(string value) =>
        value == "x" ||
        value == "1" ||
        value == "2" ||
        value == "3" ||
        value == "4" ||
        value == "5" ||
        value == "6" ||
        value == "7" ||
        value == "8" ||
        value == "9" ||
        value == "10" ||
        value == "0" ||
        value == "";
}

/// <summary>
/// Validator for DMFT_DMFS Six Surface Tooth
/// </summary>
public class DMFT_DMFSSixSurfaceToothValidator : AbstractValidator<SixSurfaceTooth>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DMFT_DMFSSixSurfaceToothValidator"/> class.
    /// </summary>
    public DMFT_DMFSSixSurfaceToothValidator()
    {
        RuleFor(x => x.R).Must(BeValidValue).WithMessage("R must be between '0' to '10' or 'x'."); // Validate Root
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("B must be between '0' to '10' or 'x'."); // Validate Buccal
        RuleFor(x => x.O).Must(BeValidValue).WithMessage("O must be between '0' to '10' or 'x'."); // Validate Occlusal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("L must be between '0' to '10' or 'x'."); // Validate Lingual
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("D must be between '0' to '10' or 'x'."); // Validate Distal
        RuleFor(x => x.M).Must(BeValidValue).WithMessage("M must be between '0' to '10' or 'x'."); // Validate Mesial
    }

    /// <summary>
    /// Validate if the values is x or 1 to 10
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>True if the value is x or 1 to 10, otherwise false</returns>
    private bool BeValidValue(string value) =>
        value == "x" ||
        value == "1" ||
        value == "2" ||
        value == "3" ||
        value == "4" ||
        value == "5" ||
        value == "6" ||
        value == "7" ||
        value == "8" ||
        value == "9" ||
        value == "10" ||
        value == "0" ||
        value == "";
}