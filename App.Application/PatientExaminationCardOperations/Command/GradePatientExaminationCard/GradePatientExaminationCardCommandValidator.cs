using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.Command.GradePatientExaminationCard;

/// <summary>
/// The validator for the <see cref="GradePatientExaminationCardCommand"/>.
/// </summary>
public class GradePatientExaminationCardCommandValidator
    : AbstractValidator<GradePatientExaminationCardCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GradePatientExaminationCardCommandValidator"/> class.
    /// </summary>
    public GradePatientExaminationCardCommandValidator()
    {
        // Validate CardId
        RuleFor(command => command.CardId)
            .NotNull()
            .NotEmpty();

        // Validate TotalScore
        RuleFor(command => command.TotalScore)
            .NotNull();
    }
}
