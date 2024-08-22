using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CommentAPIForm;

/// <summary>
/// Validator for CommentAPIFormCommand
/// </summary>
public class CommentAPIFormCommandValidator : AbstractValidator<CommentAPIFormCommnand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentAPIFormCommandValidator"/> class.
    /// </summary>
    public CommentAPIFormCommandValidator()
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
