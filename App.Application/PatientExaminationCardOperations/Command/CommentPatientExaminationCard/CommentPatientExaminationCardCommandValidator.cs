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
        RuleFor(command => command.Cardid)
            .NotEmpty();

        RuleFor(command => command.Comment)
            .NotEmpty()
            .MaximumLength(500);
    }
}
