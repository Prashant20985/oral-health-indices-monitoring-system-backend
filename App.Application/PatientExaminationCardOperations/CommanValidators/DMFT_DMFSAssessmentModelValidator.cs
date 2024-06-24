using App.Domain.Models.Common.DMFT_DMFS;
using App.Domain.Models.Common.Tooth;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommanValidators;

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
            .SetValidator(new UpperMouthValidator());

        // Validate LowerMouth
        RuleFor(x => x.LowerMouth)
            .NotNull()
            .SetValidator(new LowerMouthValidator());
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
        // Validate Tooth_18, Tooth_17, Tooth_16, Tooth_15, Tooth_14, Tooth_24, Tooth_25, Tooth_26, Tooth_27, Tooth_28
        RuleForEach(x => new[]
        {
            x.Tooth_18,
            x.Tooth_17,
            x.Tooth_16,
            x.Tooth_15,
            x.Tooth_14,
            x.Tooth_24,
            x.Tooth_25,
            x.Tooth_26,
            x.Tooth_27,
            x.Tooth_28
        }).SetValidator(new DMFT_DMFSSixSurfaceToothValidator());

        // Validate Tooth_13, Tooth_12, Tooth_11, Tooth_21, Tooth_22, Tooth_23
        RuleForEach(x => new[]
        {
            x.Tooth_13,
            x.Tooth_12,
            x.Tooth_11,
            x.Tooth_21,
            x.Tooth_22,
            x.Tooth_23
        }).SetValidator(new DMFT_DMFSFiveSurfaceToothValidator());
        
        // Validate Tooth_48, Tooth_47, Tooth_46, Tooth_45, Tooth_44, Tooth_34, Tooth_35, Tooth_36, Tooth_37, Tooth_38
        RuleForEach(x => new[]
        {
            x.Tooth_55,
            x.Tooth_54,
            x.Tooth_53,
            x.Tooth_52,
            x.Tooth_51,
            x.Tooth_61,
            x.Tooth_62,
            x.Tooth_63,
            x.Tooth_64,
            x.Tooth_65
        }).Must(BeValidValue)
        .WithMessage("Must be '1' to '10' or 'x'.");
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
        value == "10";
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
        // Validate Tooth_85, Tooth_84, Tooth_83, Tooth_82, Tooth_81, Tooth_71, Tooth_72, Tooth_73, Tooth_74, Tooth_75
        RuleForEach(x => new[]
        {
            x.Tooth_48,
            x.Tooth_47,
            x.Tooth_46,
            x.Tooth_45,
            x.Tooth_44,
            x.Tooth_34,
            x.Tooth_35,
            x.Tooth_36,
            x.Tooth_37,
            x.Tooth_38
        }).SetValidator(new DMFT_DMFSSixSurfaceToothValidator());

        // Validate Tooth_84, Tooth_83, Tooth_82, Tooth_81, Tooth_71, Tooth_72, Tooth_73, Tooth_74, Tooth_75
        RuleForEach(x => new[]
        {
            x.Tooth_43,
            x.Tooth_42,
            x.Tooth_41,
            x.Tooth_31,
            x.Tooth_32,
            x.Tooth_33
        }).SetValidator(new DMFT_DMFSFiveSurfaceToothValidator());

        // Validate Tooth_85, Tooth_84, Tooth_83, Tooth_82, Tooth_81, Tooth_71, Tooth_72, Tooth_73, Tooth_74, Tooth_75
        RuleForEach(x => new[]
        {
            x.Tooth_85,
            x.Tooth_84,
            x.Tooth_83,
            x.Tooth_82,
            x.Tooth_81,
            x.Tooth_71,
            x.Tooth_72,
            x.Tooth_73,
            x.Tooth_74,
            x.Tooth_75
        }).Must(BeValidValue)
        .WithMessage("Must be '1' to '10' or 'x'.");
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
        value == "10";
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
        RuleFor(x => x.R).Must(BeValidValue).WithMessage("R must be '1' to '10' or 'x'."); // Validate Root
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("B must be '1' to '10' or 'x'."); // Validate Buccal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("L must be '1' to '10' or 'x'."); // Validate Lingual
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("D must be '1' to '10' or 'x'."); // Validate Distal
        RuleFor(x => x.M).Must(BeValidValue).WithMessage("M must be '1' to '10' or 'x'."); // Validate Mesial
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
        value == "10";
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
        RuleFor(x => x.R).Must(BeValidValue).WithMessage("R must be '1' to '10' or 'x'."); // Validate Root
        RuleFor(x => x.B).Must(BeValidValue).WithMessage("B must be '1' to '10' or 'x'."); // Validate Buccal
        RuleFor(x => x.O).Must(BeValidValue).WithMessage("O must be '1' to '10' or 'x'."); // Validate Occlusal
        RuleFor(x => x.L).Must(BeValidValue).WithMessage("L must be '1' to '10' or 'x'."); // Validate Lingual
        RuleFor(x => x.D).Must(BeValidValue).WithMessage("D must be '1' to '10' or 'x'."); // Validate Distal
        RuleFor(x => x.M).Must(BeValidValue).WithMessage("M must be '1' to '10' or 'x'."); // Validate Mesial
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
        value == "10";
}
