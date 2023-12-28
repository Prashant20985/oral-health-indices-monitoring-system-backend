using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.PatientOperations.Command.CreatePatient;

/// <summary>
/// Validator for the CreatePatientCommand command.
/// </summary>
public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePatientCommandValidator"/> class.
    /// Configures validation rules for the properties of the CreatePatientCommand.
    /// </summary>
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.CreatePatient.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("First name is required.");

        RuleFor(x => x.CreatePatient.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Last name is required.");

        RuleFor(x => x.CreatePatient.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required.");

        RuleFor(x => x.CreatePatient.Gender)
            .Must(r => string.IsNullOrWhiteSpace(r) || Enum.IsDefined(typeof(Gender), r))
            .WithMessage("Invalid Gender Input");

        RuleFor(x => x.CreatePatient.EthnicGroup)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Ethnic Group is required");

        RuleFor(x => x.CreatePatient.OtherGroup)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.CreatePatient.OtherGroup))
            .WithMessage("Max length id 50");

        RuleFor(x => x.CreatePatient.YearsInSchool)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Years in school is required");

        RuleFor(x => x.CreatePatient.OtherData)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.CreatePatient.OtherData))
            .WithMessage("Max length id 50");

        RuleFor(x => x.CreatePatient.OtherData2)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.CreatePatient.OtherData2))
            .WithMessage("Max length id 50");

        RuleFor(x => x.CreatePatient.OtherData3)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.CreatePatient.OtherData3))
            .WithMessage("Max length id 50");

        RuleFor(x => x.CreatePatient.Location)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Location is required");

        RuleFor(x => x.CreatePatient.Age)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Age is required");

        RuleFor(x => x.DoctorId)
            .NotNull()
            .NotEmpty()
            .Must(r => string.IsNullOrEmpty(r) || Guid.TryParse(r, out _))
            .WithMessage("DoctorId is required");
    }
}
