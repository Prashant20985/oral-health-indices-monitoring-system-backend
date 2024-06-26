﻿using App.Domain.Models.Common.RiskFactorAssessment;
using FluentValidation;

namespace App.Application.PatientExaminationCardOperations.CommanValidators;

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
        "Regular dental care",
        "Systemic diseases",
        "Eating disorders",
        "Complex pharmacotherapy",
        "Alcohol/Nicotine",
        "New caries foci within last 36 months",
        "Visible plaque",
        "Tooth extraction for caries within the last 36 months",
        "Unusual tooth morphology",
        "1 or more fills on tangent surfaces",
        "Exposed root surfaces",
        "Overhanging fills, no contact points",
        "Fixed braces",
        "Xerostomy",
        "Caries risk factors assessment"
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
            .NotEmpty().WithMessage("LowRisk must not be empty.");

        // Validate ModerateRisk
        RuleFor(x => x.ModerateRisk)
            .NotEmpty().WithMessage("ModerateRisk must not be empty.");

        // Validate HighRisk
        RuleFor(x => x.HighRisk)
            .NotEmpty().WithMessage("HighRisk must not be empty.");
    }
}

