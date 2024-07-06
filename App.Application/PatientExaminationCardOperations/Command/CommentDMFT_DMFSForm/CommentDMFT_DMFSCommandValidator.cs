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
        RuleFor(x => x.CardId)
            .NotEmpty();

        RuleFor(x => x.Comment)
            .NotEmpty()
            .MaximumLength(500);
    }
}
