using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.PatientOperations.Command.UpdatePatient;

/// <summary>
/// Validator for the UpdatePatientCommand command, responsible for validating input data.
/// </summary>
public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdatePatientCommandValidator class.
    /// </summary>
    public UpdatePatientCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.UpdatePatient.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("First name is required.");

        RuleFor(x => x.UpdatePatient.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Last name is required.");

        RuleFor(x => x.UpdatePatient.Gender)
            .Must(r => string.IsNullOrWhiteSpace(r) || Enum.IsDefined(typeof(Gender), r))
            .WithMessage("Invalid Gender Input");

        RuleFor(x => x.UpdatePatient.EthnicGroup)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Ethnic Group is required");

        RuleFor(x => x.UpdatePatient.OtherGroup)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.UpdatePatient.OtherGroup))
            .WithMessage("Max length id 50");

        RuleFor(x => x.UpdatePatient.YearsInSchool)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Years in school is required");

        RuleFor(x => x.UpdatePatient.OtherData)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.UpdatePatient.OtherData))
            .WithMessage("Max length id 50");

        RuleFor(x => x.UpdatePatient.OtherData2)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.UpdatePatient.OtherData2))
            .WithMessage("Max length id 50");

        RuleFor(x => x.UpdatePatient.OtherData3)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.UpdatePatient.OtherData3))
            .WithMessage("Max length id 50");

        RuleFor(x => x.UpdatePatient.Location)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Location is required");

        RuleFor(x => x.UpdatePatient.Age)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Age is required");
    }
}
