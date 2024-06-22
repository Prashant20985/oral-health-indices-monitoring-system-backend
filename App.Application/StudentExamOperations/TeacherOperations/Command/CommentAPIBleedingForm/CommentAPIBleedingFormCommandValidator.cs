using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentAPIBleedingForm;

/// <summary>
/// Validator for the <see cref="CommentAPIBleedingFormCommand"/> command.
/// </summary>
public class CommentAPIBleedingFormCommandValidator : AbstractValidator<CommentAPIBleedingFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentAPIBleedingFormCommandValidator"/> class.
    /// </summary>
    public CommentAPIBleedingFormCommandValidator()
    {
        RuleFor(x => x.PracticeExaminationCardId).NotEmpty();

        RuleFor(x => x.DoctorComment)
            .NotEmpty()
            .MaximumLength(500)
            .OverridePropertyName("Doctor Comment");
    }
}

