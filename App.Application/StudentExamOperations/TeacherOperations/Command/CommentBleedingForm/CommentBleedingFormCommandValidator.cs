using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentBleedingForm;

/// <summary>
/// Validator for the <see cref="CommentBleedingFormCommand"/> command.
/// </summary>
public class CommentBleedingFormCommandValidator : AbstractValidator<CommentBleedingFormCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentBleedingFormCommandValidator"/> class.
    /// </summary>
    public CommentBleedingFormCommandValidator()
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
