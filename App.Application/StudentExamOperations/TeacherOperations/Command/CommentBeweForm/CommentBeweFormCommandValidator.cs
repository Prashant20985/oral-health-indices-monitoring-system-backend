using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentBeweForm;

/// <summary>
/// Validator for the <see cref="CommentBeweFormCommand"/> command.
/// </summary>
public class CommentBeweFormCommandValidator : AbstractValidator<CommentBeweFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentBeweFormCommandValidator"/> class.
    /// </summary>
    public CommentBeweFormCommandValidator()
    {
        RuleFor(x => x.PracticeExaminationCardId).NotEmpty();

        RuleFor(x => x.DoctorComment)
            .NotEmpty()
            .MaximumLength(500)
            .OverridePropertyName("Doctor Comment");
    }
}
