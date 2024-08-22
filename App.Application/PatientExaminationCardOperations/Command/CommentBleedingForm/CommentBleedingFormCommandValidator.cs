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
        // Defining a validation rule for the CardId property
        RuleFor(x => x.CardId)
            .NotNull()
            .NotEmpty();

        // Defining a validation rule for the Comment property  
        RuleFor(x => x.Comment)
            .MaximumLength(500);
    }
}
