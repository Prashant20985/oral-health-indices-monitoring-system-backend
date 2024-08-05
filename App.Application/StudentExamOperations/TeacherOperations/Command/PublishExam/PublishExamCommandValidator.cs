using FluentValidation;

namespace App.Application.StudentExamOperations.TeacherOperations.Command.PublishExam;

/// <summary>
/// Validator for the PublishExamCommand.
/// </summary>
public class PublishExamCommandValidator : AbstractValidator<PublishExamCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PublishExamCommandValidator"/> class.
    /// </summary>
    public PublishExamCommandValidator()
    {
        RuleFor(x => x.PublishExam.DateOfExamination)
            .NotEmpty()
            .OverridePropertyName("DateOfExamination");

        RuleFor(x => x.PublishExam.ExamTitle)
            .NotEmpty()
            .MaximumLength(100)
            .OverridePropertyName("ExamTitle");

        RuleFor(x => x.PublishExam.Description)
            .MaximumLength(500)
            .OverridePropertyName("Description");

        RuleFor(x => x.PublishExam.StartTime)
            .NotEmpty()
            .Must((command, startTime) => startTime < command.PublishExam.EndTime)
            .WithMessage("Start time must be less than end time")
            .Must((command, startTime) =>
            {
                var now = DateTime.Now;
                return command.PublishExam.DateOfExamination.Date != now.Date ||
                       startTime > TimeOnly.FromDateTime(now);
            })
            .WithMessage("If the exam is today, start time must be later than the current time")
            .OverridePropertyName("StartTime");


        RuleFor(x => x.PublishExam.EndTime)
            .NotEmpty()
            .Must((command, endTime) => endTime > command.PublishExam.StartTime)
            .WithMessage("End time must be greater than start time")
            .OverridePropertyName("EndTime");

        RuleFor(x => x.PublishExam.GroupId)
            .NotEmpty()
            .OverridePropertyName("GroupId");

        RuleFor(x => x.PublishExam.DurationInterval)
            .NotEmpty()
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("Duration must be greater than zero")
            .Must((command, duration) => duration <= command.PublishExam.EndTime - command.PublishExam.StartTime)
            .OverridePropertyName("Duration");
    }
}
