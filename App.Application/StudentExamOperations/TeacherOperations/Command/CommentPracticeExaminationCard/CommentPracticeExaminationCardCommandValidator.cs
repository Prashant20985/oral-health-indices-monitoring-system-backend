using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.CommentPracticeExaminationCard;

/// <summary>
/// Validator for the <see cref="CommentPracticeExaminationCardCommand"/> command.
/// </summary>
public class CommentPracticeExaminationCardCommandValidator : AbstractValidator<CommentPracticeExaminationCardCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentPracticeExaminationCardCommandValidator"/> class.
    /// </summary>
    public CommentPracticeExaminationCardCommandValidator()
    {
        //  Validate the PracticeExaminationCardId property.
        RuleFor(x => x.PracticeExaminationCardId).NotEmpty();

        //  Validate the DoctorComment property.
        RuleFor(x => x.DoctorComment)
            .NotEmpty()
            .MaximumLength(500)
            .OverridePropertyName("Doctor Comment");
    }
}

