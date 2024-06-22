using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIForm;

public class CommentAPIFormCommandValidator : AbstractValidator<CommentAPIFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentAPIFormCommandValidator"/> class.
    /// </summary>
    public CommentAPIFormCommandValidator()
    {
        RuleFor(x => x.PracticeExaminationCardId).NotEmpty();

        RuleFor(x => x.DoctorComment)
            .NotEmpty()
            .MaximumLength(500)
            .OverridePropertyName("Doctor Comment");
    }
}
