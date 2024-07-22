using App.Domain.Models.Common.RiskFactorAssessment;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommandValidators;

/// <summary>
/// Validator for RiskFactorAssessmentModel
/// </summary>
public class RiskFactorAssessmentModelValidator : AbstractValidator<RiskFactorAssessmentModel>
{
    // Predefined questions
    private readonly List<string> PredefinedQuestions =
    [
        "Fluoride exposure",
        "Consumption of sweetened products and beverages",
        "Systematic Dental Care",
        "Systemic diseases",
        "Eating disorders",
        "Complex Pharmacotherapy",
        "Alcohol/Nicotine",
        "New carious lesions in the last 36 months",
        "Visible Plaque",
        "Teeth extraction due to caries in the last 36 months",
        "Abnormal Tooth Morphology",
        "1 or more proximal restorations",
        "Exposed root surfaces",
        "Overhanging fills, no contact points",
        "Fixed Orthodontic Braces",
        "Xerostomy",
        "Caries risk factor assessment"
    ];


    /// <summary>
    /// Initializes a new instance of the <see cref="RiskFactorAssessmentModelValidator"/> class.
    /// </summary>
    public RiskFactorAssessmentModelValidator()
    {
        // Validate Questions
        RuleFor(x => x.Questions)
            .NotNull().WithMessage("Questions must not be null.")
            .Must(questions => questions.Count == 17).WithMessage("There must be exactly 17 questions.") // 17 predefined questions
            .Must(questions => questions.Select(q => q.QuestionText).Distinct().Count() == 17).WithMessage("Questions must be unique.") // 17 unique questions
            .ForEach(question =>
            {
                question.SetValidator(new QuestionValidator());
                question.Must(q => PredefinedQuestions.Contains(q.QuestionText)).WithMessage("Invalid question text."); // Question text must be in predefined questions
            });
    }
}

/// <summary>
/// Validator for RiskFactorAssessmentQuestionModel
/// </summary>
public class QuestionValidator : AbstractValidator<RiskFactorAssessmentQuestionModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionValidator"/> class.
    /// </summary>
    public QuestionValidator()
    {
        // Validate QuestionText
        RuleFor(x => x.QuestionText)
            .NotEmpty().WithMessage("QuestionText must not be empty.");

        // Validate Answer
        RuleFor(x => x.Answer)
            .NotNull().WithMessage("Answer must not be null.")
            .SetValidator(new AnswerValidator());
    }
}

/// <summary>
/// Validator for RiskFactorAssessmentAnswerModel
/// </summary>
public class AnswerValidator : AbstractValidator<RiskFactorAssessmentAnswerModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnswerValidator"/> class.
    /// </summary>
    public AnswerValidator()
    {
        // Validate LowRisk
        RuleFor(x => x.LowRisk)
            .NotNull().WithMessage("LowRisk must not be null.");

        // Validate ModerateRisk
        RuleFor(x => x.ModerateRisk)
            .NotNull().WithMessage("ModerateRisk must not be null.");

        // Validate HighRisk
        RuleFor(x => x.HighRisk)
            .NotNull().WithMessage("HighRisk must not be null.");
    }
}

