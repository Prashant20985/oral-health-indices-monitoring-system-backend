using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.UpdateExam;

/// <summary>
/// Validator for the <see cref="UpdateExamCommand"/>.
/// </summary>
public class UpdateExamCommandValidator
    : AbstractValidator<UpdateExamCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateExamCommandValidator"/> class.
    /// </summary>
    public UpdateExamCommandValidator()
    {
        RuleFor(x => x.UpdateExam.DateOfExamination)
            .NotEmpty()
            .GreaterThan(DateTime.Now)
            .OverridePropertyName("DateOfExamination");

        RuleFor(x => x.UpdateExam.ExamTitle)
            .NotEmpty()
            .MaximumLength(100)
            .OverridePropertyName("ExamTitle");

        RuleFor(x => x.UpdateExam.Description)
            .MaximumLength(500)
            .OverridePropertyName("Description");

        RuleFor(x => x.UpdateExam.StartTime)
            .NotEmpty()
            .Must((command, startTime) => startTime < command.UpdateExam.EndTime)
            .WithMessage("Start time must be less than end time")
            .OverridePropertyName("StartTime");

        RuleFor(x => x.UpdateExam.EndTime)
            .NotEmpty()
            .Must((command, endTime) => endTime > command.UpdateExam.StartTime)
            .WithMessage("End time must be greater than start time")
            .OverridePropertyName("EndTime");

        RuleFor(x => x.UpdateExam.DurationInterval)
            .NotEmpty()
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Duration must be greater than zero")
            .Must((command, duration) => duration <= command.UpdateExam.EndTime - command.UpdateExam.StartTime)
            .OverridePropertyName("Duration");
    }
}
