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
        // Validate the ExamId property for the ExamId property.
        RuleFor(x => x.ExamId).NotEmpty();

        // Validate the StudentId property for the StudentId property.
        RuleFor(x => x.StudentId).NotEmpty();

        // Validate the CardInputModel for the PatientDto property.
        RuleFor(x => x.CardInputModel.PatientDto).SetValidator(new PracticePatientValidator());

        // Validate the CardInputModel property for the NeedForDentalInterventions property 
        RuleFor(x => x.CardInputModel.Summary.NeedForDentalInterventions)
            .MaximumLength(1);

        // Validate the CardInputModel property for the Description property.
        RuleFor(x => x.CardInputModel.Summary.Description)
            .MaximumLength(500);

        // Validate the CardInputModel property for the ProposedTreatment property.
        RuleFor(x => x.CardInputModel.Summary.ProposedTreatment)
            .MaximumLength(500);

        // Validate the CardInputModel property for the PatientRecommendations property.
        RuleFor(x => x.CardInputModel.Summary.PatientRecommendations)
            .MaximumLength(500);

        // Validate the CardInputModel property for the PracticeDMFT_DMFS property.
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
        // Validate for the FirstName property.
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("First name is required.");

        // Validate for the LastName property.
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Last name is required.");

        // Validate for the Email property.
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required.");

        // Validate for the Gender property.
        RuleFor(x => x.Gender)
            .Must(r => string.IsNullOrWhiteSpace(r) || Enum.IsDefined(typeof(Gender), r))
            .WithMessage("Invalid Gender Input");

        // Validate for the EthnicGroup property.
        RuleFor(x => x.EthnicGroup)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Ethnic Group is required");

        // Validate for the OtherGroup property.
        RuleFor(x => x.OtherGroup)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherGroup))
            .WithMessage("Max length id 50");

        // Validate for the YearsInSchool property.
        RuleFor(x => x.YearsInSchool)
            .NotNull()
            .WithMessage("Years in school is required");

        // Validate for the OtherData property.
        RuleFor(x => x.OtherData)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherData))
            .WithMessage("Max length id 50");

        // Validate for the OtherData2 property.
        RuleFor(x => x.OtherData2)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherData2))
            .WithMessage("Max length id 50");

        // Validate for the OtherData3 property.
        RuleFor(x => x.OtherData3)
            .MaximumLength(50)
            .Unless(x => string.IsNullOrEmpty(x.OtherData3))
            .WithMessage("Max length id 50");

        // Validate for the Location property.
        RuleFor(x => x.Location)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Location is required");

        // Validate for the Age property.
        RuleFor(x => x.Age)
            .NotNull()
            .WithMessage("Age is required");
    }
}
