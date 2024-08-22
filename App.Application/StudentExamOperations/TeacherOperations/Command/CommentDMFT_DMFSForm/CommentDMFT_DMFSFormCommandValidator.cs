using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentDMFT_DMFSForm;

/// <summary>
/// Validator for the <see cref="CommentDMFT_DMFSFormCommand"/> command.
/// </summary>
public class CommentDMFT_DMFSFormCommandValidator : AbstractValidator<CommentDMFT_DMFSFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentDMFT_DMFSFormCommandValidator"/> class.
    /// </summary>
    public CommentDMFT_DMFSFormCommandValidator()
    {
        // Validate the PracticeExaminationCardId property.
        RuleFor(x => x.PracticeExaminationCardId).NotEmpty();

        // Validate the DoctorComment property.
        RuleFor(x => x.DoctorComment)
            .NotEmpty()
            .MaximumLength(500)
            .OverridePropertyName("Doctor Comment");
    }
}
