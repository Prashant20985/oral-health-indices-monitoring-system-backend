using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.CommentDMFT_DMFSForm;

/// <summary>
/// Validator for the CommentDMFT_DMFSCommand
/// </summary>
public class CommentDMFT_DMFSCommandValidator : AbstractValidator<CommentDMFT_DMFSCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentDMFT_DMFSCommandValidator"/> class.
    /// </summary>
    public CommentDMFT_DMFSCommandValidator()
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
