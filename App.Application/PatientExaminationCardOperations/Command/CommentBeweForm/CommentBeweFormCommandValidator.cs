using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CommentBeweForm;

/// <summary>
/// Validator for the CommentBeweFormCommand
/// </summary>
public class CommentBeweFormCommandValidator : AbstractValidator<CommentBeweFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentBeweFormCommandValidator"/> class.
    /// </summary>
    public CommentBeweFormCommandValidator()
    {
        // Defining a validation rule for the CardId property
        RuleFor(x => x.CardId)
            .NotNull()
            .NotEmpty();

        // Defining a validation rule for the Comment property
        RuleFor(x => x.Comment)
            .MaximumLength(500);
    }
}
