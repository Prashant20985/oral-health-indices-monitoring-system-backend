using FluentValidation;

namespace App.Application.PatientOperations.Command.ArchivePatient;

/// <summary>
/// Validator for the ArchivePatientCommand.
/// </summary>
public class ArchivePatientCommandValidator : AbstractValidator<ArchivePatientCommand>
{
    /// <summary>
    /// Configures validation rules for the ArchivePatientCommand properties.
    /// </summary>
    public ArchivePatientCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Patient Id is required.");

        RuleFor(x => x.ArchiveComment)
            .NotEmpty()
            .NotNull()
            .MaximumLength(500)
            .WithMessage("Archive Comment is required.");
    }
}
