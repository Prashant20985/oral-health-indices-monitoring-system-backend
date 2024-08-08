using App.Domain.DTOs.PatientDtos.Request;
using App.Domain.Models.Enums;
using FluentValidation;

namespace App.Application.StudentExamOperations.StudentOperations.Command.AddPracticePatientExmaintionCard;

/// <summary>
/// Validator for the <see cref="AddPracticePatientExaminationCardCommand"/> command.
/// </summary>
public class AddPracticePatientExaminationCardCommandValidator : AbstractValidator<AddPracticePatientExaminationCardCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddPracticePatientExaminationCardCommandValidator"/> class.
    /// </summary>
    public AddPracticePatientExaminationCardCommandValidator()
    {
        RuleFor(x => x.ExamId).NotEmpty();

        RuleFor(x => x.StudentId).NotEmpty();

        RuleFor(x => x.CardInputModel.PatientDto).SetValidator(new PracticePatientValidator());

        RuleFor(x => x.CardInputModel.Summary.NeedForDentalInterventions)
            .MaximumLength(1);

        RuleFor(x => x.CardInputModel.Summary.Description)
            .MaximumLength(500);

        RuleFor(x => x.CardInputModel.Summary.ProposedTreatment)
            .MaximumLength(500);

        RuleFor(x => x.CardInputModel.Summary.PatientRecommendations)
            .MaximumLength(500);

        RuleFor(x => x.CardInputModel.PracticeDMFT_DMFS.ProstheticStatus)
            .MaximumLength(1);
    }
}

/// <summary>
/// Validator for the <see cref="CreatePatientDto"/> class.
/// </summary>
public class PracticePatientValidator : AbstractValidator<CreatePatientDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PracticePatientValidator"/> class.
    /// </summary>
    public PracticePatientValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Last name is required.");

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required.");

        RuleFor(x => x.Gender)
            .Must(r => string.IsNullOrWhiteSpace(r) || Enum.IsDefined(typeof(Gender), r))
            .WithMessage("Invalid Gender Input");

        RuleFor(x => x.EthnicGroup)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Ethnic Group is required");

        RuleFor(x => x.OtherGroup)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherGroup))
            .WithMessage("Max length id 50");

        RuleFor(x => x.YearsInSchool)
            .NotNull()
            .WithMessage("Years in school is required");

        RuleFor(x => x.OtherData)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherData))
            .WithMessage("Max length id 50");

        RuleFor(x => x.OtherData2)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherData2))
            .WithMessage("Max length id 50");

        RuleFor(x => x.OtherData3)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherData3))
            .WithMessage("Max length id 50");

        RuleFor(x => x.Location)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Location is required");

        RuleFor(x => x.Age)
            .NotNull()
            .WithMessage("Age is required");
    }
}
