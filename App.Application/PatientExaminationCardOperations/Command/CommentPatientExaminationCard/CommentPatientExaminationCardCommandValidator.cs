using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CommentPatientExaminationCard;

/// <summary>
/// Validator for CommentPatientExaminationCardCommand
/// </summary>
public class CommentPatientExaminationCardCommandValidator : AbstractValidator<CommentPatientExaminationCardCommand>
{
    /// <summary>
    /// Constructor for CommentPatientExaminationCardCommandValidator
    /// </summary>
    public CommentPatientExaminationCardCommandValidator()
    {
        // Defining a validation rule for the Cardid property
        RuleFor(command => command.Cardid)
            .NotNull()
            .NotEmpty();

        // Defining a validation rule for the Comment property
        RuleFor(command => command.Comment)
            .MaximumLength(500);
    }
}
