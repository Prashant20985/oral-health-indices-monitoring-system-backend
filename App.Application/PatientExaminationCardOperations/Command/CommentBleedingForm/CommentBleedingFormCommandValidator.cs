using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CommentBleedingForm;

/// <summary>
/// Validator for the CommentBleedingFormCommand
/// </summary>
public class CommentBleedingFormCommandValidator : AbstractValidator<CommentBleedingFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentBleedingFormCommandValidator"/> class.
    /// </summary>
    public CommentBleedingFormCommandValidator()
    {
        RuleFor(x => x.CardId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Comment)
            .MaximumLength(500);
    }
}
